using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Util;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
using www.aquarella.com.pe.be.Financiera;
using System.Globalization;
//using Bata.Aquarella.Pe.BE.Financiera;


namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class validateBank : System.Web.UI.Page
    {
        Users _user;
        private string _namsession_banco = "_namsession_banco";
        private string _namesession_inter = "_namesession_inter";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_namsession_banco] = "";
                Session[_namesession_inter] = null;
                dwBanks.DataSource = Banks.getAllBanks();
                dwBanks.DataBind();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int rpta;
            try
            {
                Session[_namsession_banco] = "";
                if (dwBanks.SelectedValue=="-1")
                {
                    msnMessage.LoadMessage("Seleccione el banco: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                    return;
                }
                string _banco = dwBanks.SelectedValue;

                if (FileUpload1.HasFile && System.Convert.ToInt32(dwTipArc.SelectedValue) != -1)
                {

                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    Random r = new Random();
                    int varAleatorio = r.Next(0, 999);

                    if (_banco!="4")
                    {
                        string FilePath = Server.MapPath(FolderPath + FileName + varAleatorio);
                        FileUpload1.SaveAs(FilePath);

                        //string fichero = Server.MapPath(FilePath);
                        //System.IO.StreamReader sr = new System.IO.StreamReader(fichero);
                        //string str = sr.ReadToEnd();

                        int val_dwTipArc = System.Convert.ToInt32(dwTipArc.SelectedValue);
                        rpta = validarArchivo(FilePath, Extension, val_dwTipArc);
                        if (rpta == 1)
                        {
                            Import_To_Grid(FilePath, Extension, val_dwTipArc);
                        }
                        else
                        {
                            msnMessage.LoadMessage("Debe seleccionar un tipo de archivo correcto: ", UserControl.ucMessage.MessageType.Error);
                            GridView1.DataBind();
                            return;

                        }

                        GridView1.Visible = true;

                        btGuardarDatos_2.Enabled = true;
                        File.Delete(FilePath);
                        Session[_namsession_banco] = dwBanks.SelectedValue;
                        msnMessage.LoadMessage("Carga correcta del archivo excel. Última actualización: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                    }
                    else
                    {
                        FileName = FileName.Substring(0, FileName.Length - 4);
                        string FilePath = Server.MapPath(FolderPath + FileName + varAleatorio.ToString() + Extension);
                        FileUpload1.SaveAs(FilePath);

                        string fichero = Server.MapPath(FolderPath + FileName + varAleatorio.ToString() + Extension);

                        string[] lineas = File.ReadAllLines(fichero);


                        DataTable dtinter = new DataTable();
                        dtinter.Columns.Add("DniRuc", typeof(string));
                        dtinter.Columns.Add("Pedido", typeof(string));
                        dtinter.Columns.Add("Op", typeof(string));
                        dtinter.Columns.Add("Fecha", typeof(DateTime));
                        dtinter.Columns.Add("Monto", typeof(decimal));

                        foreach(string lin in lineas)
                        {
                            string dniruc = lin.Substring(9,20).Trim();
                            string pedido = Convert.ToInt32(lin.Substring(37, 15).Trim()).ToString(); 
                            string op = lin.Substring(139, 8).Trim();
                            DateTime op_fecha =DateTime.ParseExact(lin.Substring(82, 8).Trim(), "yyyyMMdd", null, DateTimeStyles.None);                          
                            string enter =Convert.ToInt32(lin.Substring(96, 11).Trim()).ToString();
                            string decim= lin.Substring(96+11, 2).Trim().ToString();
                            Decimal op_monto =Convert.ToDecimal(enter + "." + decim);

                            dtinter.Rows.Add(dniruc, pedido, op, op_fecha, op_monto);
                        }
                        Session[_namesession_inter] = null;
                        DataTable dtreturn= Clear.getvalida_inter(dtinter);
                        Session[_namesession_inter] = dtreturn;
                        GridView1.Visible = true;
                        GridView1.DataSource = dtreturn;
                        GridView1.DataBind();
                        btGuardarDatos_2.Enabled = true;
                        File.Delete(FilePath);
                        Session[_namsession_banco] = dwBanks.SelectedValue;
                        if (dtreturn.Rows.Count==0)
                        {
                            msnMessage.LoadMessage("No hay datos para cargar , debido a que no esta registrado en nuestra base o ya ha sio registrado: " + DateTime.Now, UserControl.ucMessage.MessageType.Error);
                        }
                        else
                        { 
                            msnMessage.LoadMessage("Solo se carga los numero de OP PENDIENTES del banco interbank. Última actualización: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                        }
                        //System.IO.StreamReader sr = new System.IO.StreamReader(fichero);


                        // string str = sr.ReadToEnd();
                    }


                }
                else
                {
                    btGuardarDatos_2.Enabled = false;
                    msnMessage.LoadMessage("Debe seleccionar un tipo de archivo y adjuntarlo: ", UserControl.ucMessage.MessageType.Error);

                }
            }
            catch (Exception ex)
            {
                GridView1.Visible = false;
                btGuardarDatos_2.Enabled = false;
                string msg;
                msg = ex.Message;

                msnMessage.LoadMessage("Ocurrio un error: " + ex.Message, UserControl.ucMessage.MessageType.Error);

            }
        }


        private void Import_To_Grid(string FilePath, string Extension, int val_dwTipArc)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;

                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }

            conStr = String.Format(conStr, FilePath, "A4:F5", true);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            DataTable dt_Complete = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            
            if (val_dwTipArc == 1)
            {
                cmdExcel.CommandText = "SELECT [Cuenta],[F3],[F4],[F6] From [" + SheetName + "] where MID([F3], 1, 8) = 'EFECTIVO' ";
            }
            else
            {
                cmdExcel.CommandText = "SELECT [Cuenta],[F3],[F4],[F7] From [" + SheetName + "] where MID([F3], 1, 8) = 'EFECTIVO' ";
            }
           
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            //CultureInfo PE = new CultureInfo("es-PE");
            for (Int32 i = 0; i < dt.Rows.Count; ++i)
            {

               // dt.Rows[i]["F4"] = Convert.ToDecimal(dt.Rows[i]["F4"].ToString().Replace(',', '.'));
                string _MONTOstr = dt.Rows[i]["F4"].ToString().Replace(',', '.');

                dt.Rows[i]["F4"] = formato_numerico(_MONTOstr);
                //dt.Rows[i]["F4"] = _MONTO.ToString("N", CultureInfo.CurrentCulture);
                //dt.Rows[i]["F4"] = _MONTO.ToString("N", CultureInfo.CurrentCulture);

                if (val_dwTipArc == 1)
                {
                    dt.Rows[i]["F6"] = Convert.ToString(dt.Rows[i]["F6"].ToString().PadLeft(8, '0'));
                }
                else
                {
                    dt.Rows[i]["F7"] = Convert.ToString(dt.Rows[i]["F7"].ToString().PadLeft(8, '0'));
                }

            }

                connExcel.Close();

            GridView1.Caption = Path.GetFileName(FilePath);
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        private decimal formato_numerico(string _valor)
        {
            decimal monto = 0;
            long b = _valor.LongCount(letra => letra.ToString() == ".");

            if (b > 1)
            {
                string cad_decimal = _valor.Substring(_valor.Length - 3, 3);
                string cad_comas = _valor.Substring(0, _valor.Length - 3);
                cad_comas = cad_comas.Replace('.', ',');
                string _numero_str = cad_comas + cad_decimal;
                monto =Convert.ToDecimal(_numero_str);
            }
            else
            {
                _valor = _valor.Replace(',', '.');
                monto =Convert.ToDecimal(_valor);
            }

            return monto;
        }


        public static int validarArchivo(string FilePath, string Extension, int val_dwTipArc)
        {
            int rpta = 0;
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;

                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }

            conStr = String.Format(conStr, FilePath, "A4:F5", true);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            DataTable dt_Complete = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            if (val_dwTipArc == 1)
            {
                cmdExcel.CommandText = "SELECT count([F3]) AS operacion From [" + SheetName + "] where [F6] = 'Nº operación' ";
            }
            else
            {
                cmdExcel.CommandText = "SELECT count([F3]) AS operacion From [" + SheetName + "] where [F7] = 'Operación - Número' ";
            }

            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            int a = 0 ;
            foreach (DataRow dr in dt.Rows) //.Tables[0].Rows)
            {
                a = System.Convert.ToInt32(dr["operacion"]);
                if (val_dwTipArc == 1)
                {
                    if (a > 0)
                    { rpta = 1; 
                        break;
                    }
                }
                else
                {
                    if (a > 0)
                    { rpta = 1; 
                        break; 
                    }
                }
            }

            connExcel.Close();

            return rpta;

        }

        protected void btGuardarDatos_2_Click(object sender, EventArgs e)
        {
            string _banco = Session[_namsession_banco].ToString();
            if (_banco=="4")
            {
                SaveInter();
            }
            else
            {
                Save();
            }
           
        }
        private void SaveInter()
        {
           
            try
            {
                DataTable dt = (DataTable)Session[_namesession_inter];
                if (dt!=null)
                {
                    if (dt.Rows.Count==0)
                    {
                        msnMessage.LoadMessage("Lamentablemente no se ha guardado el archivo", UserControl.ucMessage.MessageType.Error);
                    }
                    else
                    {
                        DataTable dtvalida = new DataTable();
                        dtvalida.Columns.Add("dniruc", typeof(string));
                        dtvalida.Columns.Add("pedido", typeof(string));
                        dtvalida.Columns.Add("op", typeof(string));
                        dtvalida.Columns.Add("fecha", typeof(DateTime));
                        dtvalida.Columns.Add("monto", typeof(Decimal));

                        for(Int32 i=0;i<dt.Rows.Count;++i)
                        {
                            string dniruc = dt.Rows[i]["doc"].ToString() ;
                            string pedido = dt.Rows[i]["liq"].ToString();
                            string op = dt.Rows[i]["op"].ToString();
                            DateTime fecha =Convert.ToDateTime(dt.Rows[i]["fecha"]);
                            Decimal monto =Convert.ToDecimal(dt.Rows[i]["tpagar"]);
                            dtvalida.Rows.Add(dniruc, pedido, op, fecha, monto);

                        }

                        string ban_id = Session[_namsession_banco].ToString();

                        string _error=Documents_Trans.SaveValidaInter(ban_id, _user._bas_id,dtvalida);

                        if (_error!="-1")
                        {
                            msnMessage.LoadMessage("Se guardo correctamente. El archivo del banco Interbank" + " Última actualización: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                        }
                        else
                        {
                            msnMessage.LoadMessage("Lamentablemente no se ha guardado el archivo", UserControl.ucMessage.MessageType.Error);
                        }

                    }
                }
                else
                {
                    msnMessage.LoadMessage("Lamentablemente no se ha guardado el archivo", UserControl.ucMessage.MessageType.Error);
                }


            }
            catch (Exception ex)
            {

                GridView1.Visible = false;
                string msg;
                msg = ex.Message;

                msnMessage.LoadMessage("Se genero el error: " + ex.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        protected void Save()
        {
            Be_Documents_trans objArray = new Be_Documents_trans();
            Be_Documents_trans objRpta = new Be_Documents_trans();
            int contador;
            int restar2da = 0;
            contador = GridView1.Rows.Count;

            string[] varFecha = new string[contador];
            string[] varDescripcion = new string[contador];
            //string[] varMonto = new string[contador];
            decimal[] varMonto = new decimal[contador];
            string[] varOperacion = new string[contador];
            string[] varBanco = new string[contador];
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                dt.Columns.Add("Pago_Fecha", typeof(DateTime));
                dt.Columns.Add("Pago_Descripcion", typeof(string));
                dt.Columns.Add("Pago_Monto", typeof(decimal));
                dt.Columns.Add("Pago_Operacion", typeof(string));
                dt.Columns.Add("Pag_BanId", typeof(string));


                for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
                {
                    dt.Rows.Add(GridView1.Rows[j].Cells[0].Text.Replace("&nbsp;", "nulo"), GridView1.Rows[j].Cells[1].Text.Replace("&nbsp;", "nulo"), Convert.ToDecimal(GridView1.Rows[j].Cells[2].Text), GridView1.Rows[j].Cells[3].Text.Replace("&nbsp;", "nulo"),Session[_namsession_banco].ToString() /*dwBanks.SelectedValue.Replace("&nbsp;", "")*/);

                    varFecha[j - restar2da] = GridView1.Rows[j].Cells[0].Text.Replace("&nbsp;", "nulo");
                    varDescripcion[j - restar2da] = GridView1.Rows[j].Cells[1].Text.Replace("&nbsp;", "nulo");
                    //varMonto[j - restar2da] =  Convert.ToDecimal(GridView1.Rows[j].Cells[2].Text.Replace("&nbsp;", "nulo"));
                    varMonto[j - restar2da] = Convert.ToDecimal(GridView1.Rows[j].Cells[2].Text);
                    varOperacion[j - restar2da] = GridView1.Rows[j].Cells[3].Text.Replace("&nbsp;", "nulo");
                    varBanco[j - restar2da] = dwBanks.SelectedValue.Replace("&nbsp;", "");
                }

                objArray.getSetFecha = varFecha;
                objArray.getSetDescripcion = varDescripcion;
                objArray.getSetMonto = varMonto;
                objArray.getSetOperacion = varOperacion;
                objArray.getSetBanco = varBanco;

                int var_user_ID = System.Convert.ToInt32(_user._usn_userid);
                string _banco = Session[_namsession_banco].ToString();
                objRpta = Documents_Trans.SaveValidateBank(objArray, var_user_ID,dt, _banco);


                if (objRpta.Ok)
                {
                    msnMessage.LoadMessage("Se guardo correctamente. " + objRpta.Mensaje + " Última actualización: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                }
                else
                    msnMessage.LoadMessage("Lamentablemente no se ha guardado el archivo", UserControl.ucMessage.MessageType.Error);
            }
            catch (Exception ex)
            {
                GridView1.Visible = false;
                string msg;
                msg = ex.Message;

                msnMessage.LoadMessage("Se genero el error: " + ex.Message, UserControl.ucMessage.MessageType.Error);
            }

        }

        //protected void Save()
        //{
        //    int contador;
        //    int restarFila = 0;
        //    int restar2da = 0;
        //    //contador=GridView1.Rows.Count -4;
        //    contador = GridView1.Rows.Count;

        //    //string[] varFecha = new string[contador];
        //    //string[] varDescripcion = new string[contador];
        //    //string[] varMonto = new string[contador];
        //    //string[] varOperacion = new string[contador];

        //    string[] varFecha;
        //    string[] varDescripcion;
        //    string[] varMonto;
        //    string[] varOperacion;
        //    string[] varBanco;


        //    try
        //    {
        //        //para saber cuantos registros no se guardaran porque no cumple con necesario
        //        for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
        //        {
        //            if (GridView1.Rows[i].Cells[2].Text != "&nbsp;" && GridView1.Rows[i].Cells[2].Text != "Monto")
        //            {
        //                double negativo = System.Convert.ToDouble(GridView1.Rows[i].Cells[2].Text);

        //                if (negativo < 0)
        //                {
        //                    restarFila = restarFila + 1;
        //                }
        //            }
        //            else { restarFila = restarFila + 1; }
        //        }

        //        varFecha = new string[contador - restarFila];
        //        varDescripcion = new string[contador - restarFila];
        //        varMonto = new string[contador - restarFila];
        //        varOperacion = new string[contador - restarFila];
        //        varBanco = new string[contador - restarFila];

        //        //recorro y guardo en array lo correcto
        //        // for (int i = 4; i <= GridView1.Rows.Count -1; i++)
        //        for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
        //        {
        //            if (GridView1.Rows[j].Cells[2].Text != "&nbsp;" && GridView1.Rows[j].Cells[2].Text != "Monto")
        //            {
        //                double negativo_2 = System.Convert.ToDouble(GridView1.Rows[j].Cells[2].Text);

        //                if (negativo_2 < 0)
        //                {
        //                    restar2da = restar2da + 1;
        //                }
        //                else
        //                {
        //                    varFecha[j - restar2da] = GridView1.Rows[j].Cells[0].Text.Replace("&nbsp;", "nulo");
        //                    varDescripcion[j - restar2da] = GridView1.Rows[j].Cells[1].Text.Replace("&nbsp;", "nulo");
        //                    varMonto[j - restar2da] = GridView1.Rows[j].Cells[2].Text.Replace("&nbsp;", "nulo");
        //                    varOperacion[j - restar2da] = GridView1.Rows[j].Cells[3].Text.Replace("&nbsp;", "nulo");
        //                    varBanco[j - restar2da] = dwBanks.SelectedValue.Replace("&nbsp;", "");
        //                }
        //            }
        //            else { restar2da = restar2da + 1; }
        //        }

        //        BE_Documents_trans objArray = new BE_Documents_trans();
        //        BE_Documents_trans objRpta = new BE_Documents_trans();



        //        objArray.getSetFecha = varFecha;
        //        objArray.getSetDescripcion = varDescripcion;
        //        objArray.getSetMonto = varMonto;
        //        objArray.getSetOperacion = varOperacion;
        //        objArray.getSetBanco = varBanco;

        //        int var_user_ID = System.Convert.ToInt32(_user._usn_userid);
        //        objRpta = Documents_Trans.SaveValidateBank(objArray, var_user_ID);


        //        if (objRpta.Ok)
        //        {
        //            msnMessage.LoadMessage("Se guardo correctamente. " + objRpta.Mensaje + " Última actualización: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
        //        }
        //        else
        //            msnMessage.LoadMessage("Lamentablemente no se ha guardado el archivo", UserControl.ucMessage.MessageType.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        GridView1.Visible = false;
        //        string msg;
        //        msg = ex.Message;

        //        msnMessage.LoadMessage("Se genero el error: " + ex.Message, UserControl.ucMessage.MessageType.Error);
        //    }

        //}


    }
}