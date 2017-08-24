using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using System.Data;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class stockForm : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "ItemsStock_";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = new object();
            }
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            initGrid(TxtItem.Text.Trim());
        }

        private void initGrid( string item)
        {
            this.Session[this._nameSessionData] = (object)Stock.getArticleStock(item);
            this.gvStock.DataSourceID = this.odsConsult.ID;
            this.gvStock.DataBind();
        }

        /// <summary>
        /// Devolver la fuente de datos
        /// </summary>
        /// <returns></returns>
        public DataTable getSource()
        {
            try
            {
                return ((DataSet)this.Session[this._nameSessionData]).Tables[0];
            }
            catch
            {
                return new DataTable();
            }
        }

        protected void gvStock_RowCreated(object sender, GridViewRowEventArgs e)
        {
            ///
            // The GridViewCommandEventArgs class does not contain a 
            // property that indicates which row's command button was
            // clicked. To identify which row's button was clicked, use 
            // the button's CommandArgument property by setting it to the 
            // row's index.
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
    }
}