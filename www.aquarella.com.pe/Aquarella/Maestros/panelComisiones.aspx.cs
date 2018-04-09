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
    public partial class panelComisiones : System.Web.UI.Page
    {
        private static Users _user;

        private bool respuesta;

        string DSComisiones = "DataSetComisiones";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session.Remove(DSComisiones);

                fill();
            }
        }

        protected void fill()
        {
            DataSet data = Comision.GetAllComisionesDS();
            gridComisiones.DataSource = data.Tables[0];
            gridComisiones.DataBind();
            Session[DSComisiones] = data;
        }
             
        protected void gridComisiones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridComisiones.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[DSComisiones];
            gridComisiones.DataSource = data.Tables[0];
            gridComisiones.DataBind();

        }

        protected void btnSaveComision_Click(object sender, EventArgs e)
        {
            Comision comis = new Comision();
            msnMessage.HideMessage();

            try
            {
                comis.comis_Id = 0;
                comis.comis_Descripcion = txtDescripcion.Text.Trim();
                comis.comis_MontoMin= Convert.ToDecimal(txtMontoMin.Text.Trim());
                comis.comis_MontoMax = Convert.ToDecimal(txtMontoMax.Text.Trim());
                comis.comis_Porcentaje = Convert.ToDecimal(txtComision.Text.Trim());
                comis.comis_FechaIni = txtDateStart.Text.Trim();
                comis.comis_FechaFin = txtDateEnd.Text.Trim();
                comis.comis_usuarioId = Convert.ToInt16(_user._usn_userid);
                comis.comis_Estado = "A";


                respuesta = comis.InsertarComision();

                if (respuesta)
                {
                    msnMessage.LoadMessage("Comision insertada satisfactoriamente", UserControl.ucMessage.MessageType.Information);
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
            txtMontoMin.Text = "";
            txtMontoMax.Text = "";
            txtComision.Text = "";
            txtDateStart.Text = "";
            txtDateEnd.Text = "";

        }

        [WebMethod()]
        public static string ajaxUpdateComision(int comi_id, string comi_Descripcion, string comi_Porcentaje, string comi_MontoMin, string comi_MontoMax, string comi_FechaIni, string comi_FechaFin, string comi_Estado)
        {

            Int16 idUser = Convert.ToInt16(_user._usn_userid);

            bool respuesta = Comision.updateComision(comi_id, comi_Descripcion, comi_Porcentaje, comi_MontoMin, comi_MontoMax, comi_FechaIni, comi_FechaFin, comi_Estado);

            if (respuesta)
            {
                return "1";
       
            }
            else
                return "-1";
        }
    }
}