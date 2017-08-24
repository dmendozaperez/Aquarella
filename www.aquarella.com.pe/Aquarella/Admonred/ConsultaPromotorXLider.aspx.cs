using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.Aquarella.Lider;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class ConsultaPromotorXLider : System.Web.UI.Page
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
                cargarLider();
                consultar();
            }
        }
        protected void btConsult_Click(object sender, EventArgs e)
        {
            consultar();
        }
        private void consultar()
        {
            try
            {
                string _area = dwlider.SelectedValue;
                DataTable dt = Lider.Lider.get_promotorXlider(_area);
                Session[_nameSessionData] = dt;
                gvpromotor.DataSource = dt;
                gvpromotor.DataBind();
                MergeRows(gvpromotor, 1);
            }
            catch(Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        protected void cargarLider()
        {
            // Mostrar Panel de Seleccion de Coordinador

            /// Realizar la consulta de lideres        
            dwlider.Focus();
            dwlider.DataSource = Area.getAllAreas(_user._asesor);
            dwlider.DataBind();
        }

        protected void gvmanifiesto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpromotor.PageIndex = e.NewPageIndex;
            gvpromotor.DataSource = (DataTable)Session[_nameSessionData];

            gvpromotor.DataBind();
            MergeRows(gvpromotor, 1);
        }
        private void MergeRows(GridView gv, int rowPivotLevel)
        {
            for (int rowIndex = gv.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gv.Rows[rowIndex];
                GridViewRow prevRow = gv.Rows[rowIndex + 1];
                for (int colIndex = 0; colIndex < rowPivotLevel; colIndex++)
                {
                    if (row.Cells[colIndex].Text == prevRow.Cells[colIndex].Text)
                    {
                        row.Cells[colIndex].RowSpan = (prevRow.Cells[colIndex].RowSpan < 2) ? 2 : prevRow.Cells[colIndex].RowSpan + 1;
                        prevRow.Cells[colIndex].Visible = false;
                    }
                }
            }
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvpromotor.DataSource = (DataTable)Session[_nameSessionData];
            gvpromotor.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvpromotor);
            gvpromotor.DataBind();

            string nameFile = "ListaPromotorXLider";

            //  pass the grid that for exporting ...
            Decimal[] _columnacaracter = { 3 };
            GridViewExportUtil.Export(nameFile + ".xls", gvpromotor,true, _columnacaracter);
        }
    }
}