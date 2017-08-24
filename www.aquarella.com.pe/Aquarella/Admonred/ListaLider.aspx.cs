using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class ListaLider : System.Web.UI.Page
    {
        Users _user;
       
        string _nameSessionData = "_ReturnData";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session[Constants.NameSessionUser]==null) Utilities.logout(Page.Session,Page.Response);
            else
                _user=(Users)Session[Constants.NameSessionUser];
            if (!IsPostBack)
            {
                cargarlista();
            }

        }
        private void cargarlista()
        {

            //odsReturns.DataBinding
            //odsReturns.Selecting();
            odsReturns.SelectParameters[0].DefaultValue = "";
            gdlista.DataSourceID = odsReturns.ID;
            gdlista.DataBind();

            //DataSet ds = new DataSet();
            //ds = Lider.Lider.getlistalider();
            //Session[_nameSessionData] = ds.Tables[0];
            //gdlista.DataSource = ds.Tables[0];
            //gdlista.DataBind();
            
        }

      

        //protected void gdlista_PageIndexChanging1(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        //{
        //    gdlista.PageIndex = e.NewPageIndex;
        //    gdlista.DataSource = (DataTable)Session[_nameSessionData];
        //    gdlista.DataBind();
        //}

        #region Exportar Excel
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {

            gdlista.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gdlista);
            gdlista.DataBind();

            string nameFile = "Listalideres";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gdlista);

          
        }



     

        #endregion

        protected void odsReturns_Selected(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessionData] = dt;
            }
            catch
            { }
        }

      
    }
   
}   