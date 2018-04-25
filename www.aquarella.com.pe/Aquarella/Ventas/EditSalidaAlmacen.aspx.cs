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
           

            if (dsreturn.Tables.Count > 0)
            {
                dt1 = dsreturn.Tables[0];
             
            }
            else
            {
                DataTable dt2 = new DataTable();
                dsreturn.Tables.Add(dt2);
            }

            gvReturns.DataSource = dt1;
            gvReturns.DataBind();
           
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
            ExportarExcel(dt, "0,1,2,3,14,15,16,17,18,19", "2", "Orden_Despacho");

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

            string strTable = "<table <Table border='1' bgColor='#ffffff' " +
            "borderColor='#000000' cellSpacing='2' cellPadding='2' " +
            "style='font-size:10.0pt; font-family:Calibri; background:white;'>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Nro. Documento </ td ><td width='400' >" + nrodoc + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Descripción</ td ><td width='400' >" + desc + "</ td > </tr>";
            strTable += "<tr height=38 ><td height=38  bgcolor='#969696' width='38'>Fec. Creación. </ td ><td width='400' align='left' >" + fec + "</ td > ";
            strTable += "<td height=38  bgcolor='#969696' width='38'>Estado </ td ><td width='400' >" + est + "</ td ></tr>";
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
                
                strDataDetalle += "<row  ";
                strDataDetalle += " IdDetalle=¿" + strIdDetalle + "¿ ";
                strDataDetalle += " ParesSalida=¿" + strParesSalida + "¿ ";
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