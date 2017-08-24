using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.Bll.Control;

namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class Catalogo_Config : System.Web.UI.Page
    {
        //public bool respuesta;
        public Users _user;
        string _nameSessDatamanifiestoconsulta = "session_mani_consulta";
        //public string DSApplication = "DataSetApplications";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {                                
                _consultar();
                txtcatalogo.Focus();
            }
            //if (Session[Constants.NameSessionUser] == null)
            //    Utilities.logout(Page.Session, Page.Response);
            //else
            //    _user = (Users)Session[Constants.NameSessionUser];

            //if (!IsPostBack)
            //{
            //    Session.Remove(DSApplication);               
            //    llenarGrillar();
            //}
        }
        private void _consultar()
        {
            msnMessage.HideMessage();
            try
            {
                string _des = txtcatalogo.Text;               
                DataTable dt = CatalogoClass.consulta_catalogo(_des);
                Session[_nameSessDatamanifiestoconsulta] = dt;
                gvmanifiesto.DataSource = dt;
                gvmanifiesto.DataBind();
            }
            catch (Exception ex)
            {
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            _consultar();
        }

        protected void btncrearman_Click(object sender, EventArgs e)
        {
            Session["estado"] = "1";
            Response.Redirect("Catalogo.aspx");
        }

        protected void gvmanifiesto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvmanifiesto.PageIndex = e.NewPageIndex;
            gvmanifiesto.DataSource = (DataTable)Session[_nameSessDatamanifiestoconsulta];

            gvmanifiesto.DataBind();
        }

        protected void gvmanifiesto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msnMessage.HideMessage();
            if (e.CommandName.Equals("EditOrder"))
            {
                //esta session es para modificar
                Session["estado"] = "2";
                Response.Redirect("Manifiesto.aspx?noManifiesto=" + e.CommandArgument.ToString());
            }


            if (e.CommandName.Equals("starnular"))
            {
                this.msnMessage.HideMessage();
                this.msnMessage.Visible = false;
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _idman = e.CommandArgument.ToString();

                try
                {

                    Boolean _valida =CatalogoClass._anular_catalogo(Convert.ToDecimal(_idman));
                    if (_valida)
                    {
                        _consultar();
                        msnMessage.LoadMessage("Se Anulo el catalogo con id N° " + _idman, UserControl.ucMessage.MessageType.Information);
                    }
                    else
                    {
                        msnMessage.LoadMessage("Hubo un problema, no Se Anulo el catalogo con id N°" + _idman, UserControl.ucMessage.MessageType.Information);
                    }

                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
                }

            }
        }

        protected void gvmanifiesto_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                // Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());

                try
                {
                    string _estid = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Est_Id").ToString();
                    if (!string.IsNullOrEmpty(_estid))
                    {
                        //

                        if (_estid == "A")
                        ///
                        {
                            Image imganula = (Image)e.Row.FindControl("ibanular");
                            ///
                            imganula.Visible = true;

                            Image imgmodi = (Image)e.Row.FindControl("imgedit");
                            ///
                            imgmodi.Visible = true;

                            Image imgimp = (Image)e.Row.FindControl("imgInv");
                            ///
                            imgimp.Visible = true;


                        }
                    }

                }
                catch
                {
                }
            }
        }

        protected void gvmanifiesto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                string str = DataBinder.Eval(e.Row.DataItem, "IdCatalogo").ToString();
                ImageButton imageButton1 = (ImageButton)e.Row.FindControl("ibanular");
                imageButton1.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Anular el Catalogo Virtual : -" + DataBinder.Eval(e.Row.DataItem, "descripcion") + "- ?')");
            }
            catch
            {
            }
        }
        //protected void llenarGrillar()
        //{
        //    //DataTable data = CatalogoClass.GetAllCatalogo();
        //    //data.DefaultView.Sort = "descripcion";
        //    //GridApplication.DataSource = data;
        //    //GridApplication.DataBind();
        //    //Session[DSApplication] = data;
        //}
        //protected void SaveApp_Click(object sender, EventArgs e)
        //{
        //    //CatalogoClass cat = new  CatalogoClass();

        //    //cat.idcatalogo = 0;
        //    //cat.descripcion = txtdescripcion.Text.Trim();
        //    //cat.header_title = txtheadertitle.Text.Trim();            

        //    //if (txtnropagina.Text != String.Empty)
        //    //    cat.nropagina = Convert.ToDecimal(txtnropagina.Text);

        //    //cat.est_id= DDStatus.SelectedValue;


        //    //respuesta = cat.InsertCatalogo();

        //    //if (respuesta)
        //    //{
        //    //    llenarGrillar();                
        //    //}
        //}

        //protected void btnFiltrar_Click(object sender, EventArgs e)
        //{
        //    //msnMessage.HideMessage();

        //    //if (txtFilterGrid.Text != String.Empty)
        //    //{
        //    //    llenarGrillar();
        //    //    DataTable data = (DataTable)Session[DSApplication];
        //    //    EnumerableRowCollection<DataRow> filteredRows = data.AsEnumerable().Where(x => x.Field<string>("descripcion").ToUpper().Contains(txtFilterGrid.Text.Trim().ToUpper()));

        //    //    if (filteredRows.Count() > 0)
        //    //    {
        //    //        DataTable dataFiltrado = filteredRows.CopyToDataTable();
        //    //        Session[DSApplication] = dataFiltrado;
        //    //        dataFiltrado.DefaultView.Sort = "descripcion";
        //    //        GridApplication.DataSource = dataFiltrado;
        //    //        GridApplication.DataBind();
        //    //    }
        //    //    else
        //    //        msnMessage.LoadMessage("Palabra no encontra {AQUARELLA}.", UserControl.ucMessage.MessageType.Information);
        //    //}
        //    //else
        //    //{
        //    //    msnMessage.LoadMessage("Digite una palabra para filtrar por nombre", UserControl.ucMessage.MessageType.Information);
        //    //    llenarGrillar();
        //    //}
        //}

        //protected void GridApplication_PageIndexChanging(object sender, GridViewPageEventArgs e)
        ////{
        ////    GridApplication.PageIndex = e.NewPageIndex;
        ////    DataTable data = (DataTable)Session[DSApplication];
        ////    data.DefaultView.Sort = "Descripcion";
        ////    GridApplication.DataSource = data;
        ////    GridApplication.DataBind();
        //}
        //[WebMethod()]
        //public static string ajaxUpdateApp(decimal CAT_IDCATALOGO, string CAT_DESCRIPCION, string CAT_HEADERTITLE, Decimal CAT_NROPAGINA, string CAT_ESTID)
        //{
        //    //    CAT_IDCATALOGO,CAT_DESCRIPCION,CAT_HEADERTITLE,CAT_NROPAGINA, CAT_ESTID
        //    bool respuesta = CatalogoClass.UpdateCatalogo(CAT_IDCATALOGO, CAT_DESCRIPCION, CAT_HEADERTITLE, CAT_NROPAGINA, CAT_ESTID);

        //    if (respuesta)
        //        return "1";
        //    else
        //        return "-1";

        //}

        //protected void btnrefresh_Click(object sender, EventArgs e)
        //{
        //    llenarGrillar();
        //}
    }
}