using System;
using System.Data;
using System.Web.UI;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Interfaces;
//using Bata.Aquarella.BLL.Interfaces;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll.Logistica;
//using Bata.Aquarella.BLL.Logistica;
using System.IO;
using System.Collections.Generic;


namespace www.aquarella.com.pe.Aquarella.Interface
{
    public partial class impMovimiento : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        string _nameSessionArt = "_ReturnArt";
        string _nameSessionStock = "_Returnstock";
        //Tabla temporal para actuaizar stock
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                if ((_user._usu_tip_id == "02"))
                {
                    Utilities.logout(Page.Session, Page.Response);
                }
                else
                {
                    this.formForEmployee();
                }

                //if (_user._usv_employee)
                //    this.formForEmployee();
                //else
                //    Utilities.logout(Page.Session, Page.Response);
            }
        }

        protected void formForEmployee()
        {
            if (!string.IsNullOrEmpty(_user._usv_warehouse))
            {
                if (!string.IsNullOrEmpty(_user._usv_area))
                {
                    //WareAreaForm.wareAreaForm(_user._usv_co, _user._usv_region);
                    //WareAreaForm.setFormByUser(_user);
                    //WareAreaForm.unableArea();
                }

            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            msnMessage.Visible = false;
            //string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx" };
            string[] validFileTypes = { "TXT","txt"};
            string ext = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break;
                }
            }
            if (!isValidFile)
            {
                lblmensaje.ForeColor = System.Drawing.Color.Red;
                lblmensaje.Text = "Archivo Incorrecto. por favor carcar el archivo con " + string.Join(",", validFileTypes);
                gdarchivo.DataSource = dt;
                gdarchivo.DataBind();
                
            }
            else
            {
                int VFila = 0;
                Stream Lector = fileUpload.PostedFile.InputStream;
                using (StreamReader sr = new StreamReader(Lector))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        
                        string[] Array = linea.Split(Convert.ToChar(";"));
                        //for (int i = 0; i < Array.Length - 1; i++)
                        //{
                        //    lstarchivo.Items.Add(Array[i].ToString());
                        //}
                        //if linea=""
                        SbTemporal(VFila, Array);
                        VFila += 1;
                    }
                    gdarchivo.DataSource = dt;
                    //string cat = dt.Rows[0]["categ"].ToString();                  
                    gdarchivo.DataBind();


                   
                }
                lblmensaje.ForeColor = System.Drawing.Color.Green;
                lblmensaje.Text = "Archivo cargado satisfactoriamente.";

                if (optart.Checked)
                {
                    Session[_nameSessionArt] = dt;
                }
                else if (optmov.Checked)
                {
                    Session[_nameSessionData] = dt;
                }
                else if (optstock.Checked)
                {
                    Session[_nameSessionStock] = dt;
                }
            }
        }

        private void SbTemporal(int Vfila,String[] Array)
        {
            if (Vfila == 0)
            {
                dt = new DataTable();
                for (int i = 0; i < Array.Length; i++)
                {
                    dt.Columns.Add(Array[i].ToString());
                }
            }
            else
            {
                DataRow Vitem = dt.NewRow();
                for (int i = 0; i < Array.Length; i++)
                {
                    //dt.Rows.Add(Array);
                    Vitem[i] = (Array[i].ToString().Trim().Length==0) ? "0" :Array[i].ToString();
                    Vitem[i] = Vitem[i].ToString().Trim();
                }
                dt.Rows.Add(Vitem);
            }
        }

        protected void gdarchivo_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            DataTable dt = (DataTable)Session[_nameSessionData];
            if (dt == null)
            {
                dt = (DataTable)Session[_nameSessionArt];
            }
            if (dt == null)
            {
                dt = (DataTable)Session[_nameSessionStock];
            }

            gdarchivo.PageIndex = e.NewPageIndex;
            gdarchivo.DataSource = dt;
            gdarchivo.DataBind();
        }

        protected void btnenviar_Click(object sender, EventArgs e)
        {
            msnMessage.Visible = false;

            if (optmov.Checked)
            {
                List<Transaction_det> savetransaction = new List<Transaction_det>();
                List<Transaction_det> savetransactionsalida = new List<Transaction_det>();
                DataTable dtstock = (DataTable)Session[_nameSessionData];
                String pares_ent = "";
                String pares_sal = "";

                DataTable dt = new DataTable();

                dt.Columns.Add("Con_Bata", typeof(string));
                dt.Columns.Add("Doc_Bata", typeof(string));
                dt.Columns.Add("Est_Bata", typeof(string));

                dt.Columns.Add("Con_Id", typeof(string));
                dt.Columns.Add("Art_Id", typeof(string));
                dt.Columns.Add("Tal_Id", typeof(string));
                dt.Columns.Add("Cantidad", typeof(Int32));

                foreach (DataRow dr in dtstock.Rows)
                {
                    pares_ent = dr["PARES_ENTRAN"].ToString().Trim();

                    if (pares_ent.Length != 0)
                    {
                        if (Convert.ToInt16(dr["PARES_ENTRAN"]) > 0)
                        {
                            dt.Rows.Add("","","","30", dr["ARTICULO"].ToString(), dr["TALLA"].ToString(), Convert.ToInt16(dr["PARES_ENTRAN"]));
                        }
                    }

                    pares_sal = dr["PARES_SALEN"].ToString().Trim();
                    if (pares_sal.Length != 0)
                    {
                        if (Convert.ToInt16(dr["PARES_SALEN"]) > 0)
                        {
                            dt.Rows.Add("", "", "", "31", dr["ARTICULO"].ToString(), dr["TALLA"].ToString(), Convert.ToInt16(dr["PARES_ENTRAN"]));
                        }
                    }
                }
                DataTable dt_venta=new DataTable();
                string Resultado = www.aquarella.com.pe.bll.Logistica.Transaction_det.ejecuta_transferencia(dt,ref dt_venta);

                if (Resultado.Length ==0)
                {
                    msnMessage.LoadMessage("Se Ingreso los Productos al Stock con Exito...", UserControl.ucMessage.MessageType.Information);
                    gdarchivo.DataSource = dt;
                    gdarchivo.DataBind();
                }
                else
                {
                    msnMessage.LoadMessage(Resultado, UserControl.ucMessage.MessageType.Error);
                    gdarchivo.DataSource = dt;
                    gdarchivo.DataBind();
                }
            }
            else if (optart.Checked)
            {

                DataTable dt =(DataTable)Session[_nameSessionArt];
                if ( dt==null)
                {
                    msnMessage.LoadMessage("No hay Productos por Actualizar", UserControl.ucMessage.MessageType.Error);
                }
                else
                {
                    string Resultado = www.aquarella.com.pe.bll.Logistica.Transaction_det.grabar_articulo(dt);

                    if (Resultado.Length == 0)
                    {
                        msnMessage.LoadMessage("Se Ingreso los Productos con Exito...", UserControl.ucMessage.MessageType.Information);
                    }
                    else
                    {
                        msnMessage.LoadMessage(Resultado, UserControl.ucMessage.MessageType.Error);
                    }
                }

            }
            else if(optstock.Checked) 
            {

                DataTable dt_stk =(DataTable) Session[_nameSessionStock];
                DataRow[] fila = dt_stk.Select("STOCK>=0");
                DataTable dt = new DataTable();

                dt.Columns.Add("Con_Bata", typeof(string));
                dt.Columns.Add("Doc_Bata", typeof(string));
                dt.Columns.Add("Est_Bata", typeof(string));

                dt.Columns.Add("Con_Id", typeof(string));
                dt.Columns.Add("Art_Id", typeof(string));
                dt.Columns.Add("Tal_Id", typeof(string));
                dt.Columns.Add("Cantidad", typeof(Int32));

                foreach (DataRow vrow in fila)
                {
                    dt.Rows.Add("", "", "", "", vrow["COD_ARTIC"].ToString().Trim(), vrow["TALLA"].ToString().Trim(),Convert.ToDecimal(vrow["STOCK"].ToString()));
                }

                string Resultado = www.aquarella.com.pe.bll.Logistica.Transaction_det.actualza_stock(dt );

                if (Resultado.Length ==0)
                {
                    msnMessage.LoadMessage("Se Ingreso los Productos al Stock con Exito...", UserControl.ucMessage.MessageType.Information);
                    gdarchivo.DataSource = dt;
                    gdarchivo.DataBind();
                }
                else
                {
                    msnMessage.LoadMessage(Resultado, UserControl.ucMessage.MessageType.Error);
                    gdarchivo.DataSource = dt;
                    gdarchivo.DataBind();
                }
            }
        }
     
    }
}