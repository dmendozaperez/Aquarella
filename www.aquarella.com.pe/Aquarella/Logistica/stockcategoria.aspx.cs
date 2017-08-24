using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.UserControl;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using System.Data;
using www.aquarella.com.pe.bll.Ventas;
//using Bata.Aquarella.Pe.BLL.Ventas;
//using Bata.Aquarella.Bll;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class stockcategoria : System.Web.UI.Page
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
                cargarCategoria();
                cargarTemporada();
                formForEmployee();
                
            }
        }
        protected void cargarCategoria()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de lideres        
            dwcategoria.Focus();
            dwcategoria.DataSource =www.aquarella.com.pe.bll.Stock.getAllcategoria();
            dwcategoria.DataBind();

        }

        protected void cargarTemporada()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de lideres        
            dwtemporada.Focus();
            dwtemporada.DataSource = www.aquarella.com.pe.bll.Stock.getAlltemporada();
            dwtemporada.DataBind();

        }

        private void formForEmployee()
        {
            try
            {
                this.msnMessage.Visible = false;
                sbconsulta(Convert.ToString(dwcategoria.SelectedValue));
                //odsReturns.SelectParameters[0].DefaultValue = _user._usv_co;
                //odsReturns.SelectParameters[1].DefaultValue = dwCustomers.SelectedValue;
            }
            catch (Exception ex)
            {
                this.msnMessage.LoadMessage("Error de Consulta: " + ex.Message, ucMessage.MessageType.Error);
                return;
            }
        }
        private void sbconsulta(string valor)
        {
            string _tempo = dwtemporada.SelectedValue;

            DataSet dsreturn = www.aquarella.com.pe.bll.Stock.getstockcategoria(valor,_tempo);

            Pivot pvt = new Pivot(dsreturn.Tables[0]);
            string[] fila = { "Categoria", "Codigo", "Descripcion", "tempo", "stock", "foto" };
            //string[] col = { "Ano","Mes","Semana", "Dia" };
            //string[] col = { "Ano", "Mes", "Semana" };
            string[] col = {"talla"};
           
            gvReturns.DataSource = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);
           

            gvReturns.DataBind();


            fijarcolumna();

            MergeRows(gvReturns, 1);

            Session[_nameSessionData] = dsreturn.Tables[0];
        }
        protected void fijarcolumna()
        {
            
             for (int rowIndex = gvReturns.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gvReturns.Rows[rowIndex];
                row.Cells[2].Width = 200;
                row.Cells[1].Wrap = true;
            }
             gvReturns.HeaderStyle.Wrap = true;
             gvReturns.RowStyle.Wrap = true;
            
           // var grid=gvReturns.view
           // gvReturns.HeaderRow.AccessKey
        }
        protected void btConsult_Click(object sender, EventArgs e)
        {

        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            Pivot pvt = new Pivot((DataTable)Session[_nameSessionData]);
            string[] fila = { "Categoria", "Codigo", "Descripcion", "tempo", "stock", "foto" };
            //string[] col = {"Ano", "Mes","Semana", "Dia" };
            string[] col = { "talla" };

            gvReturns.DataSource = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);
           
            gvReturns.DataBind();
            fijarcolumna();
            MergeRows(gvReturns, 1);
        }

        protected void grdPivot2_RowCreated(object sender, GridViewRowEventArgs e)
        {


            //        gvReturns.Columns[2].ItemStyle.Width = Unit.Parse("10px");

            if ((e.Row.RowType == DataControlRowType.Header))
            {
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[7].Visible = false;
                //e.Row.Cells[3].Visible = false;
            }

            

            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                
                

                /// Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());

                try
                {


                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[3].Visible = false;
                   
                    //
                    string referencia = DataBinder.Eval(e.Row.DataItem, "Codigo").ToString();

                    string rutafoto= DataBinder.Eval(e.Row.DataItem, "foto").ToString();
                    //
                    string nameArticle = DataBinder.Eval(e.Row.DataItem, "Codigo").ToString();
                    //
                    Label lblPhotoArticle = (Label)e.Row.FindControl("lblPhotoArticle");

                    string pAcc = (_user._usv_employee) ? "F" : "T";

                    //
                    lblPhotoArticle.Text = "<a class='iframe' href='../Maestros/informationarticle.aspx?elcitra=" + referencia + "&isForPublicAcces=" + pAcc + "' title='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "'>" +
                        //"<img src='../../Design/images/Botones/8564884_1.jpg' border='0' width='30' height='30' alt='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "' />" +
                         "<img src='" +  rutafoto + "' border='0' width='30' height='30' alt='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "' />" +
                        "</a>";
                }
                catch
                {
                }
            }
            
        }

        protected void dwcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vidcategoria = dwcategoria.SelectedValue;
            sbconsulta(vidcategoria);
        }
        protected DataTable getSource()
        {
            // Chequeado es ventas por semana y categoria
            /*if (chkGroupByWeek.Checked)
                return (DataTable)Session[_nameSessionData];
            // No chequeado es ventas netas entre las fechas dAQUARELLAs
            else*/
            return (DataTable)Session[_nameSessionData];
        }

        #region <metodos de pivot>
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
        #endregion

        #region < Exportar Excel>

        /// <summary>
        /// Exportar a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.AllowPaging = false;

            Pivot pvt = new Pivot((DataTable)Session[_nameSessionData]);
            string[] fila = { "Categoria", "Codigo", "Descripcion", "tempo", "stock" };
            //string[] col = {"Ano", "Mes","Semana", "Dia" };
            string[] col = { "talla" };

            gvReturns.DataSource = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);

            gvReturns.DataBind();

            MergeRows(gvReturns, 1);

            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "stockdetallado";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }

        #endregion

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            //Int32 widestData = 0;
            //System.Data.DataRowView drv;
            //drv = (System.Data.DataRowView)e.Row.DataItem;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (drv != null)
            //    {
            //        String catName = drv[1].ToString();
            //        Response.Write(catName + "/");

            //        int catNameLen = catName.Length;
            //        if (catNameLen > widestData)
            //        {
            //            widestData = catNameLen;
            //            gvReturns.Columns[2].ItemStyle.Width = 20;
            //            gvReturns.Columns[2].ItemStyle.Wrap = false;
            //        }

            //    }
            //}


        }

        protected void gvReturns_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void dwtemporada_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vidcategoria = dwcategoria.SelectedValue;
            sbconsulta(vidcategoria);
        }
    }

}