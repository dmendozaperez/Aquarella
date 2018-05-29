using System;
using System.Data;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;

namespace www.bata.aquarella.com.pe.Aquarella.Maestros
{
    public partial class panelArticulo_App : System.Web.UI.Page
    {
        private Users _user;

        public string _TITULO { get; set; }
        public string _Estado_ID { get; set; }
        public string _Premio_ID { get; set; }
        public decimal _APN_ID { get; set; }

        public bool respuesta;

        string DSArticulos = "DataSetArticulos";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            _Premio_ID = Request.QueryString["PREM_ID"];
             _TITULO = Request.QueryString["PREM_DESCRIPCION"];
            lblTitulo.Text = _TITULO;

            if (!IsPostBack)
            {
                Session.Remove(DSArticulos);
           
                llenarGrilla();

            }
        }

        public void llenarGrilla() {
            DataSet ds = new DataSet();
            ds = Premio.ConsultarPremiosArticulo(_Premio_ID);
            Session[DSArticulos]= ds;
            GridArticulos.DataSource = ds;
            GridArticulos.DataBind();
            
        }              


        protected void GridArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridArticulos.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[DSArticulos];
            GridArticulos.DataSource = data.Tables[0];
            GridArticulos.DataBind();

        }
    }
}