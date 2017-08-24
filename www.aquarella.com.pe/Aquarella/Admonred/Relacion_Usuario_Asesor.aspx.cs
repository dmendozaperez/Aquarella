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

namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class Relacion_Usuario_Asesor : System.Web.UI.Page
    {
        Users _user;
        DataTable dtasesor;
        DataTable dtlider;
        string _nameSessData_lider_asesor = "session_lider_asesor";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
            if (!IsPostBack)
            {
                cargarasesor();
                cargarlider();
                cargar_lider_asesor();
            }
        }
        private void cargar_lider_asesor()
        {
            DataTable dt = null;
            string _bas_asesor = "";
            msnMessage.HideMessage();
            try
            {
                _bas_asesor = dwasesor.SelectedValue.ToString();
                dt = Asesor.dtleerlider_asesor(Convert.ToDecimal(_bas_asesor));
                Session[_nameSessData_lider_asesor] = dt;
                gvrel_lider.DataSource = dt;
                gvrel_lider.DataBind();
            }
            catch(Exception ex)
            {
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        private void cargarasesor()
        {
            dtasesor = new DataTable();
            dtasesor = Asesor.getasesor();
            dwasesor.Items.Clear();
            ListItem valor = new ListItem();
            valor.Text = "-- Seleccionar un Asesor --";
            valor.Value = "-1";
            dwasesor.Items.Add(valor);
            dwasesor.DataSource = dtasesor;
            dwasesor.DataTextField = "Nombres";
            dwasesor.DataValueField = "Bas_Id";
            dwasesor.DataBind();
        }
        private void cargarlider()
        {
            dtlider = new DataTable();
            dtlider = Asesor.getlider_usuarios(true);
            dwlider.Items.Clear();
            ListItem valor = new ListItem();
            valor.Text = "-- Seleccionar un Lider --";
            valor.Value = "-1";
            dwlider.Items.Add(valor);
            dwlider.DataSource = dtlider;
            dwlider.DataTextField = "Nombres";
            dwlider.DataValueField = "Bas_Id";
            dwlider.DataBind();
        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            Boolean _valida = false;
            try
            {
                string idaseror = dwasesor.SelectedValue.ToString();
                string idlider = dwlider.SelectedValue.ToString();
                string lider_name = dwlider.SelectedItem.Text;
                if (fvalidar()) return;
                _valida = Asesor.update_lider_asesor(1,Convert.ToDecimal(idaseror),Convert.ToDecimal(idlider));
                if (_valida)
                {                     
                    cargarlider();
                    cargar_lider_asesor();
                    msnMessage.LoadMessage("Se agrego correctamente el lider " + lider_name, UserControl.ucMessage.MessageType.Information);
                }
                else
                {
                    msnMessage.LoadMessage("Hubo un error al agregar...", UserControl.ucMessage.MessageType.Error);
                }

            }
            catch(Exception ex)
            {
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        private Boolean fvalidar()
        {
            Boolean _valida = false;
            msnMessage.HideMessage();
            try
            {
                string idaseror = dwasesor.SelectedValue.ToString();
                string idlider = dwlider.SelectedValue.ToString();
                string _mensaje = "";
                if (idaseror=="-1")
                {
                    _mensaje = "Seleccione el Asesor Comercial...";
                    msnMessage.LoadMessage(_mensaje, UserControl.ucMessage.MessageType.Error);
                    _valida = true;
                    dwasesor.Focus();
                    return _valida;
                }
                if (idlider == "-1")
                {
                    _mensaje = "Seleccione el Lider...";
                    msnMessage.LoadMessage(_mensaje, UserControl.ucMessage.MessageType.Error);
                    _valida = true;
                    dwlider.Focus();
                    return _valida;
                }
            }
            catch
            {
                _valida = false;
            }
            return _valida;
        }

        protected void gvrel_lider_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gvrel_lider.PageIndex = e.NewPageIndex;
            gvrel_lider.DataSource = (DataTable)Session[_nameSessData_lider_asesor];

            gvrel_lider.DataBind();
        }

        protected void gvrel_lider_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msnMessage.HideMessage();           

            if (e.CommandName.Equals("starnular"))
            {
                this.msnMessage.HideMessage();
                this.msnMessage.Visible = false;
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _id = e.CommandArgument.ToString();
                //GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string strlider = ((Label)row.FindControl("lbllider")).Text.ToString();
                //string strlider = DataBinder.Eval(row.DataItemIndex, "lider").ToString();
                //string _estid = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Est_Id").ToString();
                
                try
                {
                    Boolean _valida = Asesor.update_lider_asesor(2, 0,Convert.ToDecimal(_id));
                    if (_valida)
                    {
                        cargar_lider_asesor();
                        cargarlider();
                        msnMessage.LoadMessage("Se eliminio de la lista el lider " + strlider, UserControl.ucMessage.MessageType.Information);
                    }
                    else
                    {
                        msnMessage.LoadMessage("Hubo un problema, no se elimino el registro " + strlider, UserControl.ucMessage.MessageType.Information);
                    }

                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
                }

            }
        }

        protected void gvrel_lider_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvrel_lider_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                string strlider = DataBinder.Eval(e.Row.DataItem, "lider").ToString();
                ImageButton imageButton1 = (ImageButton)e.Row.FindControl("ibanular");
                imageButton1.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Eliminar de la lista el lider : -" + strlider + "- ?')");
            }
            catch
            {
            }
        }

        protected void dwasesor_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargar_lider_asesor();
        }

        protected void dwlider_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvrel_lider.DataSource = (DataTable)Session[_nameSessData_lider_asesor];
            gvrel_lider.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvrel_lider);
            gvrel_lider.DataBind();

            string nameFile = "Asesor-Lider";
            Decimal[] _columna_caracter = {0};
            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvrel_lider,false, _columna_caracter);
        }
    }
}