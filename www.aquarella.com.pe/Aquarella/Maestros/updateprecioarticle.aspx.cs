using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.Bll;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.BLL;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.OleDb;
namespace www.aquarella.com.pe.Aquarella.Maestros
{
    public partial class updateprecioarticle : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = null;                
            }
        }
        private void sbconsultar()
        {
            msnMessage.Visible = false;
            string varticulo = txtarticulo.Text.Trim();

            if (varticulo.Length == 0) 
            {
                msnMessage.LoadMessage("Ingrese el codigo de articulo", UserControl.ucMessage.MessageType.Information);
                return;
            }
            DataSet dsreturn = Article.fgetarticleprecio(txtarticulo.Text);
            DataTable dtdata = new DataTable();
            sbactualizargrid();
            if (dsreturn.Tables[0].Rows.Count == 0)
            {
                msnMessage.LoadMessage("El codigo de articulo no existe ", UserControl.ucMessage.MessageType.Information);
                return;
            }
            else
            {
                if (!(Session[_nameSessionData] == null))
                {
                    if (fvalidaarticulo(varticulo))
                    {
                        msnMessage.LoadMessage("El codigo de articulo ya existe en la lista", UserControl.ucMessage.MessageType.Information);
                        return;
                    }
                    dtdata = (DataTable)(Session[_nameSessionData]);                   
                    string vcodigo = dsreturn.Tables[0].Rows[0]["articulo"].ToString();
                    string vdescripcion = dsreturn.Tables[0].Rows[0]["descripcion"].ToString();
                    Decimal vprecio = Convert.ToDecimal(dsreturn.Tables[0].Rows[0]["precioigv"].ToString());
                    dtdata.Rows.Add(vcodigo, vdescripcion, vprecio, 0);

                }
                else
                {
                    dtdata = dsreturn.Tables[0];
                }
            }


            //ahora validar si este codigo de articulo ya existe en la lista para no repetir.

            Session[_nameSessionData] = dtdata;

            

            gvreturn.DataSource = dtdata;
            gvreturn.DataBind();

            sbvisualizagrid();
            
        }

        private Boolean fvalidaarticulo(string varticulo)
        {
            DataTable dtdata = (DataTable)(Session[_nameSessionData]);
            Boolean vexiste = false;
            if (dtdata.Rows.Count > 0)
            {
                for (Int32 i = 0; i < dtdata.Rows.Count; ++i)
                {
                    string varticulocompara = dtdata.Rows[i]["articulo"].ToString();
                    if (varticulocompara.ToString()==varticulo.ToString())
                    {
                        vexiste = true;
                        break;
                    }
                }
            }
            return vexiste;
        }
        private void sbactualizargrid()
        {
            if (!(Session[_nameSessionData] == null))
            {
                DataTable dt = (DataTable)(Session[_nameSessionData]);
                if (dt.Rows.Count > 0)
                {
                   // btnactualizar.Visible = true;
                    for (Int32 i = 0; i < dt.Rows.Count; ++i)
                    {
                        TextBox vtxt = (TextBox)gvreturn.Rows[i].FindControl("txtprecio");
                        if (vtxt.Text.Length == 0) vtxt.Text = "0";
                        Decimal _precio = 0;
                        Decimal.TryParse(vtxt.Text, out _precio);
                        dt.Rows[i]["precion"] = _precio;


                    }
                }
                else
                {
                    //btnactualizar.Visible = false;
                }
            }
            else
            {
                //btnactualizar.Visible = false;
            }
        }
        private void sbvisualizagrid()
        {
            if (!(Session[_nameSessionData] == null))
            {
                DataTable dt = (DataTable)(Session[_nameSessionData]);
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dt.Rows.Count; ++i)
                    {
                        TextBox vtxt = (TextBox)gvreturn.Rows[i].FindControl("txtprecio");
                        vtxt.Text = dt.Rows[i]["precion"].ToString();
                    }
                }
            }
        }
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }

        protected void gvreturn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("starnular"))
            {
                if (!(Session[_nameSessionData] == null))
                {
                    string varticuloborrar = e.CommandArgument.ToString();
                    DataTable dt = (DataTable)Session[_nameSessionData];
                    if (dt.Rows.Count>0)
                    {
                        for (Int32 i = 0; i < dt.Rows.Count; ++i)
                        {
                            string varticulo = dt.Rows[i]["articulo"].ToString();
                            if (varticulo == varticuloborrar)
                            {
                                dt.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    gvreturn.DataSource = dt;
                    gvreturn.DataBind();
                    Session[_nameSessionData] = dt;
                    sbvisualizagrid();
                }


            }
            if (e.CommandName.Equals("actualizar"))
            {
                msnMessage.Visible = false;
                sbactualizargrid();
                String varticulo = "";
                if (fvdalidaprecio(ref varticulo) == 1)
                {
                    msnMessage.LoadMessage("Error en la actualizacion de precios; El articulo [" + varticulo + "] no puede tener precio de valor cero", UserControl.ucMessage.MessageType.Error);
                    return;
                }

                string[] noactualiza=null;
                DataTable dt = (DataTable)Session[_nameSessionData];
                noactualiza = Article.fupdatearticuloprecio(dt, _user._bas_id,_user._nombre);
                if (!noactualiza[0].Equals("-1"))
                {
                    dt.Rows.Clear();
                    Session[_nameSessionData]=null;
                    gvreturn.DataSource = dt;
                    gvreturn.DataBind();
                    msnMessage.LoadMessage("Los articulos se actualizaron sus precios satisfactoriamente.", UserControl.ucMessage.MessageType.Information);
                }
                else
                {
                    msnMessage.LoadMessage("Error en la actualizacion de precios; intente de nuevo.", UserControl.ucMessage.MessageType.Error);

                }
            }
        }

        private Int32 fvdalidaprecio(ref string varticulo)
        {
            Int32 valor = 0;
            if (!(Session[_nameSessionData] == null))
            {
                DataTable dt = (DataTable)Session[_nameSessionData];
                for (Int32 i = 0; i < dt.Rows.Count; ++i)
                {
                    decimal vprecio = Convert.ToDecimal(dt.Rows[i]["precion"].ToString());
                    
                    if (vprecio == 0)
                    {
                        string vcodigoart = dt.Rows[i]["articulo"].ToString();
                        varticulo = vcodigoart;
                        valor = 1; 
                        break;
                    }
                     
                }
            }
            return valor;
        }

        protected void gvreturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    ImageButton imgactualizar = (ImageButton)e.Row.FindControl("imgactualizar");
                    imgactualizar.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Actualizar los precios -" + "- ?')");
                }
            }

            catch (Exception) 
            {  }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int rpta;
            try
            {
                if (FileUpload1.HasFile)
                {

                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    Random r = new Random();
                    int varAleatorio = r.Next(0, 999);

                    string FilePath = Server.MapPath(FolderPath + FileName + varAleatorio);
                    FileUpload1.SaveAs(FilePath);
                    int val_dwTipArc = 1;
                    rpta = validarArchivo(FilePath, Extension, val_dwTipArc);
                    if (rpta == 1)
                    {
                        Import_To_Grid(FilePath, Extension, val_dwTipArc);
                    }
                    else
                    {
                        msnMessage.LoadMessage("Debe seleccionar un tipo de archivo correcto: ", UserControl.ucMessage.MessageType.Error);
                        gvreturn.DataBind();
                        return;

                    }

                    gvreturn.Visible = true;

                   // btGuardarDatos_2.Enabled = true;
                    File.Delete(FilePath);
                    msnMessage.LoadMessage("Carga correcta del archivo excel. Última actualización: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                }
                else
                {
                    //btGuardarDatos_2.Enabled = false;
                    msnMessage.LoadMessage("Debe seleccionar un tipo de archivo y adjuntarlo: ", UserControl.ucMessage.MessageType.Error);

                }
            }
            catch (Exception ex)
            {
                gvreturn.Visible = false;
                //btGuardarDatos_2.Enabled = false;
                string msg;
                msg = ex.Message;

                msnMessage.LoadMessage("Ocurrio un error: " + ex.Message, UserControl.ucMessage.MessageType.Error);

            }
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
                cmdExcel.CommandText = "SELECT count(*) AS articulo From [" + SheetName + "]";
            }
            //else
            //{
            //    cmdExcel.CommandText = "SELECT count([F3]) AS operacion From [" + SheetName + "] where [F7] = 'Operación - Número' ";
            //}

            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);

            int a = 0;
            foreach (DataRow dr in dt.Rows) //.Tables[0].Rows)
            {
                a = System.Convert.ToInt32(dr["articulo"]);
                if (val_dwTipArc == 1)
                {
                    if (a > 0)
                    {
                        rpta = 1;
                        break;
                    }
                }
                else
                {
                    if (a > 0)
                    {
                        rpta = 1;
                        break;
                    }
                }
            }

            connExcel.Close();

            return rpta;

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
                cmdExcel.CommandText = "SELECT [codigo],[precio] From [" + SheetName + "]";
            }
            //else
            //{
            //    cmdExcel.CommandText = "SELECT [Cuenta],[F3],[F4],[F7] From [" + SheetName + "] where MID([F3], 1, 8) = 'EFECTIVO' ";
            //}

            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);


            for (Int32 i = 0; i < dt.Rows.Count; ++i)
            {               
                string _MONTOstr = dt.Rows[i]["precio"].ToString().Replace(',', '.');

                dt.Rows[i]["precio"] = formato_numerico(_MONTOstr);                                      
            }

            //CultureInfo PE = new CultureInfo("es-PE");
                       
            connExcel.Close();

            if (dt.Rows.Count > 0)
            {
                DataTable dt_import = new DataTable();
                dt_import.Columns.Add("Art_Id", typeof(string));
                dt_import.Columns.Add("Precio_Igv", typeof(decimal));


                for (Int32 i = 0; i<dt.Rows.Count; ++i)
                {
                    dt_import.Rows.Add(dt.Rows[i][0].ToString(),Convert.ToDecimal(dt.Rows[i][1].ToString()));
                }

                    //foreach (DataRow fila in dt.Rows)
                    //{
                    //    dt_import.ImportRow(fila);
                    //}

                

                DataSet dsreturn = Article.lista_articulo_precio(dt_import);
                Session[_nameSessionData] = dsreturn.Tables[0];
                gvreturn.DataSource = dsreturn.Tables[0];
                gvreturn.DataBind();
                sbvisualizagrid();
                sbactualizargrid();
                
            }

            //GridView1.Caption = Path.GetFileName(FilePath);
            //GridView1.DataSource = dt;
            //GridView1.DataBind();

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
                monto = Convert.ToDecimal(_numero_str);
            }
            else
            {
                _valor = _valor.Replace(',', '.');
                monto = Convert.ToDecimal(_valor);
            }

            return monto;
        }
      
    }
}