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
using System.IO;

using System.Text;
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
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");
                cargarLider();
                consultar();
            }
        }
        protected void btConsult_Click(object sender, EventArgs e)
        {
            consultar();
        }
        protected void formForCustomer(DateTime fechaini, DateTime fechafin)
        {
            // Ocultar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = false;
            string _area = dwlider.SelectedValue;
            DataTable dt = Lider.Lider.get_promotorXlider(_area, fechaini,fechafin, _user._asesor);
            Session[_nameSessionData] = dt;
            gvpromotor.DataSource = dt;
            gvpromotor.DataBind();
            MergeRows(gvpromotor, 2);
            //
            //   setParamsDataSource(_user._usv_area.ToString());
            //
            //refreshGrid();
        }
        private void formForEmployee(DateTime fechaini,DateTime fechafin)
        {
            try
            {
                string _area = dwlider.SelectedValue;
                DataTable dt = Lider.Lider.get_promotorXlider(_area,fechaini,fechafin,_user._asesor);
                Session[_nameSessionData] = dt;
                gvpromotor.DataSource = dt;
                gvpromotor.DataBind();
                MergeRows(gvpromotor, 2);
                //       //odsReturns.SelectParameters[0].DefaultValue = _user._usv_co;
                //    odsReturns.SelectParameters[0].DefaultValue = dwCustomers.SelectedValue;
                //     odsReturns.SelectParameters[1].DefaultValue = _user._asesor;
            }
            catch
            {
                return;
            }
        }
        private void consultar()
        {
            try
            {
                DateTime fechaini =Convert.ToDateTime(txtDateStart.Text);
                DateTime fechafin =Convert.ToDateTime(txtDateEnd.Text);

                if ((_user._usu_tip_id == "01") || (_user._usu_tip_id == "03"))
                {
                    formForCustomer(fechaini,fechafin);
                }
                else
                {
                    formForEmployee(fechaini,fechafin);
                }


               
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
            MergeRows(gvpromotor, 2);
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
            //gvpromotor.DataSource = (DataTable)Session[_nameSessionData];
            //gvpromotor.AllowPaging = false;
            //GridViewExportUtil.removeFormats(ref gvpromotor);
            //gvpromotor.DataBind();

            //string nameFile = "ListaPromotorXLider";

            ////  pass the grid that for exporting ...
            //Decimal[] _columnacaracter = { 4 };
            //GridViewExportUtil.Export(nameFile + ".xls", gvpromotor,true, _columnacaracter);

            ExportarExcel();
        }


        private void ExportarExcel()
        {

            DataTable dt = (DataTable)Session[_nameSessionData];

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();

            String inicio;

            Style stylePrueba = new Style();
            stylePrueba.Width = Unit.Pixel(200);
            string strRows = "";
            string strRowsHead = "";
            strRowsHead = strRowsHead + "<tr height=38 >";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string strDescrip = dt.Columns[i].ColumnName;

                switch (i)
                {
                    case 11:
                        strDescrip= "Fec.Ing";

                        break;
                    case 12:
                        strDescrip = "Fec.Activacion";
                        break;
                }

                strRowsHead = strRowsHead + "<td height=38  bgcolor='#969696' width='38'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + strDescrip + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</ td > ";
            }

            strRowsHead = strRowsHead + "</tr>";

            foreach (DataRow row in dt.Rows)
            {
                strRows = strRows + "<tr height='38' >";
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                   
                        strRows = strRows + "<td width='400'   >" + row[i].ToString() + "</ td > ";
                   
                }

                strRows = strRows + "</tr>";
            }

            inicio = "<div> " +
                    "<table <Table border='1' bgColor='#ffffff' " +
                    "borderColor='#000000' cellSpacing='2' cellPadding='2' " +
                    "style='font-size:10.0pt; font-family:Calibri; background:white;'>" +
                    strRowsHead +
                     strRows +
                    "</table>" +
                    "</div>";

            sb.Append(inicio);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=ListaPromotorXLider.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }


    }
}