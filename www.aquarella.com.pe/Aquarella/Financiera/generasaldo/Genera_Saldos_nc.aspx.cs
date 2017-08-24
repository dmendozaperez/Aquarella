using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
using System.Web.Services;
//using www.aquarella.com.pe.bll;
namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class Genera_Saldos_nc : System.Web.UI.Page
    {
        Users _user;
        static string _nameSessionData = "_ReturnData";
        static string _nameSessionCorrelativo = "_ReturnCorrelativo";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
               

                 sbconsultar();
            }
        }
        private void sbconsultar()
        {
            DataTable dtview = Documents_Trans.leer_saldo_factura();
            gvReturns.DataSource = dtview;
            gvReturns.DataBind();
            Session[_nameSessionData] = dtview;

            if (dtview.Rows.Count>0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("dniruc", typeof(string));
                dt.Columns.Add("seriedoc", typeof(string));
                dt.Columns.Add("numerofac", typeof(string));
                dt.Columns.Add("numeronc", typeof(string));
                Session[_nameSessionCorrelativo] = dt;
            }
        }

        protected void btfa_Click(object sender, EventArgs e)
        {
            sbactualizargrid();
            sbvisualizagrid();
        }
        private void sbactualizargrid()
        {
            if (!(Session[_nameSessionData] == null))
            {
                DataTable dt = (DataTable)(Session[_nameSessionData]);
                if (dt.Rows.Count > 0)
                {
                    // btnactualizar.Visible = true;
                    string _fecha_fac_all = "";
                    string _fecha_nc_all = "";
                    for (Int32 i = 0; i < dt.Rows.Count; ++i)
                    {
                        TextBox vtxtfac = (TextBox)gvReturns.Rows[i].FindControl("txtfecfac");
                        TextBox vtxtnc = (TextBox)gvReturns.Rows[i].FindControl("txtfecnc");
                        TextBox vtxtmontoutil = (TextBox)gvReturns.Rows[i].FindControl("txtmontoutil");

                        TextBox vtxtseriefac = (TextBox)gvReturns.Rows[i].FindControl("txtseriefac");
                        TextBox vtxtnumerofac = (TextBox)gvReturns.Rows[i].FindControl("txtnumerofac");
                        TextBox vtxtserienc = (TextBox)gvReturns.Rows[i].FindControl("txtserienc");
                        TextBox vtxtnumeronc = (TextBox)gvReturns.Rows[i].FindControl("txtnumeronc");

                        Label vlblpercepcion = (Label)gvReturns.Rows[i].FindControl("lblpercepcion");
                        Label vlblmontofac = (Label)gvReturns.Rows[i].FindControl("lblmontofac");
                        Label vlblmontonc = (Label)gvReturns.Rows[i].FindControl("lblmontonc");

                        CheckBox chk = (CheckBox)gvReturns.Rows[i].FindControl("chkactivar");

                        //verificar si esta activo 
                        if (chk.Checked)
                        {
                            DataTable dt_cor =(DataTable) Session[_nameSessionCorrelativo];

                            if (dt_cor.Rows.Count>0)
                            {
                                Label lbldocumento = (gvReturns.Rows[i].FindControl("lbldocumento") as Label);
                                string _doc = lbldocumento.Text;
                                DataRow[] _fila = dt_cor.Select("dniruc='" + _doc + "'");
                                if (_fila.Length>0)
                                {
                                    vtxtseriefac.Text=_fila[0]["seriedoc"].ToString();
                                    vtxtnumerofac.Text = _fila[0]["numerofac"].ToString();
                                    vtxtserienc.Text=_fila[0]["seriedoc"].ToString();
                                    vtxtnumeronc.Text = _fila[0]["numeronc"].ToString();
                                }

                            }
                        }
                        else
                        {
                            vtxtseriefac.Text = "";
                            vtxtnumerofac.Text = "";
                            vtxtserienc.Text = "";
                            vtxtnumeronc.Text = "";
                        }

                        //******************************


                        if (vtxtmontoutil.Text.Length == 0) vtxtmontoutil.Text = "0";
                        Decimal _montoutil = 0;
                        Decimal.TryParse(vtxtmontoutil.Text, out _montoutil);
                        dt.Rows[i]["monto_util"] = _montoutil;

                        if (i == 0)
                        {
                            _fecha_fac_all = vtxtfac.Text;
                            _fecha_nc_all = vtxtnc.Text;
                        }
                        decimal _percepcion=Convert.ToDecimal((vlblpercepcion.Text)) + 1;
                        decimal _monto_fac=Math.Round((_montoutil)/(_percepcion),2,MidpointRounding.AwayFromZero);
                        string _fec_fac = _fecha_fac_all;
                        string _fec_nc = _fecha_nc_all;
                        //Decimal.TryParse(vtxt.Text, out _precio);
                        dt.Rows[i]["monto_util"] = _montoutil;
                        dt.Rows[i]["fec_fac"] = _fec_fac;
                        dt.Rows[i]["fec_nc"] = _fec_nc;
                        dt.Rows[i]["montofac"] = _monto_fac;
                        dt.Rows[i]["montonc"] = _monto_fac;
                        dt.Rows[i]["seriefac"]=vtxtseriefac.Text;
                        dt.Rows[i]["numerofac"] = vtxtnumerofac.Text;
                        dt.Rows[i]["serienc"] = vtxtserienc.Text;
                        dt.Rows[i]["numeronc"] = vtxtnumeronc.Text;
                        dt.Rows[i]["chk"] = chk.Checked;
                    }
                    (Session[_nameSessionData]) = dt;
                }
                else
                {
                    //btnactualizar.Visible = false;
                }
            }
            else
            {
                //btnactualizar.Visible = false;
            }
        }
        private void sbvisualizagrid()
        {
            if (!(Session[_nameSessionData] == null))
            {
                DataTable dt = (DataTable)(Session[_nameSessionData]);
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dt.Rows.Count; ++i)
                    {
                        
                        TextBox vtxt_fa = (TextBox)gvReturns.Rows[i].FindControl("txtfecfac");
                        TextBox vtxt_nc = (TextBox)gvReturns.Rows[i].FindControl("txtfecnc");
                        TextBox vtxt_montoutil = (TextBox)gvReturns.Rows[i].FindControl("txtmontoutil");

                        TextBox vtxtseriefac = (TextBox)gvReturns.Rows[i].FindControl("txtseriefac");
                        TextBox vtxtnumerofac = (TextBox)gvReturns.Rows[i].FindControl("txtnumerofac");

                        TextBox vtxtserienc = (TextBox)gvReturns.Rows[i].FindControl("txtserienc");
                        TextBox vtxtnumeronc = (TextBox)gvReturns.Rows[i].FindControl("txtnumeronc");

                        Label vlblmontofac=(Label)gvReturns.Rows[i].FindControl("lblmontofac");
                        Label vlblmontonc = (Label)gvReturns.Rows[i].FindControl("lblmontonc");
                        vtxt_fa.Text = dt.Rows[i]["fec_fac"].ToString();
                        vtxt_nc.Text = dt.Rows[i]["fec_nc"].ToString();
                        vtxt_montoutil.Text = dt.Rows[i]["monto_util"].ToString();
                        vlblmontofac.Text = dt.Rows[i]["montofac"].ToString();
                        vlblmontonc.Text = dt.Rows[i]["montonc"].ToString();
                        vtxtseriefac.Text = dt.Rows[i]["seriefac"].ToString();
                        vtxtnumerofac.Text = dt.Rows[i]["numerofac"].ToString();
                        vtxtserienc.Text = dt.Rows[i]["serienc"].ToString();
                        vtxtnumeronc.Text = dt.Rows[i]["numeronc"].ToString();
                    }
                }
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            sbactualizargrid();
            sbvisualizagrid();
        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
               

                e.Row.Cells[4].ColumnSpan = 4;
                e.Row.Cells[4].Text = "<table style='background-color: #CCCCFF;color: #003366 ' border=1 cellpadding=1 cellspacing=1><tr><td colspan=4><b>Factura</b></td></tr><tr><td>&nbspSerie&nbsp&nbsp</td><td>&nbsp&nbsp&nbsp&nbspNumero&nbsp&nbsp&nbsp</td><td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspFecha&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td><td>Monto</td></tr></table>";

              
                //'Agrupando las dos ultimas columnas (col=3 y col=4)               
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                

                e.Row.Cells[5].ColumnSpan = 4;
                e.Row.Cells[5].Text = "<table table style='background-color: #CCCCFF;color: #003366 ' border=1 cellpadding=1 cellspacing=1><tr><td colspan=4><b>Nota de Credito</b></td></tr><tr><td>&nbspSerie&nbsp&nbsp</td><td>&nbsp&nbsp&nbsp&nbspNumero&nbsp&nbsp&nbsp</td><td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspFecha&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td><td>Monto</td></tr></table>";


                //esta opcion es para capturar el opcional validacion para eliminar
                Button btnanular = (Button)e.Row.FindControl("btnanu");
                btnanular.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Anular todos los saldos de clientes seleccionado " + "- ?')");
            }
        }

        protected void chkactivar_CheckedChanged(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            foreach (GridViewRow row in gvReturns.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    TextBox vtxt_montoutil =(row.Cells[3].FindControl("txtmontoutil") as TextBox);
                    TextBox vtxt_seriefac = (row.Cells[3].FindControl("txtseriefac") as TextBox);
                    TextBox vtxt_numerofac = (row.Cells[3].FindControl("txtnumerofac") as TextBox);
                    TextBox vtxt_serienc = (row.Cells[3].FindControl("txtserienc") as TextBox);
                    TextBox vtxt_numeronc = (row.Cells[3].FindControl("txtnumeronc") as TextBox);
                    CheckBox chkrow=(row.Cells[3].FindControl("chkactivar") as CheckBox);
                    
                    vtxt_montoutil.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                    vtxt_seriefac.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                    vtxt_numerofac.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                    vtxt_serienc.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                    vtxt_numeronc.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                 
                    
                    vtxt_montoutil.Enabled =(chkrow.Checked)? true:false;
                    //vtxt_seriefac.Enabled = (chkrow.Checked) ? true : false;
                    //vtxt_numerofac.Enabled = (chkrow.Checked) ? true : false;
                    //vtxt_serienc.Enabled = (chkrow.Checked) ? true : false;
                    //vtxt_numeronc.Enabled = (chkrow.Checked) ? true : false;    
                    vtxt_seriefac.Enabled = (chkrow.Checked) ? false : false;
                    vtxt_numerofac.Enabled = (chkrow.Checked) ? false : false;
                    vtxt_serienc.Enabled = (chkrow.Checked) ? false : false;
                    vtxt_numeronc.Enabled = (chkrow.Checked) ? false : false;

                    //documento del cliente
                    Label lbldocumento = (row.Cells[0].FindControl("lbldocumento") as Label);

                    CheckBox chkanular = (row.Cells[3].FindControl("chkanular") as CheckBox);
                    if  (chkrow.Checked)
                    {
                        row.BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                        row.ForeColor = System.Drawing.ColorTranslator.FromHtml("Black"); 
                        chkanular.Checked = false;
                    }
                        
                    

                    //if (chkrow.Checked)
                    //{
                    //    string _doc_cli = lbldocumento.Text;


                    //    DataTable dt = (DataTable)Session[_nameSessionCorrelativo];
                    //    if (dt.Rows.Count==0 )
                    //    {
                    //        string _serie_ant_doc="";
                    //        string _serie_ant_nc="";
                    //        decimal _numero_ant_doc=0;
                    //        decimal _numero_ant_nc=0;

                    //      // string _error_correlativo=Documents_Trans._correlativo_anticipo(_doc_cli, ref _serie_ant_doc, ref _serie_ant_nc, ref _numero_ant_doc, ref _numero_ant_nc);
                    //        if (_error_correlativo.Length==0)
                    //        {                                                       
                    //            dt.Rows.Add(_serie_ant_doc, _numero_ant_doc, _numero_ant_nc);
                    //            Session[_nameSessionCorrelativo] = dt;
                    //        }
                    //        else
                    //        {
                    //            msnMessage.LoadMessage(_error_correlativo, UserControl.ucMessage.MessageType.Error);
                    //        }
                    //    }
                    //    else
                    //    {


                    //    }
                    //}
                    //else
                    //{

                    //}

                }
            }
            sbactualizargrid();

            //Int32 _fila = gvReturns
            //TextBox vtxt_montoutil = (TextBox)gvReturns.Rows[_fila].FindControl("txtmontoutil");
            //vtxt_montoutil.Enabled = true; 

        }

        private void autogenerar_correlativo()
        {
            try
            {
                DataTable dtcorrelativo = (DataTable)Session[_nameSessionCorrelativo];
                dtcorrelativo.Rows.Clear();
                DataTable dt =(DataTable) (Session[_nameSessionData]);
                if (dt.Rows.Count>0 )
                {
                    
                    Int32 _valdni=0,_valruc = 0;
                    decimal num_bol=0, num_fac=0, num_nc_bol=0, num_nc_fac=0;
                    string serie_bol="", serie_fac = "";
                    foreach (GridViewRow row in gvReturns.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            CheckBox chkrow = (row.Cells[3].FindControl("chkactivar") as CheckBox);
                            if (chkrow.Checked)
                            {
                                Label lbldocumento = (row.Cells[0].FindControl("lbldocumento") as Label);
                                string _doc = lbldocumento.Text;
                                if (_doc.Length==8)
                                {
                                    if (_valdni==0)
                                    {
                                        string _error_correlativo = Documents_Trans._correlativo_anticipo(_doc, ref serie_bol, ref serie_bol, ref num_bol, ref num_nc_bol);
                                        if (_error_correlativo.Length == 0)
                                        {

                                            dtcorrelativo.Rows.Add(_doc, serie_bol, num_bol.ToString().PadLeft(8, '0').ToString(), num_nc_bol.ToString().PadLeft(8, '0').ToString());

                                            Session[_nameSessionCorrelativo] = dtcorrelativo;
                                            num_bol += 1;
                                            num_nc_bol += 1;

                                        }
                                        else
                                        {
                                            msnMessage.LoadMessage(_error_correlativo, UserControl.ucMessage.MessageType.Error);
                                            return;
                                        }
                                        _valdni = 1;
                                    }
                                    else
                                    {
                                        dtcorrelativo.Rows.Add(_doc, serie_bol, num_bol.ToString().PadLeft(8, '0').ToString(), num_nc_bol.ToString().PadLeft(8, '0').ToString());
                                        Session[_nameSessionCorrelativo] = dtcorrelativo;
                                        num_bol += 1;
                                        num_nc_bol += 1;
                                    }
                                }
                                if (_doc.Length == 11)
                                {
                                    if (_valruc==0)
                                    {                                        
                                        string _error_correlativo = Documents_Trans._correlativo_anticipo(_doc, ref serie_fac, ref serie_fac, ref  num_fac, ref num_nc_fac);
                                        if (_error_correlativo.Length == 0)
                                        {
                                            dtcorrelativo.Rows.Add(_doc, serie_fac, num_fac.ToString().PadLeft(8, '0').ToString(), num_nc_fac.ToString().PadLeft(8, '0').ToString());
                                            Session[_nameSessionCorrelativo] = dtcorrelativo;
                                            num_fac += 1;
                                            num_nc_fac += 1;
                                        }
                                        else
                                        {
                                            msnMessage.LoadMessage(_error_correlativo, UserControl.ucMessage.MessageType.Error);
                                            return;
                                        }
                                        _valruc = 1;
                                    }
                                    else
                                    {
                                        dtcorrelativo.Rows.Add(_doc, serie_fac, num_fac.ToString().PadLeft(8, '0').ToString(), num_nc_fac.ToString().PadLeft(8, '0').ToString());
                                        Session[_nameSessionCorrelativo] = dtcorrelativo;
                                        num_fac += 1;
                                        num_nc_fac += 1;                                       
                                    }
                                }

                            }

                        }
                    }
                    
                }
                else
                {
                    msnMessage.LoadMessage("No hay datos para generar correlativos", UserControl.ucMessage.MessageType.Error);
                }

                Session[_nameSessionCorrelativo] = dtcorrelativo;

                //**refrescar data
                sbactualizargrid();
                sbvisualizagrid();
                
            }
            catch(Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }
        }

        protected void btge_Click(object sender, EventArgs e)
        {
            try
            {
                msnMessage.Visible = false;

                DataTable dt_envia = (DataTable)Session["_ReturnData"];
                DataTable dt_genera = new DataTable();
                dt_genera.Columns.Add("bas_id", typeof(Decimal));
                dt_genera.Columns.Add("monto_util", typeof(Decimal));
                dt_genera.Columns.Add("num_fac", typeof(string));
                dt_genera.Columns.Add("fec_fac", typeof(DateTime));
                dt_genera.Columns.Add("num_nc", typeof(string));
                dt_genera.Columns.Add("fec_nc", typeof(DateTime));

                DataRow[] _fila = dt_envia.Select("chk=1 and monto_util>0");

                if (_fila.Length > 0)
                {
                    for (Int32 i = 0; i < _fila.Length; ++i)
                    {
                        decimal _bas_id = Convert.ToDecimal(_fila[i]["bas_id"].ToString());
                        decimal _monto_util = Convert.ToDecimal(_fila[i]["monto_util"].ToString());
                        string _num_fac = _fila[i]["seriefac"].ToString() + _fila[i]["numerofac"].ToString(); ;
                        DateTime _fec_fac = Convert.ToDateTime(_fila[i]["fec_fac"].ToString());
                        string _num_nc = _fila[i]["serienc"].ToString() + _fila[i]["numeronc"].ToString(); ;
                        DateTime _fec_nc = Convert.ToDateTime(_fila[i]["fec_nc"].ToString());

                        dt_genera.Rows.Add(_bas_id, _monto_util, _num_fac, _fec_fac, _num_nc, _fec_nc);
                    }

                    DataTable dt=Documents_Trans.dt_ejecutar_provisiones(dt_genera,_user._bas_id);
                    string script = string.Empty;
                    if (dt==null || dt.Rows.Count==0)
                    {
                        msnMessage.LoadMessage("Error al generar los anticipos", UserControl.ucMessage.MessageType.Error);
                        script = string.Empty;
                        script += "closeDialogLoad()";
                        System.Web.UI.ScriptManager.RegisterStartupScript(upGrid, Page.GetType(), "CloseDialog", script, true);
                        return;
                    }
                    else
                    {
                        for (Int32 i=0;i<dt.Rows.Count;++i)
                        {
                            string tipo = dt.Rows[i]["tipo"].ToString();
                            string _numero = dt.Rows[i]["num_doc"].ToString();
                            string _codigo_hash = "";
                            string _error = "";
                            F_Electronico.ejecutar_factura_electronica(tipo, _numero, ref _codigo_hash, ref _error);
                            if (_error.Length==0)
                            {
                                Documents_Trans.insertar_codigo_hash(_numero, _codigo_hash,(tipo=="B" ||  tipo=="F") ? "V":"N");
                            }
                            else
                            {
                                msnMessage.LoadMessage(_error, UserControl.ucMessage.MessageType.Error);
                                script = string.Empty;
                                script += "closeDialogLoad()";
                                System.Web.UI.ScriptManager.RegisterStartupScript(upGrid, Page.GetType(), "CloseDialog", script, true);
                                return;
                            }


                            ////enviar web service                           
                        }
                    }
                    F_Electronico._enviar_webservice_xml();
                    sbconsultar();
                    msnMessage.LoadMessage("se genero las provisiones de los saldos seleccionados", UserControl.ucMessage.MessageType.Information);
                    script = string.Empty;
                    script += "closeDialogLoad()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upGrid, Page.GetType(), "CloseDialog", script, true);                   
                }
            }
            catch(Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
                string script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upGrid, Page.GetType(), "CloseDialog", script, true);
            }

        }
        //validar datos con el ajax
        [WebMethod()]
        public static string retorna_data()
        {            
            string _retorna = "0";
            DataTable dt =(DataTable) HttpContext.Current.Session[_nameSessionData];
            if (dt == null)
            {
                _retorna = "1";

            }
            else
            {
                if (dt.Rows.Count == 0) _retorna = "1";
                if (dt.Rows.Count > 0)
                {
                    DataRow[] vfila = dt.Select("chk=1");
                    if (vfila.Length == 0) _retorna = "1";

                    DataRow[] vfila_cero = dt.Select("chk=1 and (monto_util=0 or len(seriefac)=0)");

                    if (vfila_cero.Length > 0)
                    {
                        _retorna = "2";
                    }
                }
            }
            return _retorna;
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            msnMessage.Visible = false;
            sbactualizargrid();
            sbvisualizagrid();
        }

        protected void btgecor_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            //***generar hash
            //string _codigo_hash = "";
            //string _error = "";
            //F_Electronico.ejecutar_factura_electronica("N", "1764", ref _codigo_hash, ref _error);
            ////enviar web service
            //F_Electronico._enviar_webservice_xml();
            //*************
            autogenerar_correlativo();

        }

        protected void chkanular_CheckedChanged(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            foreach (GridViewRow row in gvReturns.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                  
                    CheckBox chkanular = (row.Cells[10].FindControl("chkanular") as CheckBox);
                    if (chkanular.Checked)
                    {
                        row.BackColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
                        row.ForeColor = System.Drawing.ColorTranslator.FromHtml("White");
                        CheckBox chkrow = (row.Cells[3].FindControl("chkactivar") as CheckBox);
                        chkrow.Checked = false;

                        TextBox vtxt_montoutil = (row.Cells[3].FindControl("txtmontoutil") as TextBox);
                        TextBox vtxt_seriefac = (row.Cells[3].FindControl("txtseriefac") as TextBox);
                        TextBox vtxt_numerofac = (row.Cells[3].FindControl("txtnumerofac") as TextBox);
                        TextBox vtxt_serienc = (row.Cells[3].FindControl("txtserienc") as TextBox);
                        TextBox vtxt_numeronc = (row.Cells[3].FindControl("txtnumeronc") as TextBox);

                        vtxt_montoutil.Enabled = false;

                        vtxt_seriefac.Text = "";
                        vtxt_numerofac.Text = "";
                        vtxt_serienc.Text = "";
                        vtxt_numeronc.Text = "";

                        vtxt_montoutil.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                        vtxt_seriefac.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                        vtxt_numerofac.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                        vtxt_serienc.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");
                        vtxt_numeronc.BackColor = (chkrow.Checked) ? System.Drawing.ColorTranslator.FromHtml("#99FF99") : System.Drawing.ColorTranslator.FromHtml("");

                    }
                    else
                    {
                        row.BackColor = System.Drawing.ColorTranslator.FromHtml("White");
                        row.ForeColor = System.Drawing.ColorTranslator.FromHtml("Black"); 
                    }

                }
            }            
        }

        protected void btnanu_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session[_nameSessionData];
            if (dt==null || dt.Rows.Count==0)
            {
                msnMessage.LoadMessage("No hay datos para anular...", UserControl.ucMessage.MessageType.Error);
            }
            else
            {
                Boolean _valida_chk = false;
                foreach (GridViewRow row in gvReturns.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {                        
                        CheckBox chkanular = (row.Cells[10].FindControl("chkanular") as CheckBox);
                        if (chkanular.Checked)
                        {
                            _valida_chk = true;
                            break;
                        }

                    }
                  
                }

                if (!_valida_chk)
                {
                    msnMessage.LoadMessage("No ha seleccionado ningun Items...", UserControl.ucMessage.MessageType.Error);
                }
                else
                {
                    string _error = _anular_saldo();
                    if (_error.Length==0)
                    {
                        sbconsultar();
                        msnMessage.LoadMessage("se anulo los items seleccionados...", UserControl.ucMessage.MessageType.Information);                        
                    }
                    else
                    {
                        msnMessage.LoadMessage(_error, UserControl.ucMessage.MessageType.Error);
                    }

                }

            }

           
        }
       private string _anular_saldo()
        {
            string _error = "";
            string _dni_ruc = "";
            Int32 _ingreso = 0;
           try
           {
               foreach (GridViewRow row in gvReturns.Rows)
               {
                   if (row.RowType == DataControlRowType.DataRow)
                   {
                       CheckBox chkanular = (row.Cells[10].FindControl("chkanular") as CheckBox);
                       if (chkanular.Checked)
                       {
                           if (_ingreso==0)
                           {
                               Label lbldocumento = (row.Cells[0].FindControl("lbldocumento") as Label);
                               _dni_ruc += lbldocumento.Text;
                               _ingreso = 1;
                           }
                           else
                           {
                               Label lbldocumento = (row.Cells[0].FindControl("lbldocumento") as Label);
                               _dni_ruc += "," + lbldocumento.Text;
                           }
                           
                       }
                   }
               }

               if (_dni_ruc.Length>0)
               {
                   _error = Documents_Trans._anular_saldo(_dni_ruc,_user._bas_id);
               }

           }
           catch(Exception exc)
           {
               _error = exc.Message;
           }
           return _error;
        }

        

     
    }
}