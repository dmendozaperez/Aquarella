using System;
using System.Data;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;

using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class panelRotulo_App : System.Web.UI.Page
    {
        private Users _user;

        

        public string _Descripcion { get; set; }
        public string _Estado_ID { get; set; }
        public string _IdLider { get; set; }
        public decimal _APN_ID { get; set; }

        public bool respuesta;

        string DSArticulos = "DataSetArticulos";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            //_IdLider = "409";//Request.QueryString["LIDER_ID"];
            //_Descripcion = "seleccionar";// Request.QueryString["DESCRIPCION"];
            _IdLider = Request.QueryString["LIDER_ID"];
            _Descripcion = Request.QueryString["DESCRIPCION"];
            TxtLider.Text = _Descripcion;
            llenarGrilla();



            if (!IsPostBack)
            {
                Session.Remove(DSArticulos);
                             
            }
        }

        public void llenarGrilla() {
            DataSet ds = new DataSet();
            string idLider = _IdLider;
            string Descripcion = txtDescripcion.Text;

           ds = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.getRotulo(idLider, Descripcion);
            Session[DSArticulos]= ds;
            GridRotulos.DataSource = ds;
            GridRotulos.DataBind();
                      
        }
        

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrilla();
        }

        protected void txtArticulo_TextChanged(object sender, EventArgs e)
        {
            //string articulo = txtArticulo.Text;
        }

      
      

        protected void GridRotulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string sss = "";
             
            }
        }

        protected void GridRotulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridRotulos.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[DSArticulos];
            GridRotulos.DataSource = data.Tables[0];
            GridRotulos.DataBind();

        }
    }
}