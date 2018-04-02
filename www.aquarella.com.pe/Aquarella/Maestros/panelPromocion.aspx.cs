using System;
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
    public partial class panelPromocion : System.Web.UI.Page
    {
        private Users _user;

        private bool respuesta;

        string DSPromociones = "DataSetPromociones";

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
            DataSet data = Promocion.GetAllPromocionesDS();
            gridFunctions.DataSource = data.Tables[0];
            gridFunctions.DataBind();
            Session[DSPromociones] = data;
        }

        protected void gridFunctions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridFunctions.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[DSPromociones];
           gridFunctions.DataSource = data.Tables[0];
            gridFunctions.DataBind();

        }

        protected void btnSavePromocion_Click(object sender, EventArgs e)
        {
            Promocion prm = new Promocion();
            msnMessage.HideMessage();

            try
            {
                prm.promo_Id = 0;
                prm.promo_Descripcion = txtDescripcion.Text.Trim();
                prm.promo_Porcentaje= Convert.ToDecimal(txtPorc.Text.Trim());
                prm.promo_Max_pares = Convert.ToInt16(txtPares.Text.Trim());
                prm.promo_FechaIni = txtDateStart.Text.Trim();
                prm.promo_FechaFin = txtDateEnd.Text.Trim();
                prm.promo_usuarioId = Convert.ToInt16(_user._usn_userid);



                respuesta = prm.InsertarPromocion();

                if (respuesta)
                {
                    msnMessage.LoadMessage("Promocion insertada satisfactoriamente", UserControl.ucMessage.MessageType.Information);
                    eraseFields();
                    fill();
                }
                else
                    msnMessage.LoadMessage("Ocurrio un problema durante la insercion", UserControl.ucMessage.MessageType.Error);

            }
            catch (Exception) { msnMessage.LoadMessage("Ocurrio un problema durante la insercion", UserControl.ucMessage.MessageType.Error); }
        }

        public void eraseFields()
        {
            txtDescripcion.Text = "";
            txtPorc.Text = "";
            txtPares.Text = "";
            txtDateStart.Text = "";
            txtDateEnd.Text = "";

        }

        [WebMethod()]
        public static string ajaxUpdatePromocion(int promo_id, string Ofe_Descripcion, string Ofe_MaxPares, string Ofe_Porc, string FechaIni, string FechaFin)
        {
            
            bool respuesta = Promocion.updatePromocion(promo_id, Ofe_Descripcion, Ofe_MaxPares, Ofe_Porc, FechaIni, FechaFin);

            if (respuesta)
            {
                return "1";
            }
            else
                return "-1";
        }
    }
}