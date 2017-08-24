using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Logistica;
using System.Data;
using System.Web.Services;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class Manifiesto : System.Web.UI.Page
    {
        Users _user;
        string _nameSessDatamanifiesto = "session_mani";
        string _pageManReport = "panelFramesManReport.aspx";
        private static string _estado { set; get; }// { return HttpContext.Current.Session["estado"].ToString(); }; }
        private string _idmanifiesto { get; set; } //{ return this.Request.Params["noManifiesto"] != null ? ((object)this.Request.Params["noManifiesto"]).ToString() : string.Empty; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            _estado = HttpContext.Current.Session["estado"].ToString();
            _idmanifiesto= this.Request.Params["noManifiesto"] != null ? ((object)this.Request.Params["noManifiesto"]).ToString() : string.Empty;
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

             

                if (_estado=="1")
                { 
                    crear_temporal();
                    _estadonuevo();
                }
                else
                {
                    _estado_modifica();
                }
                txtdocumento.Focus();
            }
        }
        private void _estado_modifica()
        {
            try
            {
                _idmanifiesto = this.Request.Params["noManifiesto"] != null ? ((object)this.Request.Params["noManifiesto"]).ToString() : string.Empty;
                Decimal _var_idmanifiesto =Convert.ToDecimal(_idmanifiesto);
                lblnumero.Text = _var_idmanifiesto.ToString();
                lblestado.Text = "Modificando";
                DataTable dt = ManifiestoBll._consulta_modifica_manifiesto(_var_idmanifiesto);
                Session[_nameSessDatamanifiesto] = dt;
                gvmanifiesto.DataSource = dt;
                gvmanifiesto.DataBind();
            }
            catch(Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        private void _estadonuevo()
        {
            msnMessage.HideMessage();
            try
            {
                lblnumero.Text = (ManifiestoBll._correlativo_manifiesto()).ToString();
                lblestado.Text = "Nuevo";
            }
            catch(Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        private void limpiar()
        {
            crear_temporal();
            HttpContext.Current.Session["estado"] = "0";
        }
        private void crear_temporal()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("guia", typeof(String));
            dt.Columns.Add("doc", typeof(String));
            dt.Columns.Add("lider", typeof(String));
            dt.Columns.Add("promotor", typeof(String));
            dt.Columns.Add("pares", typeof(decimal));
            dt.Columns.Add("agencia", typeof(String));
            dt.Columns.Add("destino", typeof(String));
            dt.Columns.Add("items", typeof(Decimal));
            dt.TableName = "detalle";

            Session[_nameSessDatamanifiesto] = dt;

            gvmanifiesto.DataSource = dt;
            gvmanifiesto.DataBind();
        }

        protected void btnagregar_Click(object sender, EventArgs e)
        {
            _agregar();
        }
        private void _agregar()
        {
            msnMessage.HideMessage();
            
            string _mensaje = "";
            DataTable dtagregar = null;
            if (_fvalidar(ref _mensaje,ref dtagregar)==1)
            {
                msnMessage.LoadMessage(_mensaje, UserControl.ucMessage.MessageType.Error);
                return;
            }

            DataTable dt = (DataTable)Session[_nameSessDatamanifiesto];
            dtagregar.TableName = "detalle";
            foreach (DataRow datRow in dtagregar.Rows)
            {
                    dt.ImportRow(datRow);
            }

            _calcular_items(dt);

            txtdocumento.Text = "";
            Session[_nameSessDatamanifiesto] = dt;
            gvmanifiesto.DataSource = dt;
            gvmanifiesto.DataBind();
            txtdocumento.Focus();

        }
        //actualizando numero de items
        private void _calcular_items(DataTable dt)
        {
            for (Int32 i=0;i<dt.Rows.Count;++i)
            {
                dt.Rows[i]["items"] = i + 1;
            }
        }
        private Int32 _fvalidar(ref string _mensaje,ref DataTable dtagregar)
        {
            Int32 _existe = 0;
            string _doc = txtdocumento.Text.Trim().ToString();
            try
            {
                _estado=HttpContext.Current.Session["estado"].ToString();
                if (_doc.Length>0)
                { 
                    DataTable dt = (DataTable)Session[_nameSessDatamanifiesto];

                    if (dt != null)
                    {
                        DataRow[] _fila = dt.Select("doc='" + _doc + "'");
                        if (_fila.Length>0)
                        {
                            _existe = 1;
                            _mensaje = "El numero de Documento existe en la lista...";
                        }
                        else
                        {
                            DataSet ds = ManifiestoBll.Manifiesto_AgregarXDoc(_doc);                            
                            if (ds!=null)
                            {
                                DataTable dtvalida = ds.Tables[0];
                                if (dtvalida != null)
                                {
                                    string _estado = dtvalida.Rows[0]["estado"].ToString();
                                    _mensaje = dtvalida.Rows[0]["descripcion"].ToString();
                                    switch (_estado)
                                    {
                                        case "0":
                                            dtagregar = ds.Tables[1];
                                            break;
                                        case "1":
                                            _existe = 1;                                            
                                            break;
                                        case "-1":
                                            _existe = 1;                                            
                                            break;
                                    }
                                    
                                }
                            }    

                        }
                    }
                }
                else
                {
                    _existe = 1;
                    _mensaje = "Ingrese el numero de documento a buscar...";
                }
            }
            catch(Exception exc)
            {
                _existe = 1;
                _mensaje = exc.Message;
            }
            return _existe;
        }

        protected void gvmanifiesto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName.Equals("starnular"))
            {
                this.msnMessage.HideMessage();
                this.msnMessage.Visible = false;
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _nodoc = e.CommandArgument.ToString();


                try
                {

                    DataTable dt =(DataTable) Session[_nameSessDatamanifiesto];
                    DataRow[] vfila = dt.Select("doc='" + _nodoc + "'");
                    dt.Rows.Remove(vfila[0]);
                    _calcular_items(dt);
                    Session[_nameSessDatamanifiesto] = dt;
                    gvmanifiesto.DataSource = dt;
                    gvmanifiesto.DataBind();                    
                }
                catch (Exception ex)
                {                    
                    msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
                }

            }
        }

        protected void gvmanifiesto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                string str = DataBinder.Eval(e.Row.DataItem, "doc").ToString();
                ImageButton imageButton1 = (ImageButton)e.Row.FindControl("ibanular");
                imageButton1.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Eliminar el documento número : -" + DataBinder.Eval(e.Row.DataItem, "doc") + "- ?')");              
            }
            catch
            {
            }
        }

        protected void gvmanifiesto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvmanifiesto.PageIndex = e.NewPageIndex;
            gvmanifiesto.DataSource = (DataTable)Session[_nameSessDatamanifiesto];

            gvmanifiesto.DataBind();
        }

        protected void btcrearmanifiesto_Click(object sender, EventArgs e)
        {
              generar_manifiesto();
        }

        private void generar_manifiesto()
        {
            string script = string.Empty;
            try
            {
                _idmanifiesto = this.Request.Params["noManifiesto"] != null ? ((object)this.Request.Params["noManifiesto"]).ToString() : string.Empty;
                _estado = HttpContext.Current.Session["estado"].ToString();
                msnMessage.Visible = false;
                DataTable dt = (DataTable)Session[_nameSessDatamanifiesto];
                if (dt.Rows.Count == 0)
                {
                    msnMessage.LoadMessage("No hay detalles para generar manifiesto", UserControl.ucMessage.MessageType.Error);
                    script = string.Empty;
                    script += "closeDialogLoad()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upmanifiesto, Page.GetType(), "CloseDialog", script, true);
                    return;
                }
                else
                {
                    DataTable dtman = new DataTable();
                    dtman.Columns.Add("Man_Det_VenID",typeof(String));
                    dtman.Columns.Add("Man_Det_Items", typeof(Decimal));
                    dtman.Columns.Add("Man_Det_Lider",typeof(string));
                    dtman.Columns.Add("Man_Det_Promotor",typeof(string));
                    dtman.Columns.Add("Man_Det_Agencia",typeof(string));
                    dtman.Columns.Add("Man_Det_Destino",typeof(string));


                   

                    for (Int32 i=0;i<dt.Rows.Count;++i)
                    {
                        string _doc = dt.Rows[i]["doc"].ToString();
                        string _lider = dt.Rows[i]["lider"].ToString();
                        string _promotor = dt.Rows[i]["promotor"].ToString();
                        string _agencia = dt.Rows[i]["agencia"].ToString();
                        string _destino = dt.Rows[i]["destino"].ToString();
                        decimal _items =Convert.ToDecimal(dt.Rows[i]["items"]);
                        dtman.Rows.Add(_doc, _items, _lider, _promotor, _agencia, _destino);
                    }

                    if (_estado=="1")
                    { 
                    //verificar si es que no estan grabando e varias sessiones el documento
                            string _mensaje = "";
                        decimal _valida_registro = ManifiestoBll._valida_manifiesto(dtman, ref _mensaje);

                        if (_valida_registro==1)
                        {
                            msnMessage.LoadMessage(_mensaje, UserControl.ucMessage.MessageType.Error);
                            script = string.Empty;
                            script += "closeDialogLoad()";
                            System.Web.UI.ScriptManager.RegisterStartupScript(upmanifiesto, Page.GetType(), "CloseDialog", script, true);
                            return;
                        }
                    }
                    //


                    Decimal _id = 0;

                    if (!(ManifiestoBll.actualizar_manifiesto(Convert.ToInt32(_estado),Convert.ToDecimal((_idmanifiesto.Length==0)?"0": _idmanifiesto), _user._bas_id, dtman,ref _id)))
                    {
                        msnMessage.LoadMessage("Hubo un problerma con la actualizacion, por favor consulte con sistemas", UserControl.ucMessage.MessageType.Error);
                        script = string.Empty;
                        script += "closeDialogLoad()";
                        System.Web.UI.ScriptManager.RegisterStartupScript(upmanifiesto, Page.GetType(), "CloseDialog", script, true);
                    }
                    else
                    {

                        limpiar();
                        string url = _pageManReport  + "?noMan=" +  ((_estado=="1")? _id.ToString() : _idmanifiesto.ToString());//"?NoOrder=" + noOrder + "&TypeReport=2";
                                                                               //
                        Response.Redirect(url);
                      
                    }

                }
            }
            catch (Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
                script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upmanifiesto, Page.GetType(), "CloseDialog", script, true);
            }
        }

        protected void btExit_Click(object sender, EventArgs e)
        {
            //limpiar();
            Response.Redirect("PanelManifiesto.aspx");
        }       
    }
}