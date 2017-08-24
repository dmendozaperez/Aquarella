using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Ventas;
namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class reportlider_afiliadoventa : System.Web.UI.Page
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
                //DateTime _fechaactual = DateTime.Now;
                ////DateTime vpri = DateTime.MinValue;
             
                //DateTime fecha_ini;

                //fecha_ini = new DateTime(_fechaactual.Year, _fechaactual.Month + 1, 1).AddMonths(-1);



                ////vpri = vprimerdia.AddDays(1 - Convert.ToDouble(vprimerdia.DayOfWeek));
                ////DateTime Vult = vpri.AddDays(6);

                //txtDateStart.Text = fecha_ini.ToString("dd/MM/yyyy");
                ////txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //txtDateEnd.Text = _fechaactual.ToString("dd/MM/yyyy");
               
            }
        }
        protected void sbconsultar()
        {
            try
            {                
                msnMessage.Visible = false;

                Session[_nameSessionData] = Facturacion._Consulta_Lider_N(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text));

                gvReturns.DataSource =(DataTable) Session[_nameSessionData];

                gvReturns.DataBind();               
                
               
            }
            catch (Exception ex)
            {
                msnMessage.Visible = true;
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);

            }
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataSource =(DataTable) Session[_nameSessionData];
            gvReturns.DataBind();

            string nameFile = "VentaLiderNuevos";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable)Session[_nameSessionData];

            gvReturns.DataBind();
        }

        protected void gvReturns_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvReturns_DataBinding(object sender, EventArgs e)
        {
            //DataControlField xxx = gvReturns.Columns[3];
            //if (xxx is BoundField)
            //{
            //    BoundField boundField = xxx as BoundField;
            //    boundField.DataFormatString = "{N:2}";
            //}
        }
    }
}