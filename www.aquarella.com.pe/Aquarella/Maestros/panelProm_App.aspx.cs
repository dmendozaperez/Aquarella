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
    public partial class panelProm_App : System.Web.UI.Page
    {
        private Users _user;

        public string _FUV_CO { get; set; }
        public string _Estado_ID { get; set; }
        public int _Promo_ID { get; set; }
        public decimal _APN_ID { get; set; }

        public bool respuesta;

        string DSArticulos = "DataSetArticulos";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            _Promo_ID = Convert.ToInt32(Request.QueryString["PROM_ID"]);
            _Estado_ID = Request.QueryString["ESTADO"];


            if (!IsPostBack)
            {
                Session.Remove(DSArticulos);
                llenarDropDawn();
                llenarGrilla();

                if (_Estado_ID == "I") {
                    btnAdicionar.Visible = false;
                }
            }
        }

        public void llenarGrilla() {
            DataSet ds = new DataSet();
            ds = Promocion.ArticulosXPromocion(_Promo_ID);
            Session[DSArticulos]= ds;
            GridArticulos.DataSource = ds;
            GridArticulos.DataBind();
            
        }

        public void llenarDropDawn() {
            DataTable datos = Promocion.GetAllMarcaDt();
            datos.DefaultView.Sort = "Mar_Descripcion";
            DDMarca.DataSource = datos;
            DDMarca.DataTextField = "Mar_Descripcion";
            DDMarca.DataValueField = "Mar_Id";
            DDMarca.DataBind();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            String strMarcaId = DDMarca.SelectedValue;
            string strArticuloId = dwArticulo.SelectedValue;

            respuesta = Promocion.insertarMarcaArticulo(_Promo_ID, strMarcaId, strArticuloId);

            if (respuesta)
            {
              
                llenarGrilla();
            }
            else
            {
               
            }
        }

        protected void DDMarca_SelectedIndexChanged(object sender, EventArgs e)
        {          

            string  IdMarca = DDMarca.SelectedValue;
            if (!(DDMarca.SelectedValue == "-1"))
            {
                fcombo_Articulo(IdMarca);
            }
            else
            {
                dwArticulo.Items.Clear();
               
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwArticulo.Items.Add(vlista);
                dwArticulo.DataBind();
                
            }
        }

        protected void fcombo_Articulo(string idMarca)
        {
            try
            {
                DataTable dt = Promocion.GetAllArticuloDt(idMarca);
                dwArticulo.Items.Clear();
                dwArticulo.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                
                dwArticulo.Items.Add(vlista);
                if (dt.Rows.Count > 0)
                {
                    ListItem vlista2 = new ListItem();
                    vlista2.Text = "--Todos--";
                    vlista2.Value = "99999T";
                    dwArticulo.Items.Add(vlista2);
                }
                dwArticulo.DataSource = dt;
                dwArticulo.DataBind();
                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        protected void GridArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                string cadena = (e.CommandArgument).ToString();
                string[] parametros = cadena.Split('_');

                respuesta = Promocion.deleteAppArticulo(_Promo_ID, parametros[0], parametros[1]);

                if (respuesta)
                    llenarGrilla();
            }
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