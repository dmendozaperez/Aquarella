using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.Bll.Control;

namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class Catalogo : System.Web.UI.Page
    {
        Users _user;
        string _pageCatReturn = "Catalogo_Config.aspx";
        private static string _estado { get { return HttpContext.Current.Session["estado"].ToString(); } }
        private string _idcatalogo { get { return this.Request.Params["noCatalogo"] != null ? ((object)this.Request.Params["noCatalogo"]).ToString() : string.Empty; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (_estado == "0")
            {
                Response.Redirect("PanelManifiesto.aspx");
                return;
            }
            if (!IsPostBack)
            {



                if (_estado == "1")
                {
                    //crear_temporal();
                    _estadonuevo();
                }
                else
                {
                    //_estado_modifica();
                }
                txtdescripcion.Focus();
                
            }
        }
        private void _estadonuevo()
        {
            msnMessage.HideMessage();
            try
            {
                lblnumero.Text = (CatalogoClass._correlativo_catalogo()).ToString();
                lblestado.Text = "Nuevo";
            }
            catch (Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        protected void btcrearmanifiesto_Click(object sender, EventArgs e)
        {
            generar_catalogo();
        }
        private Boolean _fvalida()
        {            
            msnMessage.Visible = false;
            string _descripcion = txtdescripcion.Text.Trim();
            string _header_title = txtheadertitle.Text.Trim();
            decimal _nropagina = 0;
            Decimal.TryParse(txtnropagina.Text,out _nropagina);

            if (_descripcion.Length==0)
            {                
                msnMessage.LoadMessage("Ingrese la descripcion", UserControl.ucMessage.MessageType.Error);
                return false;
            }
            if (_header_title.Length == 0)
            {
                msnMessage.LoadMessage("Ingrese el header", UserControl.ucMessage.MessageType.Error);
                return false;
            }
            if (_nropagina == 0)
            {
                msnMessage.LoadMessage("Ingrese el Nro de Paginas", UserControl.ucMessage.MessageType.Error);
                return false;
            }
            return true;
        }
        private void generar_catalogo()
        {
            string script = string.Empty;
            try
            {
                string _descripcion = txtdescripcion.Text.Trim();
                string _header_title =txtheadertitle.Text.Trim();
                string _nropagina = txtnropagina.Text;

                msnMessage.Visible = false;
                //DataTable dt = (DataTable)Session[_nameSessDatamanifiesto];
                //if (dt.Rows.Count == 0)
                //{
                //    msnMessage.LoadMessage("No hay detalles para generar manifiesto", UserControl.ucMessage.MessageType.Error);
                //    script = string.Empty;
                //    script += "closeDialogLoad()";
                //    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                //    return;
                //}
                //else
                //{
                    //DataTable dtman = new DataTable();
                    //dtman.Columns.Add("Man_Det_VenID", typeof(String));
                    //dtman.Columns.Add("Man_Det_Items", typeof(Decimal));
                    //dtman.Columns.Add("Man_Det_Lider", typeof(string));
                    //dtman.Columns.Add("Man_Det_Promotor", typeof(string));
                    //dtman.Columns.Add("Man_Det_Agencia", typeof(string));
                    //dtman.Columns.Add("Man_Det_Destino", typeof(string));




                    //for (Int32 i = 0; i < dt.Rows.Count; ++i)
                    //{
                    //    string _doc = dt.Rows[i]["doc"].ToString();
                    //    string _lider = dt.Rows[i]["lider"].ToString();
                    //    string _promotor = dt.Rows[i]["promotor"].ToString();
                    //    string _agencia = dt.Rows[i]["agencia"].ToString();
                    //    string _destino = dt.Rows[i]["destino"].ToString();
                    //    decimal _items = Convert.ToDecimal(dt.Rows[i]["items"]);
                    //    dtman.Rows.Add(_doc, _items, _lider, _promotor, _agencia, _destino);
                    //}

                    if (_estado == "1")
                    {
                        //verificar si es que no estan grabando e varias sessiones el documento
                        //string _mensaje = "";
                        //decimal _valida_registro = ManifiestoBll._valida_manifiesto(dtman, ref _mensaje);

                        //if (_valida_registro == 1)
                        //{
                        //    msnMessage.LoadMessage(_mensaje, UserControl.ucMessage.MessageType.Error);
                        //    script = string.Empty;
                        //    script += "closeDialogLoad()";
                        //    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                        //    return;
                        //}
                    }
                    //


                    Decimal _id = 0;

                    if (!(CatalogoClass.actualizar_catalogo(Convert.ToInt32(_estado), Convert.ToDecimal((_idcatalogo.Length == 0) ? "0" : _idcatalogo),_descripcion,_header_title,Convert.ToDecimal(_nropagina), ref _id)))
                    {
                        msnMessage.LoadMessage("Hubo un problerma con la actualizacion, por favor consulte con sistemas", UserControl.ucMessage.MessageType.Error);
                        script = string.Empty;
                        script += "closeDialogLoad()";
                        System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                    }
                    else
                    {

                        limpiar();
                    string url = _pageCatReturn;
                                                                                                                                 //
                        Response.Redirect(url);

                    }

                //}
            }
            catch (Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
                script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
            }
        }
        private void limpiar()
        {           
            HttpContext.Current.Session["estado"] = "0";
        }
        protected void btExit_Click(object sender, EventArgs e)
        {

        }
    }
}