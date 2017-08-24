using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class consultaHPedido : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
               
 
            }
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            
            sbbuscar();
            gvped.DataSourceID = odsReturns.ID;
            gvped.DataBind();
        }

        protected void sbbuscar()
        {
            // Ocultar Panel de Seleccion de Coordinador
          
            //
            setParamsDataSource();
            //
            //refreshGrid();
        }
        protected void setParamsDataSource()
        {
            odsReturns.SelectParameters[0].DefaultValue = txtdniruc.Text;
        }

        protected void odsReturns_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessionData] = dt;
            }
            catch
            { }
        }
        #region <exportar excel>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvped.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvped);
            gvped.DataBind();

            string nameFile = "HistorialPedido";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvped);
        }
        #endregion

        protected void gvped_DataBound(object sender, EventArgs e)
        {
           for (int rowIndex = gvped.Rows.Count - 2; 
                                     rowIndex >= 0; rowIndex--)
              {
                GridViewRow gvRow = gvped.Rows[rowIndex];

                GridViewRow gvPreviousRow = gvped.Rows[rowIndex + 1];

                for (int cellCount = 0; cellCount < gvRow.Cells.Count-11; 

                                                              cellCount++)
                {
                 if (gvRow.Cells[cellCount].Text == 
                                        gvPreviousRow.Cells[cellCount].Text)
                 {
                   if (gvPreviousRow.Cells[cellCount].RowSpan < 2)
                   {
                     gvRow.Cells[cellCount].RowSpan = 2;
                   }
                   else
                   {
                    gvRow.Cells[cellCount].RowSpan = 
                        gvPreviousRow.Cells[cellCount].RowSpan + 1;
                   }
                   gvPreviousRow.Cells[cellCount].Visible = false;
                }
               }
             }
        }
    }
}