using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Ventas;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.Pe.BLL.Ventas;
using System.Collections.Generic;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
using www.aquarella.com.pe.UserControl;

using System.Data;
using www.aquarella.com.pe.bll.Ventas;
using System.IO;

using System.Text;

namespace www.aquarella.com.pe.Aquarella.Ventas

{
    public partial class EditDespachoAlmacen : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        string _nameSessionDataLiq = "_ReturnDataLiq";
        string _Separator = ".";
        string flgAnulado = "N";
        string gEstado = "";
        private string _iddespacho { get; set; }
        private string _TotalPedido { get; set; }
        private string _TotalEnviado { get; set; }
        private string _TotalCatalogPedido { get; set; }
        private string _TotalCatalogEnviado { get; set; }

        private string _TotalMonto { get; set; }

        private string _nombreSession = "ValoresventaxLider";


        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            _iddespacho = this.Request.Params["IdDespacho"] != null ? ((object)this.Request.Params["IdDespacho"]).ToString() : string.Empty;
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {

                formUsuario();

            }
        }

        #region <METODOS DEL CRYSTAL>

        #endregion

        private void formUsuario()
        {
            try
            {
                this.msnMessage.Visible = false;
                sbconsulta();

            }
            catch (Exception ex)
            {
                this.msnMessage.LoadMessage("Error de Consulta: " + ex.Message, ucMessage.MessageType.Error);
                return;
            }
        }


        protected void btConsult_Click(object sender, EventArgs e)
        {

            formUsuario();
        }

        protected void btAgregarLider_Click(object sender, EventArgs e)
        {

            string strIdDespacho = _iddespacho;
            string Codigo = txtDocumento.Text;
            Response.Redirect("DespachoAlmacen.aspx?IdDespacho=" + strIdDespacho + "&Descripcion=" + Codigo);
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


        private void sbconsulta()
        {

            int idDespacho = Convert.ToInt32(_iddespacho);
            DataSet dsreturn = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.getDespacho(idDespacho);
            DataTable dt1 = new DataTable("tabla1");
            DataTable dtDt = new DataTable();

            if (dsreturn.Tables.Count > 0)
            {
                dt1 = dsreturn.Tables[0];
                dtDt = dsreturn.Tables[1];

                foreach (DataRow row in dtDt.Rows)
                {
                    _TotalPedido = row["NroPedidos"].ToString();
                    _TotalEnviado = row["NroEnviados"].ToString();
                    _TotalCatalogPedido = row["CatalogPedidos"].ToString();
                    _TotalCatalogEnviado = row["CatalogEnviados"].ToString();
                    _TotalMonto = row["MontoTotal"].ToString();
                    txtPedido.Text = _TotalPedido;
                    txtEnviado.Text = _TotalEnviado;
                    txtPedidoC.Text = _TotalCatalogPedido;
                    txtEnviadoC.Text = _TotalCatalogEnviado;
                    txtMonto.Text = _TotalMonto;


                }
            }
            else
            {
                DataTable dt2 = new DataTable();
                dsreturn.Tables.Add(dt2);
            }

            gvReturns.DataSource = dt1;
            gvReturns.DataBind();
            marcarFlete();

            Session[_nameSessionData] = dsreturn.Tables[0];

            if (dsreturn.Tables.Count > 0)
            {
                MergeRows(gvReturns, 2);
            }
        }

        protected void marcarFlete()
        {
            string strDescripcion = "";
            string strFecCreacion = "";
            string strEstado = "";
            string strIdEstado = "";
            string strFlgAtendido = "";
            string strnroDocumento = "";

            for (int i = 0; i <= gvReturns.Rows.Count - 1; i++)
            {
                string strhf_flete = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_flete"))).Value;
                string strhf_courier = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_Courier"))).Value;
                strDescripcion = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_Descripcion"))).Value;
                strFecCreacion = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_FecCrea"))).Value;
                strEstado = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_Estado"))).Value;
                strIdEstado = ((HiddenField)(gvReturns.Rows[0].FindControl("hf_IdEstado"))).Value;
                strFlgAtendido = ((HiddenField)(gvReturns.Rows[0].FindControl("hf_Atendido"))).Value;
                strnroDocumento = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_nroDoc"))).Value;
                            
                if (strhf_flete.Equals("S"))
                    ((CheckBox)(gvReturns.Rows[i].FindControl("chkFlete"))).Checked = true;

                if (strhf_courier.Equals("S"))
                    ((CheckBox)(gvReturns.Rows[i].FindControl("chkCourier"))).Checked = true;

                if (strIdEstado =="S" || strFlgAtendido =="S")
                {
                    deshabilitarControl(i);
                    gEstado = "S";
                }
            }

            TxtDescripcion.Text = strDescripcion;
            txtEstado.Text = strEstado;
            txtDocumento.Text = strnroDocumento;
            TextFecha.Text = strFecCreacion;

            if (strIdEstado.Equals("S") || strFlgAtendido.Equals("S")) {
                btGuardar.Visible = false;
                btAgregarLider.Visible = false;
                TxtDescripcion.Enabled = false;
            }
      
        }

        protected void deshabilitarControl(int i)
        {
            
            ((TextBox)(gvReturns.Rows[i].FindControl("txtAgencia"))).Enabled= false;
            ((TextBox)(gvReturns.Rows[i].FindControl("TxtDestino"))).Enabled = false;
            //((TextBox)(gvReturns.Rows[i].FindControl("txtRotulo"))).Enabled = false;
            ((TextBox)(gvReturns.Rows[i].FindControl("TxtObservacion"))).Enabled = false;
            ((TextBox)(gvReturns.Rows[i].FindControl("TxtDetalle"))).Enabled = false;
            ((CheckBox)(gvReturns.Rows[i].FindControl("chkFlete"))).Enabled = false;
            gvReturns.Rows[i].Cells[10].Visible = false;
            gvReturns.HeaderRow.Cells[10].Visible = false;
            gvReturns.FooterRow.Cells[10].Visible = false;

        }



        #region <METODO DE FORMATO PIVOT>


        private string GetHeaderText(string s, int i, int PivotLevel)
        {
            if (!s.Contains(_Separator) && i != PivotLevel)
                return string.Empty;
            else
            {
                int Index = NthIndexOf(s, _Separator, i);
                if (Index == -1)
                    return s;
                return s.Substring(0, Index);
            }
        }
        private int NthIndexOf(string str, string SubString, int n)
        {
            int x = -1;
            for (int i = 0; i < n; i++)
            {
                x = str.IndexOf(SubString, x + 1);
                if (x == -1)
                    return x;
            }
            return x;
        }

        #endregion

        /// <returns></returns>
        protected DataTable getSource()
        {

            return (DataTable)Session[_nameSessionData];
        }


        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session[_nameSessionData];


            ExportarExcel(dt, "0,1,2,3,21,22,23,24,25,26,27,28,29,30,31", "2", "Orden_Despacho");

        }
                
        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;

            DataTable dt1 = new DataTable();

            dt1 = (DataTable)Session[_nameSessionData];
            gvReturns.DataSource = dt1;
            gvReturns.DataBind();
         }
        
        private void ExportarExcel(DataTable dt, string ColumnasOcultas, string ColumnasTexto, string NombreArchivo)
        {

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            String style = style = @"<style> .textmode { mso-number-format:\@; } </script> ";
            Page page = new Page();
            String inicio;
            ColumnasOcultas = ',' + ColumnasOcultas + ",";
            ColumnasTexto = ',' + ColumnasTexto + ",";

            Style stylePrueba = new Style();
            stylePrueba.Width = Unit.Pixel(200);
            string strRows = "";
            string strRowsHead = "";
            strRowsHead = strRowsHead + "<tr height=38 >";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                bool ocultar = false;
                string comp = "," + i.ToString() + ",";

                if (ColumnasOcultas != ",,")
                {
                    ocultar = ColumnasOcultas.Contains(comp);
                }

                if (!ocultar)
                    strRowsHead = strRowsHead + "<td height=38  bgcolor='#969696' width='38'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</ td > ";
            }

            strRowsHead = strRowsHead + "</tr>";

            foreach (DataRow row in dt.Rows)
            {
                strRows = strRows + "<tr height='38' >";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    bool ocultar = false;
                    string comp = "," + i.ToString() + ",";
                    string strClass = "";

                    if (ColumnasTexto != ",,")
                    {

                        if (ColumnasTexto.Contains(comp))
                            strClass = " class='textmode'";
                    }

                    if (ColumnasOcultas != ",,")
                    {

                        ocultar = ColumnasOcultas.Contains(comp);

                    }

                    if (!ocultar)
                        strRows = strRows + "<td width='400' " + strClass + " >" + row[i].ToString() + "</ td > ";
                }

                strRows = strRows + "</tr>";
            }

            string desc = TxtDescripcion.Text;
            string nrodoc = txtDocumento.Text;
            string est = txtEstado.Text;
            string fec = TextFecha.Text;
            string strTotalPedido = txtPedido.Text;
            string strTotalEnviado = txtEnviado.Text;
            string strTotalCataPedido = txtPedidoC.Text;
            string strTotalCataEnviado = txtEnviadoC.Text;
            string strTotalMonto = txtMonto.Text;

            string strTable = "<table <Table border='1' bgColor='#ffffff' " +
            "borderColor='#000000' cellSpacing='2' cellPadding='2' " +
            "style='font-size:10.0pt; font-family:Calibri; background:white;'>"; 
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Nro. Documento </ td ><td width='400' align='left' >" + nrodoc + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Fec. Creación. </ td ><td width='400' align='left' colspan='2' >" + fec + "</ td > </tr>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Total Monto. </ td ><td width='400' align='left' >" + strTotalMonto + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Estado </ td ><td width='400' align='left' colspan='2' >" + est + "</ td ></tr>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Pares Pedido. </ td ><td width='400' align='left' >" + strTotalPedido + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Pares Enviado </ td ><td width='400' align='left' colspan='2' >" + strTotalEnviado + "</ td ></tr>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Catalogo Facturado </ td ><td width='400' align='left' >" + strTotalCataPedido + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Catalogo Enviado </ td ><td width='400' align='left' colspan='2' >" + strTotalCataEnviado + "</ td ></tr>";

            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Descripción </ td ><td colspan='4' align='left' >" + desc + "</ td > ";
            strTable += "</tr>";

            strTable += "</table>";
            
            inicio = "<div> " + strTable +
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
            Response.AddHeader("Content-Disposition", "attachment;filename=" + NombreArchivo + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(style);
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void btGuardarEdit_Click(object sender, EventArgs e)
        {
            string strDataDetalle = "";
            string strDescripcion= TxtDescripcion.Text;
            int intIdDespacho = Convert.ToInt32(_iddespacho);
            string msjError = "";

            for (int i = 0; i <= gvReturns.Rows.Count - 1; i++)
            {
                
                string strIdDetalle = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_IdDetalle"))).Value;     
                //string strRotulo = ((TextBox)(gvReturns.Rows[i].FindControl("txtRotulo"))).Text;
                string strDestino = ((TextBox)(gvReturns.Rows[i].FindControl("TxtDestino"))).Text;
                string strAgencia = ((TextBox)(gvReturns.Rows[i].FindControl("txtAgencia"))).Text;
                string strIdLider = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_IdLider"))).Value;
                string strRotulo = Request.Form["Rotulo_" + strIdLider];
                string strRotuloCourier = Request.Form["RotuloCourier_" + strIdLider];

                string strObs = ((TextBox)(gvReturns.Rows[i].FindControl("TxtObservacion"))).Text;
                string strDetalle = ((TextBox)(gvReturns.Rows[i].FindControl("TxtDetalle"))).Text;

                string strMcaCourier = "N";
                CheckBox ckCourier = ((CheckBox)(gvReturns.Rows[i].FindControl("chkCourier")));
                if (ckCourier.Checked)
                    strMcaCourier = "S";

                string strMcaFlete = "N";
                CheckBox ckFlete = ((CheckBox)(gvReturns.Rows[i].FindControl("chkFlete")));
                if (ckFlete.Checked)
                strMcaFlete = "S";

                strDataDetalle += "<row  ";
                strDataDetalle += " IdLider=¿" + strIdLider + "¿ ";
                strDataDetalle += " IdDetalle=¿" + strIdDetalle + "¿ ";
                strDataDetalle += " Rotulo=¿" + strRotulo + "¿ ";
                strDataDetalle += " RotuloCourier=¿" + strRotuloCourier + "¿ ";
                strDataDetalle += " McaCourier=¿" + strMcaCourier + "¿ ";
                strDataDetalle += " Destino=¿" + strDestino + "¿ ";
                strDataDetalle += " Agencia=¿" + strAgencia + "¿ ";
                 strDataDetalle += " Obs=¿" + strObs + "¿ ";
                strDataDetalle += " Det=¿" + strDetalle + "¿ ";
                strDataDetalle += " McaFlete=¿" + strMcaFlete + "¿ ";
                strDataDetalle += "/>";

                if (strRotulo.Trim() == "")
                {
                    msjError += "Rotulo,";
                }

                if (strAgencia.Trim() == "")
                {
                    msjError += "Agencia,";
                }

                if (strDestino.Trim() == "")
                {
                    msjError += "Destino,";
                }

                if (msjError != "")
                    break;
            }

            if (msjError == "")
            {
                Boolean valida = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.ActualizarDespacho(intIdDespacho, strDataDetalle, strDescripcion);
                if (valida)
                {
                    msnMessage.LoadMessage("Los datos del despacho fueron Actualizados ", UserControl.ucMessage.MessageType.Information);
                    sbconsulta();
                }
                else {
                    msnMessage.LoadMessage("Hubo un error en la Actualización.", UserControl.ucMessage.MessageType.Error);

                }
            }else{
                msjError = msjError.TrimEnd(',');
                string mensaje = "El Campo :" + msjError + " es obligatorio.";

                if (msjError.Length>8)
                    mensaje = "Los campos :" + msjError + " son obligatorios.";

                this.msnMessage.LoadMessage(mensaje, ucMessage.MessageType.Error);
            }
        }

        protected void gvReturns_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("starnular"))
            {
                this.msnMessage.HideMessage();
                this.msnMessage.Visible = false;
                int idDespachoDetalle = Convert.ToInt32(e.CommandArgument.ToString());
                int nroRow = gvReturns.Rows.Count;

                try
                {

                    if (nroRow > 1)
                    {
                        Boolean _valida = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.Anular_DetalleDespacho(_user._bas_id, idDespachoDetalle);
                        if (_valida)
                        {
                          flgAnulado = "S";
                          
                            sbconsulta();

                            if (gEstado != "S" && flgAnulado == "S")
                                msnMessage.LoadMessage("Se Anulo el Detalle seleccionado.", UserControl.ucMessage.MessageType.Information);
                            else
                            {
                                msnMessage.LoadMessage("No se puede Realizar la accion.", UserControl.ucMessage.MessageType.Information);
                                btAgregarLider.Visible = false;
                                btAgregarLider.Enabled = false;
                                btGuardar.Visible = false;
                                btGuardar.Enabled = false;
                                Response.Redirect("EditDespachoAlmacen.aspx?IdDespacho=" + _iddespacho);
                            }
                            flgAnulado = "N";
                        }
                        else
                        {
                            msnMessage.LoadMessage("Hubo un problema, no Se Anulo el Detalle ", UserControl.ucMessage.MessageType.Information);
                        }
                    }
                    else {
                        msnMessage.LoadMessage("Accion rechazada: El despacho debe tener al menos un detalle.", UserControl.ucMessage.MessageType.Error);
                    }
                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
                }

            }

        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                string str = DataBinder.Eval(e.Row.DataItem, "Desp_IdDetalle").ToString();
                ImageButton imageButton1 = (ImageButton)e.Row.FindControl("ibanular");
                imageButton1.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de eliminar el detalle de despacho?')");
            }
            catch
            {
            }
        }


        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaDespachoAlmacen.aspx");
        }

    }
}