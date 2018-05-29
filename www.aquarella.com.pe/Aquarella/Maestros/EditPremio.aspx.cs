using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Maestros
{
    public partial class EditPremio : System.Web.UI.Page
    {
        private static Users _user;
        private string _idpremio { get; set; }


        string DSArticuloPremio = "DataSetPremio";
        string _nameSessionData = "ItemsStock_";

        protected void Page_Load(object sender, EventArgs e)
        {
            _idpremio = this.Request.Params["IdPremio"] != null ? ((object)this.Request.Params["IdPremio"]).ToString() : string.Empty;
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session.Remove(DSArticuloPremio);

                CargarArticulosPremio();
                initGrid("csdfraesdf141222");
            }
        }

      
        private void CargarArticulosPremio()
        {
            DataSet dsPrem = Premio.getPremiosArticulo(_idpremio);
            DataTable dtArticulosPrem = dsPrem.Tables[0];
            DataTable dttitulo = dsPrem.Tables[1];
            DataRow row = dttitulo.Rows[0];
            string nombre = row["Regalo"].ToString();
            lbltitulo.Text = nombre;
            this.Session[DSArticuloPremio] = dsPrem;
            gridPremios.DataSource = dtArticulosPrem;
            gridPremios.DataBind();

        }

        protected void gridPremios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPremios.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[DSArticuloPremio];
            gridPremios.DataSource = data.Tables[0];
            gridPremios.DataBind();

        }

        protected void gvStock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStock.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[_nameSessionData];
            gvStock.DataSource = data.Tables[0];
            gvStock.DataBind();

        }


        protected void gridPremios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msnMessage.HideMessage();
            if (e.CommandName.Equals("EditPremio"))
            {
                //esta session es para modificar
        
                Response.Redirect("UpdatePremio.aspx?IdPremio=" + e.CommandArgument.ToString());
            }

            if (e.CommandName.Equals("Eliminar"))
            {
                this.msnMessage.HideMessage();
                this.msnMessage.Visible = false;
                string  strIdDetalle = e.CommandArgument.ToString();
              
                try
                {

                     Boolean _valida = Premio.EliminarArticuloPremio(_user._bas_id, strIdDetalle);
                    if (_valida)
                    {
                        CargarArticulosPremio();
                        msnMessage.LoadMessage("Se Elimino el Articulo seleccionado.", UserControl.ucMessage.MessageType.Information);
                    }
                    else
                    {
                        msnMessage.LoadMessage("Hubo un problema, no se Anulo el Articulo seleccionado. ", UserControl.ucMessage.MessageType.Information);
                    }
                    
                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
                }

            }


        }

        protected void gvPremios_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                /// Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());

                try
                {
                    //
                    string referencia = DataBinder.Eval(e.Row.DataItem, "referencia").ToString();
                    //
                    string nameArticle = DataBinder.Eval(e.Row.DataItem, "articulo").ToString();
                    //
                    Label lblPhotoArticle = (Label)e.Row.FindControl("lblPhotoArticle");

                    string pAcc = (_user._usv_employee) ? "F" : "T";

                    //
                    lblPhotoArticle.Text = "<a class='iframe' href='../Maestros/informationarticle.aspx?elcitra=" + referencia + "&isForPublicAcces=" + pAcc + "' title='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "'>" +
                        "<img src='../../Design/images/Botones/photo.png' border='0' alt='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "' />" +
                        "</a>";
                }
                catch
                {
                }
            }
        }

        protected void gvStock_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                /// Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());

                try
                {
                    //
                    string referencia = DataBinder.Eval(e.Row.DataItem, "referencia").ToString();
                    //
                    string nameArticle = DataBinder.Eval(e.Row.DataItem, "articulo").ToString();
                    //
                    Label lblPhotoArticle = (Label)e.Row.FindControl("lblPhotoArticle");

                    string pAcc = (_user._usv_employee) ? "F" : "T";

                    //
                    lblPhotoArticle.Text = "<a class='iframe' href='../Maestros/informationarticle.aspx?elcitra=" + referencia + "&isForPublicAcces=" + pAcc + "' title='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "'>" +
                        "<img src='../../Design/images/Botones/photo.png' border='0' alt='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "' />" +
                        "</a>";
                }
                catch
                {
                }
            }
        }

        protected void gridPremios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;

                ImageButton imageButton1 = (ImageButton)e.Row.FindControl("ibEliminar");
                imageButton1.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de eliminar el Articulo ?')");
            }
            catch
            {
            }
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            this.msnMessage.Visible = false;
            initGrid(TxtItem.Text.Trim());

        }

        private void initGrid(string item)
        {
            DataSet ds = Premio.getPremiosArticuloStock(item, _user._usu_tip_id);
            DataTable dtArticulosStk = ds.Tables[0];
            this.Session[this._nameSessionData] = ds;
            this.gvStock.DataSource = dtArticulosStk;
            this.gvStock.DataBind();

        }
        protected void btListar_Click(object sender, EventArgs e)
        {
            Response.Redirect("panelPremio.aspx");
        }

        protected void btAgregar_Click(object sender, EventArgs e)
        {
         
            string strIdPremio = _idpremio;
            string strDataDetalle = "";
            string listExistentes = "";
            string strErrorCantidad = "";

            DataSet data = (DataSet)Session[DSArticuloPremio];
            DataTable dt = data.Tables[0];

            int intIdDespacho = Convert.ToInt32(_idpremio);

            for (int i = 0; i <= gvStock.Rows.Count - 1; i++)
            {
               
                CheckBox ckSelect = ((CheckBox)(gvStock.Rows[i].FindControl("chkSelec")));

                if (ckSelect.Checked)
                {
                    ckSelect.Checked = false;
                    int cantStk = 0;
                    int cant = 0;
                    string str_ArtId = ((HiddenField)(gvStock.Rows[i].FindControl("hf_ArtId"))).Value;
                    string str_Talla = ((HiddenField)(gvStock.Rows[i].FindControl("hf_Talla"))).Value;
                    string str_Cantidad_Stock = ((HiddenField)(gvStock.Rows[i].FindControl("hf_Cantidad"))).Value;
                    string str_Cantidad = ((TextBox)(gvStock.Rows[i].FindControl("txtCantidad"))).Text;
                    string str_Precio = ((HiddenField)(gvStock.Rows[i].FindControl("hf_Precio"))).Value;
                    cantStk = Convert.ToInt32(str_Cantidad_Stock);
                    cant = Convert.ToInt32(str_Cantidad);
                                    
                     
                                    
                    DataRow[] _filas = dt.Select("Referencia='" + str_ArtId + "' and Talla='" + str_Talla + "'", "precio asc");

                    if (_filas.Length > 0)
                    {
                        string _articulo = _filas[0]["Referencia"].ToString();
                        string _talla = _filas[0]["Talla"].ToString();
                        listExistentes += "(Referencia:" + _articulo + "- Talla:" + _talla + "),";
                    }
                    else {

                        if (cant <= 0 || cant > cantStk)
                        {
                            strErrorCantidad += "(Referencia:" + str_ArtId + "- Talla:" + str_Talla + "),";
                        }
                        else
                        {
                            strDataDetalle += "<row  ";
                            strDataDetalle += " Codigo=¿" + str_ArtId + "¿ ";
                            strDataDetalle += " Talla=¿" + str_Talla + "¿ ";
                            strDataDetalle += " Cantidad=¿" + str_Cantidad + "¿ ";
                            strDataDetalle += " Precio=¿" + str_Precio + "¿ ";
                            strDataDetalle += "/>";
                        }
                    }

                }

            }

            if (listExistentes != "") {
                listExistentes = "No se agregaron articulos con tallas ya asociados al premio:" + listExistentes.TrimEnd(',')+"."; 
            }


            if (strDataDetalle == "")
            {
                if (strErrorCantidad != "")
                {
                    this.msnMessage.LoadMessage("Error : Debe ingresar cantidades validas (menor o igual al stock)." + strErrorCantidad.TrimEnd(',') + ".", ucMessage.MessageType.Error);
                }
                else
                {
                    this.msnMessage.LoadMessage("Error : Debe elegir al menos un elemento de la lista." + listExistentes, ucMessage.MessageType.Error);
                }
            }
            else
            {
                Boolean resullt = Premio.InsertarArticuloPremio(_user._bas_id, _idpremio, strDataDetalle);
                
                if (resullt)
                {
                    msnMessage.LoadMessage("Se agrego los articulos seleccionados."+ listExistentes, UserControl.ucMessage.MessageType.Information);
                }
                else
                {

                    this.msnMessage.LoadMessage("Error en el guardado de los articulos.", ucMessage.MessageType.Error);
                }

            }

            CargarArticulosPremio();
            if(TxtItem.Text.Trim()!="")
                initGrid(TxtItem.Text.Trim());

        }

     
    }
}