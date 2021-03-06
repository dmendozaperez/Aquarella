﻿using System;
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
    public partial class EditSalidaAlmacen : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        string _nameSessionDataLiq = "_ReturnDataLiq";
        string _Separator = ".";
        string _gStrEstado = "R";
        private string _iddespacho { get; set; }

        private string _nombreSession = "ValoresventaxLider";

        private string _session_tipo_despacho = "_session_tipo_despacho";


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
            Response.Redirect("DespachoAlmacen.aspx?IdDespacho=" + strIdDespacho);
        }




        private void sbconsulta()
        {
            string strFlgAtendido = "N";
            int idDespacho = Convert.ToInt32(_iddespacho);
            DataSet dsreturn = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.getDespacho(idDespacho);
            DataTable dt1 = new DataTable("tabla1");
            DataTable dtDt = new DataTable();


            if (dsreturn.Tables.Count > 0)
            {
                dt1 = dsreturn.Tables[0];

                Session[_session_tipo_despacho] = dt1.Rows[0]["DESP_TIPO"].ToString();
                lbltipo.Text= dt1.Rows[0]["DESP_TIPO_DES"].ToString();

                dtDt = dsreturn.Tables[1];

                foreach (DataRow row in dtDt.Rows)
                {
                    string _TotalPedido = row["NroPedidos"].ToString();
                    string _TotalEnviado = row["NroEnviados"].ToString();
                    string _TotalMonto = row["MontoTotal"].ToString();
                    string _TotalCatalogPedido = row["CatalogPedidos"].ToString();
                    string _TotalCatalogEnviado = row["CatalogEnviados"].ToString();

                    string _TotalPremioPedido = row["NroPremio"].ToString();
                    string _TotalPremioEnviado = row["PremioEnviados"].ToString();



                    txtPedido.Text = _TotalPedido;
                    txtEnviado.Text = _TotalEnviado;
                    txtPedidoC.Text = _TotalCatalogPedido;
                    txtEnviadoC.Text = _TotalCatalogEnviado;

                    txtPedidoP.Text = _TotalPremioPedido;
                    txtEnviadoP.Text = _TotalPremioEnviado;

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

            if  (Session[_session_tipo_despacho].ToString()=="L")
            {
                gvReturns.Columns[10].Visible = false;
                gvReturns.Columns[11].Visible = false;
            }


            Session[_nameSessionData] = dsreturn.Tables[0];

            TxtDescripcion.Text = ((HiddenField)(gvReturns.Rows[0].FindControl("hf_Descripcion"))).Value;
            strFlgAtendido = ((HiddenField)(gvReturns.Rows[0].FindControl("hf_Atendido"))).Value;
            TextFecha.Text = ((HiddenField)(gvReturns.Rows[0].FindControl("hf_FecCrea"))).Value;
            txtEstado.Text = ((HiddenField)(gvReturns.Rows[0].FindControl("hf_Estado"))).Value;
            txtDocumento.Text = ((HiddenField)(gvReturns.Rows[0].FindControl("hf_nroDoc"))).Value;
            _gStrEstado = ((HiddenField)(gvReturns.Rows[0].FindControl("hf_IdEstado"))).Value;

            if (strFlgAtendido == "S")
               chkAtender.Checked = true;

            if (_gStrEstado == "S")
                deshabilitarControles();

        }

        protected void deshabilitarControles()
        {

            chkAtender.Checked = true;
            chkAtender.Enabled = false;
            chkEstSalida.Checked = true;
            chkEstSalida.Enabled = false;
            btGuardar.Visible = false;

            for (int i = 0; i <= gvReturns.Rows.Count - 1; i++)
            {
                TextBox txtPares = (TextBox)(gvReturns.Rows[i].FindControl("txtPares"));
                txtPares.Enabled = false;

                TextBox txtParesPremios = (TextBox)(gvReturns.Rows[i].FindControl("txtPremio"));
                txtParesPremios.Enabled = false;

                TextBox txtParesCatalogos = (TextBox)(gvReturns.Rows[i].FindControl("txtCatalog"));
                txtParesCatalogos.Enabled = false;

            }       

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

            DataTable dtexcel = dt.Clone();

            foreach (DataRow fila in dt.Rows)
            {
                dtexcel.ImportRow(fila);
            }



            DataColumn col_remove = dtexcel.Columns["Desp_NroDoc"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Desp_Descripcion"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Estado"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Desp_FechaCre"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Rotulo_Courier"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalPremio"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalPremioEnviado"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalCatalogo"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalCatalogEnviado"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalParesEnviado"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Total_Cantidad"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Total_Cantidad_Envio"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalVenta"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["CobroFlete"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Courier"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Detalle"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["McaCourier"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["McaFlete"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Enviado"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Desp_IdDetalle"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Desp_Id"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalParesEnviadoEdit"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalCatalogEnviadoEdit"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["TotalPremioEnviadoEdit"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["IdEstado"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Atendido"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["IdLider"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["Lid_prom"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["DESP_TIPO_DES"];
            dtexcel.Columns.Remove(col_remove);

            col_remove = dtexcel.Columns["DESP_TIPO"];
            dtexcel.Columns.Remove(col_remove);

            if (Session[_session_tipo_despacho].ToString()=="L")
            {
                col_remove = dtexcel.Columns["Agencia"];
                dtexcel.Columns.Remove(col_remove);

                col_remove = dtexcel.Columns["Destino"];
                dtexcel.Columns.Remove(col_remove);
            }


            //ExportarExcel(dt, "0,1,2,3,19,20,21,22,23,24,25,26,27,28,29", "2", "Orden_Despacho");
            //ExportarExcel(dt, "0,1,2,3,21,22,23,24,25,26,27,28,29,30,31", "2", "Orden_Despacho");

            ExportarExcel(dtexcel, "", "2", "Orden_Despacho");

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
            string strTotalPremioPedido = txtPedidoP.Text;
            string strTotalPremioEnviado = txtEnviadoP.Text;
            string strTotalMonto = txtMonto.Text;
            string str_tipo_des = lbltipo.Text;

            string strTable = "<table <Table border='1' bgColor='#ffffff' " +
            "borderColor='#000000' cellSpacing='2' cellPadding='2' " +
            "style='font-size:10.0pt; font-family:Calibri; background:white;'>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Tipo Despacho </ td ><td width='400' align='left' >" + str_tipo_des + "</ td > ";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Nro. Documento </ td ><td width='400' align='left' >" + nrodoc + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Fec. Creación. </ td ><td width='400' align='left' colspan='2' >" + fec + "</ td > </tr>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Total Monto. </ td ><td width='400' align='left' >" + strTotalMonto + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Estado </ td ><td width='400' align='left' colspan='2' >" + est + "</ td ></tr>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Pares Pedido </ td ><td width='400' align='left' >" + strTotalPedido + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Pares Enviado </ td ><td width='400' align='left' colspan='2' >" + strTotalEnviado + "</ td ></tr>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Catalogo Facturado </ td ><td width='400' align='left' >" + strTotalCataPedido + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Catalogo Enviado </ td ><td width='400' align='left' colspan='2' >" + strTotalCataEnviado + "</ td ></tr>";

            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Premio Pedido </ td ><td width='400' align='left' >" + strTotalPremioPedido + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Premio Enviado </ td ><td width='400' align='left' colspan='2' >" + strTotalPremioEnviado + "</ td ></tr>";


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

        protected void btGuardar_Click(object sender, EventArgs e)
        {
            string strDataDetalle = "";
            string strAtender = "N";
            int intIdDespacho = Convert.ToInt32(_iddespacho);

            if (chkAtender.Checked)
                strAtender = "S";

            string IdEstado = _gStrEstado;
            if (chkEstSalida.Checked)
                IdEstado = "S";

            for (int i = 0; i <= gvReturns.Rows.Count - 1; i++)
            {
                
                string strIdDetalle = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_IdDetalle"))).Value;
                string strParesSalida = ((TextBox)(gvReturns.Rows[i].FindControl("txtPares"))).Text;
                string strCatalogoSalida = ((TextBox)(gvReturns.Rows[i].FindControl("txtCatalog"))).Text;
                string strPremioSalida = ((TextBox)(gvReturns.Rows[i].FindControl("txtPremio"))).Text;

                strDataDetalle += "<row  ";
                strDataDetalle += " IdDetalle=¿" + strIdDetalle + "¿ ";
                strDataDetalle += " ParesSalida=¿" + strParesSalida + "¿ ";
                strDataDetalle += " CatalogSalida=¿" + strCatalogoSalida + "¿ ";
                strDataDetalle += " PremioSalida=¿" + strPremioSalida + "¿ ";
                strDataDetalle += "/>";

            }
            Boolean bValida = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.ActualizarSalidaDespacho(_user._bas_id, intIdDespacho, IdEstado, strDataDetalle, strAtender);
            if (bValida)
            {
                sbconsulta();
            }
            else
            {
                msnMessage.LoadMessage("Hubo un error en la Actualización.", UserControl.ucMessage.MessageType.Error);
            }


        }

        
        protected void btnList_Click(object sender, EventArgs e)
        {
           Response.Redirect("ListaSalidaAlmacen.aspx");
        }

    }
}