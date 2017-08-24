using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using System.Configuration;

namespace www.aquarella.com.pe.Aquarella.Maestros
{
    public partial class informationarticle : System.Web.UI.Page
    {
        /// <summary>
        /// Variable de imagen por defecto.
        /// </summary>
        static string _imageDefault = "~/Design/images/ArticlesImages/b_shoe_image.jpg";
       
        /// <summary>
        /// Objeto User que contiene toda la información del usuario.
        /// </summary>
        Users _user;


        protected void Page_Load(object sender, EventArgs e)
        {
         
            ///Verificar que no se haya vencido la session
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
           
            String article = (String)Request.Params["elcitra"];

            /// Revizar si es una consulta publica
            String isForPublicAcces = ((String)Request.Params["isForPublicAcces"]) == null ? "T" : ((String)Request.Params["isForPublicAcces"]);

            if (article != null && _user._usn_userid != 0)
            {
                try
                {                    
                    DataRow row = null;
                    DataTable dttalla = new DataTable(); 
                    if (isForPublicAcces != null)
                    {
                     
                        if (isForPublicAcces.ToUpper().Equals("T") || isForPublicAcces.ToUpper().Equals("TRUE"))
                        {
                            row = (Article.getV_ArticleByPkAll(article,ref dttalla)).Tables[0].Rows[0];

                            /// Ocultar informacion confidencial, solo para empleados.
                            
                            HtmlGenericControl liControl = new HtmlGenericControl();
                            liControl =  this.FindControl("infoemploye") as HtmlGenericControl;
                            liControl.Visible = false;
                            
                            HtmlGenericControl divControl = new HtmlGenericControl();
                            divControl = this.FindControl("fragment3") as HtmlGenericControl;
                            divControl.Visible = false;                          
                        }
                        else
                        {
                            row = (Article.getV_ArticleByPkAll(article,ref dttalla)).Tables[0].Rows[0];
                        }

                    }
                    
                    
                    //lblSupplier.Text = (string)row["suv_name"];
                    lbCodArticle.Text = (string)row["Art_Id"];
                    lbNameArticle.Text = row.IsNull("Art_Descripcion") ? string.Empty : (string)row["Art_Descripcion"];
                    lblTitleArt2.Text = row.IsNull("Art_Descripcion") ? string.Empty : " : " + (string)row["Art_Descripcion"];
                    lbBrandArticle.Text = row["Mar_Descripcion"].ToString();
                    lblTitleArt.Text = row["Mar_Descripcion"].ToString();
                    //lbCategorization.Text = row["Cat_Pri_Descripcion"].ToString() + "-" + row["Cat_Descripcion"].ToString() + "-" + row["Sca_Descripcion"].ToString();
                    lbArticleType.Text = row["Cat_Pri_Descripcion"].ToString() + "-" + row["Cat_Descripcion"].ToString() + "-" + row["Sca_Descripcion"].ToString();                    
                    //lbOrigin.Text = row["orv_description"].ToString();
                    
                    ///No muestra la imagen ya prove sin el resolve
                    if (row.IsNull("Art_Foto") || ((String)row["Art_Foto"]).Trim().Equals(String.Empty))
                    ImageShoe.ImageUrl = ResolveClientUrl(_imageDefault);
                    else
                    ImageShoe.ImageUrl = ResolveClientUrl((string)row["Art_Foto"]);
                    //lbArticleType.Text = row["atv_description"].ToString();
                    //lbMaterial.Text = row["mav_description"].ToString();
                    
                  
                    String sizes = "";
                    //DataTable dt = Articles_Sizes.GetByARTICLE(_user._usv_co, article);
                    DataTable dt = dttalla;
                    bool first = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (first)
                        {
                            sizes = (String)dr["Tal_Descripcion"];
                            first = false;
                        }
                        else
                            sizes = sizes + "-" + (String)dr["Tal_Descripcion"];
                    }
                    lbTallas.Text = sizes;
                    lbColor.Text = row["Col_Descripcion"].ToString();
                    //lbDesign.Text = row["dev_description"].ToString();
                    //lbPacking.Text = row["ptv_description"].ToString();
                    //lbCollection.Text = row["collection"].ToString();
                    //lbSole.Text = row["sov_description"].ToString();
                    //lbUpper.Text = row["upv_description"].ToString();
                    //lbHeeled.Text = row["hev_description"].ToString();
                  
                    Decimal _odn_odv = 0;

                    if (row["Art_Costo"].ToString().Equals("Sin Establecer"))
                        lbODV.Text = row["Art_Costo"].ToString();
                    else
                    {
                        _odn_odv = Convert.ToDecimal(row["Art_Costo"]);
                        lbODV.Text = _odn_odv.ToString(ConfigurationManager.AppSettings["kCurrency"]);
                    }

                    Decimal _prn_public_price = 0;
                    _prn_public_price = Convert.ToDecimal(row["Art_Pre_Sin_Igv"]);
                    if (row["Art_Pre_Sin_Igv"].ToString().Equals("Sin Establecer"))
                        lbPublic_Price.Text = _prn_public_price.ToString(ConfigurationManager.AppSettings["kCurrency"]);
                    else
                    {
                        lbPublic_Price.Text = _prn_public_price.ToString(ConfigurationManager.AppSettings["kCurrency"]);
                        
                        lbMargen.Text = string.Format("{0:P}", www.aquarella.com.pe.bll.Util.Utilities.margenCalc(_prn_public_price, _odn_odv));
                    }
                  
                    this.ImageShoe.Focus();
                }
                catch
                {
                    msnMessage.LoadWithOutScrollMessage("Artículo No encontrado. Puede ser que el articulo no posea precio y/o costo.", UserControl.ucMessage.MessageType.Error);                    
                }
            }
            else
            {
                Utilities.logout(Page.Session, Page.Response);
            }
        }
    }
}