using System;
using System.Collections.Generic;
//using Bata.Aquarella.BLL;

namespace www.aquarella.com.pe.UserControl
{
    public partial class ucAddItemsForm : System.Web.UI.UserControl
    {
        public static string _nSCatalog = "_nSCatalog", _nsDtlArticle = "_nsDtlArticle";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session[_nSCatalog] = new List<Catalog>();
                //Session[_nsDtlArticle] = new List<Order_Dtl>();
            }
        }

        public string getNameSession()
        {
            return _nSCatalog;
        }
    }
}