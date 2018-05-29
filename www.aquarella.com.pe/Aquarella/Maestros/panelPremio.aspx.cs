using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Maestros
{
    public partial class panelPremio : System.Web.UI.Page
    {
        private static Users _user;

        private bool respuesta;
      

        string DSPromociones = "DataSetPremio";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session.Remove(DSPromociones);

                fill();
            }
        }

        protected void fill()
        {
            DataSet data = Premio.GetAllPremiosDS();
           gridPremios.DataSource = data.Tables[0];
            gridPremios.DataBind();
            Session[DSPromociones] = data.Tables[0]; ;
        }
             
        protected void gridPremios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPremios.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[DSPromociones];
            gridPremios.DataSource = data.Tables[0];
            gridPremios.DataBind();

        }

        [WebMethod()]
        public static List<Premio> ListarArticulo(string idPremio)
        {
            try
            {               
                Premio newLineOrder = new Premio();
                List<Premio> order = new List<Premio>();

                DataSet dsArt = Premio.ConsultarPremiosArticulo(idPremio);
                DataTable dt = dsArt.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Premio prem = new Premio();
                    prem.Premio_Articulo = dt.Rows[i]["Referencia"].ToString();
                    prem.Premio_cantidad = Convert.ToDecimal(dt.Rows[i]["Cantidad"]);
                    prem.Premio_talla = dt.Rows[i]["Talla"].ToString();
                    order.Add(prem);
                }
                
                return order;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
             
        protected void gridPremios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msnMessage.HideMessage();
            if (e.CommandName.Equals("EditPremio"))
            {
                //esta session es para modificar
        
                Response.Redirect("EditPremio.aspx?IdPremio=" + e.CommandArgument.ToString());
            }


        }
                

        [WebMethod()]
        public static string ajaxUpdatePromocion(int promo_id, string Ofe_Descripcion, string Ofe_MaxPares, string Ofe_Porc, string FechaIni, string FechaFin)
        {

            Int16 idUser = Convert.ToInt16(_user._usn_userid);

            bool respuesta = Promocion.updatePromocion(promo_id, Ofe_Descripcion, Ofe_MaxPares, Ofe_Porc, FechaIni, FechaFin, idUser);

            if (respuesta)
            {
                return "1";
       
            }
            else
                return "-1";
        }
    }
}