using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class panel_FE : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData", _nameSessionDataFiltered = "InfoLiqSeparatedFilter";
        DataSet _dsResult;
        SortDirection _sortDir;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                cargar_opciones();
                txtDateStart.Text = DateTime.Today.AddDays(-DateTime.Today.Day + 1).ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");
                Session[_nameSessionData] = null;
                if ((_user._usu_tip_id == "01" || _user._usu_tip_id == "02" || _user._usu_tip_id == "03"))
                {
                    dwcliente.Enabled = false;
                    dwcliente.SelectedValue = _user._bas_id.ToString();
                    consultar();
                    //Utilities.logout(Page.Session, Page.Response);
                }
                else
                {
                    consultar();
                    //formForEmployee();
                }

                txtdniruc.Focus();
            }

        }
        private void cargar_opciones()
        {
            DataSet ds = F_Electronico._leer_opciones();

            if (ds!=null)
            {
                DataTable dt_tipo = ds.Tables[0];
                DataTable dt_cliente = ds.Tables[1];

                //clientes
                dwcliente.Items.Clear();
                ListItem valor = new ListItem();
                valor.Text = " -- Seleccionar a todos --";
                valor.Value = "-1";
                dwcliente.Items.Add(valor);
                dwcliente.DataSource = dt_cliente;
                dwcliente.DataTextField = "nombre";
                dwcliente.DataValueField = "bas_id";
                dwcliente.DataBind();

                //documentos tipos
                dwtipo.Items.Clear();
                ListItem valortipo = new ListItem();
                valortipo.Text = " -- Seleccionar a todos --";
                valortipo.Value = "-1";
                dwtipo.Items.Add(valortipo);
                dwtipo.DataSource = dt_tipo;
                dwtipo.DataTextField = "tipo_des";
                dwtipo.DataValueField = "tipo_doc";
                dwtipo.DataBind();
            }
        }
       
        protected void btConsult_Click(object sender, EventArgs e)
        {
            consultar();
        }   
        
        private void consultar()
        {
            //string _cliente = dwcliente.SelectedValue;
            //string _tipo = dwtipo.SelectedValue;
            //DateTime _fechaini =Convert.ToDateTime(txtDateStart.Text);
            //DateTime _fechafin =Convert.ToDateTime(txtDateEnd.Text);
            //string _dniruc_doc = (txtdniruc.Text.Length == 0) ? "-1" : txtdniruc.Text;
            string _cliente = (dwcliente.Enabled) ? "0" : "1";


            odsReturns.SelectParameters[5].DefaultValue = _cliente;
            gvReturns.DataSourceID = odsReturns.ID;
            gvReturns.DataBind();


            if (_cliente=="1")
            {
                gvReturns.Columns[7].Visible = false;
                gvReturns.Columns[8].Visible = false;
                gvReturns.Columns[9].Visible = false;
            }            

            //DataTable dt=F_Electronico._leer_FE(_fechaini, _fechafin, _cliente, _tipo, _dniruc_doc);

            //gvReturns.DataSource = dt;
            //gvReturns.DataBind();
            //Session[_nameSessionData]=dt;
            



        }
        protected void gvReturns_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable)Session[_nameSessionData];

            gvReturns.DataBind();
        }

        protected void gvReturns_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvReturns_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {

        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }        
        
        
        protected void dwcliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultar();
        }

        protected void dwtipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultar();
        }

        protected void odsReturns_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataTable)e.ReturnValue);
                Session[_nameSessionData] = dt;
            }
            catch
            { }
        }
        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }

            set
            {
                ViewState["dirState"] = value;
            }

        }
        protected void gvReturns_Sorting(object sender, GridViewSortEventArgs e)
        {
            //BindData();
            DataTable dt = new DataTable();
            dt =(DataTable) Session[_nameSessionData];
            {
                string SortDir = string.Empty;
                if (dir == SortDirection.Ascending)
                {
                    dir = SortDirection.Descending;
                    SortDir = "Desc";
                }
                else
                {
                    dir = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                //gvReturns.DataSource = sortedView;
                //gvReturns.DataBind();

                //gvReturns.DataSourceID = sortedView.;
                //gvReturns.DataBind();
            }
        }

        protected void gvReturns_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                // Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());

                try
                {
                    string pdf = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "pdf").ToString();
                    if (!string.IsNullOrEmpty(pdf))
                    {
                        //
                       
                        ///
                        Image imgShow = (Image)e.Row.FindControl("imgpdf");
                        ///
                        imgShow.Visible = true;
                    }
                  
                }
                catch
                {
                }
            }
        }

       
    }
}