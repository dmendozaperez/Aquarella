using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.UserControl;

namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class InfoClienteFina : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_DataCliente";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = null;
                //if (_user._usv_employee)
                //    formForEmployee();
                //else Utilities.logout(Page.Session, Page.Response);
            }
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbbuscar();
        }
        private void sbbuscar()
        {
            msnMessage.Visible = false;
            DataSet ds = Payments.getclientedniruc(txtbuscar.Text);
            DataTable dtagregar=new DataTable();
            if (ds.Tables[0].Rows.Count == 0)
            {
                msnMessage.LoadMessage("El N° de Dni o Ruc no existe.", UserControl.ucMessage.MessageType.Error);
                txtbuscar.Focus();
                return;

            }
            if (Session[_nameSessionData] == null)
            {
                dtagregar = ds.Tables[0];
                txtbuscar.Focus();
            }
            else
            {
                DataTable dt = (DataTable)Session[_nameSessionData];
                DataRow[] vfila=dt.Select("rucdni='" + txtbuscar.Text + "'");

                if (vfila.Length == 0)
                {
                    
                    //DataRow vfilaagregar = ds.Tables[0].Rows[0];
                    foreach (DataRow vrow in ds.Tables[0].Rows)
                    {
                        dt.Rows.Add(vrow["rucdni"], vrow["nombres"], vrow["direccion"]);   
                    }
                   
                    dtagregar = dt;
                    txtbuscar.Focus();
                }
                else
                {
                    msnMessage.LoadMessage("Este Dni o Ruc ya esta ingresAQUARELLA en la lista.", UserControl.ucMessage.MessageType.Error);
                    dtagregar = dt;
                    txtbuscar.Focus();
                }
            }
            gvclientes.DataSource =dtagregar;
            gvclientes.DataBind();
            Session[_nameSessionData] = dtagregar;
        }

        protected void gvclientes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            //string str = DataBinder.Eval(e.Row.DataItem, "pin_employee").ToString();
            ImageButton imageButton2 = (ImageButton)e.Row.FindControl("ibanular");
            imageButton2.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de borrar de la lista el cliente con dni o ruc N° : " + DataBinder.Eval(e.Row.DataItem, "rucdni") + " ?')");
        }

        protected void gvclientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("starnular"))
            {
                this.msnMessage.Visible = false;
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _vardniruc = e.CommandArgument.ToString();
              
                {
              
                    try
                    {
              
                    
                        gvclientes.DataSource = fborraritem(_vardniruc);
                        gvclientes.DataBind();

                      
              
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error al borrar de la lista al cliente con dni o ruc N° " + _vardniruc + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }

                }
            }
        }
        protected DataTable fborraritem(string _vardniruc)
        {
            DataTable dt = (DataTable)Session[_nameSessionData];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    if (dt.Rows[i]["rucdni"].ToString()==_vardniruc)
                    {
                        dt.Rows.RemoveAt(i);
                        break;
                    }
                }
            }
            Session[_nameSessionData] = dt;
            return dt;
        }
        #region <exportar excel>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            GridView dw=new GridView();
            DataTable dt =new DataTable();
            DataTable dtgrilla=(DataTable)Session[_nameSessionData];
            dw.DataSource = dtgrilla;
            dw.DataBind();
            //for (int i = 0; dtgrilla.Columns.Count > i; ++i)
            //{
            //    dt.Columns.Add(dtgrilla.Columns[i].ColumnName);
            //}

            ////foreach (DataRow vfila in dtgrilla)

            //dw = gvclientes;

            dw.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref dw);
            dw.DataBind();

            string nameFile = "Clientes";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", dw,true);
        }
        #endregion
    }
}