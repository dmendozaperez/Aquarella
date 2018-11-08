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
    public partial class DespachoAlmacen : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        string _nameSessionDataLiq = "_ReturnDataLiq";
        string _Separator = ".";

        private string _nombreSession = "ValoresventaxLider";
        private string _iddespacho { get; set; }
        private string _strDescripcion { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            //fecha de inicio para el manejo de acumulados
           
            // Vencimiento de sesion
            _iddespacho = this.Request.Params["IdDespacho"] != null ? ((object)this.Request.Params["IdDespacho"]).ToString() : string.Empty;
            _strDescripcion = this.Request.Params["Descripcion"] != null ? ((object)this.Request.Params["Descripcion"]).ToString() : string.Empty;
        

            if (_iddespacho.Equals("0"))
            {
                btGuardar.Text = "Crear Despacho";
                btnEditDespacho.Visible = false;
                TxtDescripcion.Enabled = true;
            }
            else {
                TxtDescripcion.Enabled = false;
                btnList.Visible = false;
            }

            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                txtDateStart.Text = "19/05/2018";
                //txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

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

       

        private void sbconsulta()
        {
            string desc = _strDescripcion.ToString();
            DataSet dsreturn = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.getLiquidacionDespacho(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text));
            DataTable dt1 = new DataTable("tabla1");
            DataTable dtLiq = new DataTable("tabla2");

            TxtDescripcion.Text = desc;

            if (dsreturn.Tables.Count > 0)
            {
                dt1 = dsreturn.Tables[0];
                dtLiq = dsreturn.Tables[1];
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
            Session[_nameSessionDataLiq] = dsreturn.Tables[1];

            //if (dsreturn.Tables.Count > 0)
            //{
            //    MergeRows(gvReturns, 2);
            //}


        }

        protected void marcarFlete()
        {
          
            for (int i = 0; i <= gvReturns.Rows.Count - 1; i++)
            {
                string strhf_flete = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_flete"))).Value;
               
                if (strhf_flete.Equals("S"))
                    ((CheckBox)(gvReturns.Rows[i].FindControl("chkFlete"))).Checked = true;             

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
            ExportarExcel(dt, "1,11", "2", "Despacho_Pendiente");

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
            string strLiqLiderDespacho = "";
            int intIdDespacho = Convert.ToInt32(_iddespacho);
           string msjError = ""; 

            string Descripcion = TxtDescripcion.Text;
            Descripcion = TxtDescripcion.Text;
            for (int i = 0; i <= gvReturns.Rows.Count - 1; i++)
            {
               CheckBox ckSelect = ((CheckBox)(gvReturns.Rows[i].FindControl("chkSelec")));
                
                List<string> listaIdLider = new List<string>();

                if (ckSelect.Checked) {

                    string strIdLider = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_IdLider"))).Value;
                    string strLider = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_Lider"))).Value;
                    //string strRotulo = ((TextBox)(gvReturns.Rows[i].FindControl("txtRotulo"))).Text;
                    string strRotulo = Request.Form["Rotulo_" + strIdLider];
                    string strRotuloCourier = Request.Form["RotuloCourier_" + strIdLider];
                    string strPares = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_Pares"))).Value;
                    string strCatalogo = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_Catal"))).Value;
                    string strDestino = ((TextBox)(gvReturns.Rows[i].FindControl("TxtDestino"))).Text;
                    string strAgencia = ((TextBox)(gvReturns.Rows[i].FindControl("txtAgencia"))).Text;
                    string strMonto = ((HiddenField)(gvReturns.Rows[i].FindControl("hf_Monto"))).Value;
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
                    strDataDetalle += " Lider=¿" + strLider + "¿ ";
                    strDataDetalle += " Rotulo=¿" + strRotulo + "¿ ";
                    strDataDetalle += " RotuloCourier=¿" + strRotuloCourier + "¿ ";
                    strDataDetalle += " McaCourier=¿" + strMcaCourier + "¿ ";
                    strDataDetalle += " Pares=¿" + strPares + "¿ ";
                    strDataDetalle += " Catalogo=¿" + strCatalogo + "¿ ";
                    strDataDetalle += " Destino=¿" + strDestino + "¿ ";
                    strDataDetalle += " Agencia=¿" + strAgencia + "¿ ";
                    strDataDetalle += " Monto=¿" + strMonto + "¿ ";
                    strDataDetalle += " Obs=¿" + strObs + "¿ ";
                    strDataDetalle += " Det=¿" + strDetalle + "¿ ";
                    strDataDetalle += " McaFlete=¿" + strMcaFlete + "¿ ";
                    strDataDetalle += "/>";

                    strLiqLiderDespacho += devolverIdliquidacion(strIdLider);

                    if (strRotulo.Trim() == "") { 
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
                }

                if (msjError != "")
                    break;
            }


            if (strDataDetalle != "")
            {
                if (msjError == "")
                {

                    DataSet dsreturn = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.InsertarDespacho(_user._bas_id, intIdDespacho, strDataDetalle, strLiqLiderDespacho, Descripcion);
                    DataTable dtResult = new DataTable();
                    dtResult = dsreturn.Tables[0];

                    foreach (DataRow row in dtResult.Rows)
                    {
                        _iddespacho = row["DESPACHO_ID"].ToString();

                    }

                    Response.Redirect("EditDespachoAlmacen.aspx?IdDespacho=" + _iddespacho);
                }
                else {
                    msjError = msjError.TrimEnd(',');
                    string mensaje = "El Campo :" + msjError + " es obligatorio.";

                    if (msjError.Length > 8)
                        mensaje = "Los campos :" + msjError + " son obligatorios.";

                    this.msnMessage.LoadMessage(mensaje, ucMessage.MessageType.Error);
                }
            }
            else {
                this.msnMessage.LoadMessage("Error : Debe elegir al menos un elemento de la lista." , ucMessage.MessageType.Error);
            }

        }
        
        protected void btnList_Click(object sender, EventArgs e)
        {
            //este estado 1 cuando es nuevo manifiesto
           
            Response.Redirect("ListaDespachoAlmacen.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //este estado 1 cuando es nuevo manifiesto

            Response.Redirect("EditDespachoAlmacen.aspx?IdDespacho=" + _iddespacho);
        }

        private string devolverIdliquidacion(string strIdLider)
        {
            string StrlistLiquidacion = "";
            DataTable dtidLiquidacion = new DataTable();
            dtidLiquidacion = (DataTable)Session[_nameSessionDataLiq];

            foreach (DataRow row in dtidLiquidacion.Rows)
            {
                if (strIdLider == row["Area_Id"].ToString()) {

                    string strLiq_Id = row["Liq_Id"].ToString();
                    StrlistLiquidacion += "<row  ";
                    StrlistLiquidacion += " IdLider=¿" + strIdLider + "¿ ";
                    StrlistLiquidacion += " IdDespacho=¿xxyy¿ ";
                    StrlistLiquidacion += " IdLiqui=¿" + strLiq_Id + "¿ ";
                    StrlistLiquidacion += "/>";

                }


            }


            return StrlistLiquidacion;

        }

        public void llenarGrilla()
        {
            DataSet ds = new DataSet();
            string idLider = "449";
            string Descripcion = TextBox1.Text;

            ds = www.aquarella.com.pe.Bll.Ventas.DespachoAlmacen.getRotulo(idLider, Descripcion);
            //Session[DSArticulos] = ds;
            GridRotulos.DataSource = ds;
            GridRotulos.DataBind();

        }


        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            llenarGrilla();
        }

        protected void GridRotulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
            {
                string sss = "";

            }
        }

        protected void GridRotulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridRotulos.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[_nameSessionData];
            GridRotulos.DataSource = data.Tables[0];
            GridRotulos.DataBind();

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
    }
}