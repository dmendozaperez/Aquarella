using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.Bll.Admonred;

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class ventaxzona : System.Web.UI.Page
    {
        Users _user;
        string _nameSessDatavenazonaconsulta = "session_ventazona_consulta";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                cargarasesor();
                cargarlider();
                cargardepartamento();
                cargarprovincia();
                cargarlinea();
                cargarcategoria();
                DateTime Vult = DateTime.Now;
                DateTime fecha1;
                DateTime fechatemp;
                fechatemp = DateTime.Today;
                fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

                txtDateStart.Text = fecha1.Date.ToString("dd/MM/yyyy");
                txtDateEnd.Text = Vult.Date.ToString("dd/MM/yyyy");
                _consultar();
            }
       }
        private void _consultar()
        {
            msnMessage.HideMessage();
            try
            {
                DateTime _fecha_ini =Convert.ToDateTime(txtDateStart.Text);
                DateTime _fecha_fin =Convert.ToDateTime(txtDateEnd.Text);
                string _asesor = dwasesor.SelectedValue;
                string _lider = dwlider.SelectedValue;
                string _dep = dwdepartamento.SelectedValue;
                string _prov = dwprovincia.SelectedValue;
                string _cat = dwcategoria.SelectedValue;
                string _lin = dwlinea.SelectedValue;
                DataTable dt = invoice.getventazonacategoria(_fecha_ini,_fecha_fin,_asesor,_lider,_dep,_prov,_cat,_lin);
                Session[_nameSessDatavenazonaconsulta] = dt;                
                gvReturns.DataSource = dt;
                gvReturns.DataBind();
            }
            catch (Exception ex)
            {
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        private void cargarasesor()
        {            
            dwasesor.DataSource = Asesor.getasesor();
            dwasesor.DataTextField = "Nombres";
            dwasesor.DataValueField = "Bas_Id";
            dwasesor.DataBind();
        }
        protected void cargarlider()
        {                     
            dwlider.DataSource = Area.getAllAreas(_user._asesor);
            dwlider.DataTextField = "Are_Descripcion";
            dwlider.DataValueField = "Are_Id";
            dwlider.DataBind();
        }
        protected void cargardepartamento()
        {
            dwdepartamento.DataSource = Contactenos_Data.departamento_contacto();
            dwdepartamento.DataTextField = "Dep_Descripcion";
            dwdepartamento.DataValueField = "Dep_Id";
            dwdepartamento.DataBind();
        }
        protected void cargarprovincia()
        {
            dwprovincia.DataSource = Basic_Data.getinfoprovincia("-1");
            dwprovincia.DataTextField = "Prv_descripcion";
            dwprovincia.DataValueField = "Prv_id";
            dwprovincia.DataBind();
        }
        protected void cargarcategoria()
        {
            dwcategoria.DataSource = Article.getcategoria();
            dwcategoria.DataTextField = "Cat_Descripcion";
            dwcategoria.DataValueField = "Cat_Id";
            dwcategoria.DataBind();
        }
        protected void cargarlinea()
        {
            dwlinea.DataSource =Stock.getAllcategoria();
            dwlinea.DataTextField = "Cat_Pri_Descripcion";
            dwlinea.DataValueField = "Cat_Pri_Id";
            dwlinea.DataBind();
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            _consultar();
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable)Session[_nameSessDatavenazonaconsulta];
            gvReturns.DataBind();
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.DataSource = (DataTable)Session[_nameSessDatavenazonaconsulta];
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "ventaxzona";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }
    }
}