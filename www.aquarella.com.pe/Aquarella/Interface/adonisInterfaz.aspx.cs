using System;
using System.Data;
using System.Web.UI;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Interfaces;
//using Bata.Aquarella.BLL.Interfaces;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Ventas;
//using Bata.Aquarella.Pe.BLL.Ventas;

namespace www.aquarella.com.pe.Aquarella.Interface
{
    public partial class adonisInterfaz : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "InfoInterfacAdonis";
        decimal subtotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = null;

                pnlDwCustomers.Visible = true;
                /// Realizar la consulta de coordinadores
                DataSet dsCustomers = Coordinator.getCoordinators(_user._usv_area,_user._asesor);
                dwCustomers.Focus();
                // Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
                dwCustomers.DataSource = dsCustomers;
                dwCustomers.DataBind();

                if ((_user._usu_tip_id == "02"))
                {
                    Utilities.logout(Page.Session, Page.Response);
                }
                else
                {
                    this.formForEmployee();
                }
                //if (_user._usv_employee)
                //    this.formForEmployee();
                //else
                //    Utilities.logout(Page.Session, Page.Response);
            }
        }
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    foreach (GridViewRow row in gvDocTrans.Rows)
        //    {
        //        if (row.RowType == DataControlRowType.DataRow)
        //        {
        //            row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvDocTrans, "Select$" + row.DataItemIndex, true);
        //            row.Attributes["onmouseover"] = "this.className='selectRow';";
        //            row.Attributes["onmouseout"] = "this.className='normalRow';";
        //        }
        //    }
        //    base.Render(writer);
        //}
        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        protected void formForEmployee()
        {
            //if (!string.IsNullOrEmpty(_user._usv_warehouse))
            //{
            //    if (!string.IsNullOrEmpty(_user._usv_area))
            //    {
            //        WareAreaForm.wareAreaForm(_user._usv_co, _user._usv_region);
            //        WareAreaForm.setFormByUser(_user);
            //        WareAreaForm.unableArea();
            //    }
            //    else
            //        msnMessage.LoadMessage("No se encuentra asociado a ninguna area", UserControl.ucMessage.MessageType.Error);
            //}
            //else
            //    msnMessage.LoadMessage("No se encuentra asociado a ninguna bodega", UserControl.ucMessage.MessageType.Error);

            // Enlazar datoS
            //refreshGridView();
        }


        #region < Botones >

        protected void btConsult_Click(object sender, EventArgs e)
        {
            initGrid(DateTime.Parse(txtDateStart.Text), DateTime.Parse(txtDateEnd.Text),dwCustomers.SelectedValue);
        }

 

        #endregion

        private void initGrid(DateTime start, DateTime end,string var_cliente)
        {
            this.Session[this._nameSessionData] = Documents_Trans.get_Docn_TransAdonis(start, end, var_cliente).Tables[0];
            this.gvDocTrans.DataSource = (DataTable)this.Session[this._nameSessionData];
            this.gvDocTrans.DataBind();

            //this.Session[this._nameSessionData] = (object)Documents_Trans.get_Docn_TransAdonis(start, end,var_cliente);
            //this.gvDocTrans.DataSourceID = this.odsConsult.ID;
            //this.gvDocTrans.DataBind();
            //MergeRows(gvDocTrans, 2);
        }

        protected void grdPivot2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            MergeRows((GridView)sender, 1);
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Attributes["onmouseover"] =
            //        "javascript:setMouseOverColor(this);";
            //    e.Row.Attributes["onmouseout"] =
            //        "javascript:setMouseOutColor(this);";
            //    e.Row.Attributes["onclick"] =
            //    ClientScript.GetPostBackClientHyperlink
            //        ((GridView)sender, "Select$" + e.Row.RowIndex);
            //}
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

        protected void ibExportDoc_Click(object sender, ImageClickEventArgs e)
        {

            try {
                DateTime _fecStart = Convert.ToDateTime((txtDateStart.Text.Equals(string.Empty)) ? "01/01/1900" : txtDateStart.Text);
                DateTime _fecEnd = Convert.ToDateTime((txtDateEnd.Text.Equals(string.Empty)) ? "01/01/1900" : txtDateEnd.Text);

                DataSet _interf = Adonis.Get_Comercial_Interface( _fecStart, _fecEnd);
                DataTable dt = _interf.Tables[0];

                DataRow[] foundRows = dt.Select("", "");

                System.Text.StringBuilder str = new System.Text.StringBuilder();

                for (int i = 0; i <= foundRows.GetUpperBound(0); i++)
                {
                    str.Append(foundRows[i][0].ToString() + "\r\n");
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment;filename=InterComerAdonis.txt");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.Default;

                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                Response.Write(str.ToString());
                Response.End();

            //DataSet _interf = Adonis.Get_Comercial_Interface(_user._usv_co, DateTime.Parse(txtDateStart.Text), DateTime.Parse(txtDateEnd.Text));
            //DataTable dt = _interf.Tables[0];

            ////DataRow[] foundRows = dt.Select("", "orde ASC");
            //DataRow[] foundRows = dt.Select("","");

            //System.Text.StringBuilder str = new System.Text.StringBuilder();

            //for (int i = 0; i <= foundRows.GetUpperBound(0); i++)
            //{
            //    str.Append(foundRows[i][0].ToString() + "\r\n");
            //}

            //Response.Clear();
            //Response.Buffer = true;
            //Response.ContentType = "text/plain";
            //Response.AddHeader("Content-Disposition", "attachment;filename=InterComerAdonis.txt");
            //Response.Charset = "UTF-8";
            //Response.ContentEncoding = System.Text.Encoding.Default;

            //System.IO.StringWriter tw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            //Response.Write(str.ToString());
            //Response.End();

            }
            catch { }
        }

        protected void gvDocTrans_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if ((e.Row.RowType == DataControlRowType.DataRow))
            //{
            //    if (!(e.Row.Cells[10].Text == "&nbsp;"))
            //    {
            //        subtotal += Convert.ToDecimal(e.Row.Cells[10].Text);
            //    }
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[9].Text = "Total";
            //    e.Row.Cells[10].Text = subtotal.ToString("###,##0.00");// Convert.ToString(subtotal, "999,990.00");
            //    e.Row.Cells[10].Font.Bold = true;
            //    e.Row.Cells[10].Font.Size = 10;
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _estado = DataBinder.Eval(e.Row.DataItem, "Cuenta").ToString();

                if (_estado == String.Empty)
                {
                    e.Row.BackColor = System.Drawing.Color.Khaki;
                    e.Row.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                }


            }

            if (e.Row.RowType == DataControlRowType.Header)
            {
               //  //Agrupando las dos primeras columnas (col=0, col=1 y col=2)
               // e.Row.Cells[0].ColumnSpan = 3;
               // e.Row.Cells[1].Visible = false;
               // e.Row.Cells[2].Visible = false;

               // //'Opcion 1: Generando un tabla dinamicamente
               // Table tableCol1=new Table();
               // TableRow fila1=new TableRow();
               // TableRow fila2=new TableRow();
               // TableCell celda11=new TableCell();
               // TableCell celda21=new TableCell();
               // TableCell celda22=new TableCell();
               // TableCell celda23=new TableCell();


               // //Dim tableCol1 As New HtmlTable
               // //((Dim fila1, fila2 As New HtmlTableRow
               // //Dim celda11, celda21, celda22, celda23 As New HtmlTableCell

               // //Creando la primer fila para el encabezado (a dos filas)
               // //y agrupo por las cantidad de columnas hijas que tenga
               // celda11.Text = "Documento";
               // celda11.ColumnSpan = 5;
               // fila1.Cells.Add(celda11);

               // //'Contenido a las celdas de la fila de abajo
               // celda21.Text = "Imagen";
               // celda22.Text = "Nombre";
               // celda23.Text = "Fecha Nac.";

               // //'Agregando celdas a las filas
               // fila2.Cells.Add(celda21);
               // fila2.Cells.Add(celda22);
               // fila2.Cells.Add(celda23);
               // tableCol1.Rows.Add(fila1);
               // tableCol1.Rows.Add(fila2);

               //// 'Formateando tabla
               // tableCol1.BorderWidth= 2;
               // tableCol1.CellPadding = 0;
               // tableCol1.CellSpacing = 0;

               // //'Agregando una tabla generAQUARELLA dinamicamente a la celda 1 del encabezado 
               // //'que previamente la argupamos
               // e.Row.Cells[0].Controls.Clear();
               // e.Row.Cells[0].Controls.Add(tableCol1);



                //'Agrupando las dos ultimas columnas (col=3 y col=4)
                e.Row.Cells[6].ColumnSpan = 4;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;

                //'Opcion 2: Agregando una tabla generAQUARELLA dinamicamente
                e.Row.Cells[6].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=4><b>Documento</b></td></tr><tr><td><b>Tipo</b></td><td><b>Serie</b></td><td><b>Numero</b></td><td><b>Fecha</b></td></tr></table>";

            }

           

         

      
        }

        protected void dwCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvDocTrans.DataSource = Session[_nameSessionData];
            gvDocTrans.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvDocTrans);
            this.gvDocTrans.DataBind();

            //gvDocTrans.DataBind();

            string nameFile = "Asientos_AQ";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvDocTrans);
        }

        protected void gvDocTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocTrans.PageIndex = e.NewPageIndex;
            gvDocTrans.DataSource = (DataTable)Session[_nameSessionData];

            gvDocTrans.DataBind();
        }

      

       
    }
}