using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Logistica;
using System.Data;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class PanelManifiesto : System.Web.UI.Page
    {
        string _nameSessDatamanifiestoconsulta = "session_mani_consulta";
        Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {               
                DateTime Vult = DateTime.Now;
                DateTime fecha1;
                DateTime fechatemp;
                fechatemp = DateTime.Today;
                fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

                txtDateStart.Text = fecha1.Date.ToString("dd/MM/yyyy");             
                txtDateEnd.Text = Vult.Date.ToString("dd/MM/yyyy");
                _consultar();
                txtmanifiesto.Focus();
            }
        }
        private void _consultar()
        {
            msnMessage.HideMessage();
            try
            {
                Decimal _id = 0;
                decimal.TryParse(txtmanifiesto.Text,out _id);
                DataTable dt= ManifiestoBll.consulta_manifiesto(_id,Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text));
                Session[_nameSessDatamanifiestoconsulta] = dt;
                gvmanifiesto.DataSource = dt;
                gvmanifiesto.DataBind();
            }
            catch(Exception ex)
            {
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        protected void btncrearman_Click(object sender, EventArgs e)
        {
            //este estado 1 cuando es nuevo manifiesto
            Session["estado"] = "1";
            Response.Redirect("Manifiesto.aspx");
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

                    Boolean _valida = ManifiestoBll._anular_manifiesto(_user._bas_id, Convert.ToDecimal(_idman));
                    if (_valida)
                    {
                        _consultar();
                        msnMessage.LoadMessage("Se Anulo el numero de manifiesto N° " + _idman, UserControl.ucMessage.MessageType.Information);
                    }
                    else
                    {
                        msnMessage.LoadMessage("Hubo un problema, no Se Anulo el numero de manifiesto N° " + _idman, UserControl.ucMessage.MessageType.Information);
                    }
                  
                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
                }

            }
        }
        protected void gvmanifiesto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvmanifiesto.PageIndex = e.NewPageIndex;
            gvmanifiesto.DataSource = (DataTable)Session[_nameSessDatamanifiestoconsulta];

            gvmanifiesto.DataBind();
        }

        protected void gvmanifiesto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                string str = DataBinder.Eval(e.Row.DataItem, "IdManifiesto").ToString();
                ImageButton imageButton1 = (ImageButton)e.Row.FindControl("ibanular");
                imageButton1.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Anular el manifiesto número : -" + DataBinder.Eval(e.Row.DataItem, "IdManifiesto") + "- ?')");
            }
            catch
            {
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

                        if (_estid == "MA")
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

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            _consultar();
        }
    }
}