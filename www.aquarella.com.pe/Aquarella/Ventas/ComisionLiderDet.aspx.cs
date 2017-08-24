using System;
using System.Data;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Control;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll.Ventas;
//using Bata.Aquarella.BLL.Ventas;

namespace www.aquarella.com.pe.Aquarella.Ventas
{

    public partial class ComisionLiderDet : System.Web.UI.Page
    {
        protected Users _user;
        string _nameSessionData = "_ReturnData";
        public string _bdv_area_id { get; set; }
        public DateTime _fechainicio { get; set; }
        public DateTime _fechafinal { get; set; }

        int totalpares = 0;
        decimal totalventa = 0;
        decimal totalcomision = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            _bdv_area_id = Request.QueryString["bdv_area_id"];
            _fechainicio = Convert.ToDateTime( Request.QueryString["fechainicio"]);
            _fechafinal = Convert.ToDateTime(Request.QueryString["fechafinal"]);
            lblasesor.Text = Request.QueryString["asesor"] + "(Asesor Comercial)";
            lbllider.Text = Request.QueryString["lider"];
            lbldesde.Text = Convert.ToDateTime(Request.QueryString["fechainicio"]).ToString("dd/MM/yy");
            lblhasta.Text = Convert.ToDateTime(Request.QueryString["fechafinal"]).ToString("dd/MM/yy");


            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                llenarGrilla();
            }
        }

        private void llenarGrilla()
        {

            DataSet ds = Facturacion.sbcomisiondetallada(Convert.ToInt16(_bdv_area_id), _fechainicio, _fechafinal);
            GridFunctions.DataSource = ds;
            GridFunctions.DataBind();

            Session[_nameSessionData] = ds;

            calcular(ds);
        }
        private void calcular(DataSet ds)
        {
            int tpares = 0;
            decimal ttotal = 0;
            decimal tcomision = 0;
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                    
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count ; i++)
                    {
                       
                        tpares += Convert.ToInt16(ds.Tables[0].Rows[i]["Total Pares"].ToString());
                        ttotal += Convert.ToDecimal(ds.Tables[0].Rows[i]["Venta Total"].ToString());
                        tcomision += Convert.ToDecimal(ds.Tables[0].Rows[i]["Comision Lider"].ToString());
                    }
                }
            }
            lbltp.Text = tpares.ToString();
            lbltv.Text = ttotal.ToString("###,##0.00");
            lbltc.Text = tcomision.ToString("###,##0.00");
        }
        protected void GridFunctions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridFunctions.PageIndex = e.NewPageIndex;
            GridFunctions.DataSource = Session[_nameSessionData];
            GridFunctions.DataBind();
            

            
        }

        protected void GridFunctions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblpares = (Label)e.Row.FindControl("lblpares");
                Label lbltotal = (Label)e.Row.FindControl("lbltotal");
                Label lblcomision = (Label)e.Row.FindControl("lblcomision");
                int pares =Convert.ToInt16(lblpares.Text);
                decimal total = Convert.ToDecimal(lbltotal.Text);
                decimal comision = Convert.ToDecimal(lblcomision.Text);
                totalpares += pares;
                totalventa += total;
                totalcomision += comision;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbltpares = (Label)e.Row.FindControl("lbltpares");
                lbltpares.Text = totalpares.ToString();

                Label lbltotalg = (Label)e.Row.FindControl("lbltotalg");
                lbltotalg.Text = totalventa.ToString("###,##0.00");

                Label lblcomisiong = (Label)e.Row.FindControl("lblcomisiong");
                lblcomisiong.Text = totalcomision.ToString("###,##0.00");
            }
        }
    }
}