using System;
using System.Collections;
using System.Data;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Logistica;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Ventas;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace www.aquarella.com.pe.Reports.Ventas
{
    public partial class reportInvoice : System.Web.UI.Page
    {
        private ReportDocument facturacionObjectsReport = new ReportDocument();
        private string _nombreSession = "ValoresFacturacion";
        private string _pathFile = "InvoiceReport.rpt";        
        
        private Users _user;
        private ArrayList facturacionValues;
        private ArrayList pagoncredito;
        private ArrayList pagoforma;
        private string _noOrderUrl;
        private string _noInvoice;
        private string reportPath;

       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session[Constants.NameSessionUser] == null)
                Utilities.logout(this.Page.Session, this.Page.Response);
            else
                this._user = (Users)this.Session[Constants.NameSessionUser];
            if (!this.IsPostBack)
                this.Session[this._nombreSession] = (object)null;

            this._noOrderUrl = this.Request.Params["NoLiquidation"] == null ? "-1" : Convert.ToString(this.Request.Params["NoLiquidation"]);
            this._noInvoice = this.Request.Params["NoInvoice"] == null ? "-1" : Convert.ToString(this.Request.Params["NoInvoice"]);
            
            this.PopulateValueCrystalReportI();
            
            this.reportPath = this.Server.MapPath(this._pathFile);
            this.facturacionObjectsReport = new ReportDocument();
            this.facturacionObjectsReport.Load(this.reportPath);
            this.facturacionObjectsReport.SetDataSource((IEnumerable)this.facturacionValues);

            this.facturacionObjectsReport.OpenSubreport("VentaPNC").SetDataSource(pagoncredito);
            this.facturacionObjectsReport.OpenSubreport("formapago").SetDataSource(pagoforma);

            //Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase(_conn);

            //string[] vconexion = rptconexion.fconexion(db.ConnectionString);

            // _liqObjReport.OpenSubreport("pagonc");




            //this.facturacionObjectsReport.SetDatabaseLogon(vconexion[0], vconexion[1], vconexion[2], "");

            
            if (this.Request.Params["ShowReportOnWeb"] == null)
                this.facturacionObjectsReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "ReportFac_" + this._noInvoice);
            
            this.CrystalReportFacturacion.ReportSource = (object)this.facturacionObjectsReport;
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if ((this.facturacionObjectsReport != null) && this.facturacionObjectsReport.IsLoaded)
            { 
                this.facturacionObjectsReport.Close();
                this.facturacionObjectsReport.Dispose();
            }
        }

        public void PopulateValueCrystalReportI()
        {
            if (this.Session[this._nombreSession] == null)
            {
                DataSet ds_venta = Facturacion.getInvoiceHdr(this._noInvoice);
                DataTable invoiceHdr = ds_venta.Tables[0].Copy();
                //DataTable invoiceHdr = Facturacion.getInvoiceHdr(this._user._usv_co, this._noInvoice, this._noOrderUrl);
                if (invoiceHdr.Rows.Count > 0)
                {
                    //DataTable warehouseByPk = new warehouses(this._user._usv_co, invoiceHdr.Rows[0]["stv_warehouse"].ToString()).getWarehouseByPk();
                    string wavDescription = "";
                    string wavAddress = "";
                    string wavPhone = "";
                    string wavUbication = "";
                    //if (warehouseByPk != null && warehouseByPk.Rows.Count > 0)
                    //{
                    wavDescription = invoiceHdr.Rows[0]["almacen"].ToString().ToUpper();
                    wavAddress = invoiceHdr.Rows[0]["alm_direccion"].ToString();
                    wavPhone = invoiceHdr.Rows[0]["Alm_Telefono"].ToString();
                    wavUbication = "";
                    //}
                    string typeresolution = "";

                    //DataTable invoiceDtl = Facturacion.getInvoiceDtl(this._user._usv_co, this._noInvoice);

                    DataTable invoiceDtl = ds_venta.Tables[1].Copy();

                    string str = "";
                    Decimal descuentoGnral = 0;
                    string numeroRemision = "";
                    string destinatario = invoiceHdr.Rows[0]["nombres"].ToString();
                    string cedula = invoiceHdr.Rows[0]["Bas_Documento"].ToString();
                    string ubicacionDestinatario = invoiceHdr.Rows[0]["ubicacion"].ToString();
                    string telefono = invoiceHdr.Rows[0]["Bas_Telefono"].ToString();
                    string trasportadora = invoiceHdr.Rows[0]["Tra_Descripcion"].ToString();
                    string numeroGuia = invoiceHdr.Rows[0]["Tra_Gui_No"].ToString();
                    Decimal porc_percepcion = Convert.ToDecimal(invoiceHdr.Rows[0]["Percepcionp"].ToString());
                    Decimal iva = Convert.ToDecimal(invoiceHdr.Rows[0]["igvmonto"].ToString());
                    Decimal flete = 0;
                    DateTime fechaRemision = Convert.ToDateTime(invoiceHdr.Rows[0]["Ven_Fecha"].ToString());
                    Decimal ncredito = Convert.ToDecimal(invoiceHdr.Rows[0]["ncredito"].ToString());
                    Decimal totalop = Convert.ToDecimal(invoiceHdr.Rows[0]["totalop"].ToString());
                    this.facturacionValues = new ArrayList();

                    foreach (DataRow dataRow in (InternalDataCollectionBase)invoiceDtl.Rows)
                    {
                        string numFactura = dataRow["Ven_Det_Id"].ToString();
                        string esCopia = str;
                        string msgs = "";// invoiceHdr.Rows[0]["imv_text"].ToString();
                        string codigoArticulo = dataRow["Art_Id"].ToString();
                        string nomArticulo = dataRow["art_descripcion"].ToString();
                        Decimal cantidad = Convert.ToDecimal(dataRow["Ven_Det_Cantidad"].ToString());
                        string talla = dataRow["Ven_Det_TalId"].ToString();
                        Decimal precio = Convert.ToDecimal(dataRow["Ven_Det_Precio"].ToString());
                        Decimal valorLinea = Convert.ToDecimal(dataRow["articulo_value"].ToString());
                        Decimal descuentoArticulo = 0;
                        Decimal comisionLineal = Convert.ToDecimal(dataRow["Ven_Det_ComisionM"].ToString());
                        string descripcionArtic = dataRow["Col_Descripcion"].ToString();
                        this.facturacionValues.Add((object)new ReporteFacturacion(destinatario, ubicacionDestinatario, telefono, "", "", cedula, "", this._noOrderUrl, numFactura, fechaRemision, numeroRemision, "", esCopia, typeresolution, codigoArticulo, nomArticulo, descripcionArtic, cantidad, talla, precio, descuentoArticulo, comisionLineal, valorLinea, iva, flete, numeroGuia, trasportadora, msgs, descuentoGnral, wavDescription, wavAddress, wavPhone, wavUbication, porc_percepcion, ncredito, totalop));
                    }

                    this.pagoncredito = new ArrayList();

                    //DataSet dsLiqpagoInfo = Liquidations_Hdr.getpagoncreditoliqui(this._noOrderUrl);
                    DataSet dsLiqpagoInfo = new DataSet();
                    dsLiqpagoInfo.Tables.Add(ds_venta.Tables[2].Copy());

                    if (dsLiqpagoInfo == null)
                        return;

                    foreach (DataRow dRowDtl in dsLiqpagoInfo.Tables[0].Rows)
                    {
                        string vncredito = dRowDtl["ncredito"].ToString();
                        decimal VtotalcreditoTotal = Convert.ToDecimal(dRowDtl["Total"].ToString());
                        DateTime vfecha = Convert.ToDateTime(dRowDtl["fecha"].ToString());

                        www.aquarella.com.pe.bll.Reports.LiqNcSubinforme objLiqpagoReport = new www.aquarella.com.pe.bll.Reports.LiqNcSubinforme("", vncredito, vfecha, VtotalcreditoTotal);

                        pagoncredito.Add(objLiqpagoReport);
                    }



                    this.pagoforma = new ArrayList();
                    //DataSet dsLiqpagoformaInfo = Liquidations_Hdr.getpagonformaliqui(this._noOrderUrl);
                    DataSet dsLiqpagoformaInfo = new DataSet();
                    dsLiqpagoformaInfo.Tables.Add(ds_venta.Tables[3].Copy());
                    if (dsLiqpagoInfo == null)
                        return;

                    foreach (DataRow dRowDtl in dsLiqpagoformaInfo.Tables[0].Rows)
                    {
                        string vpago = dRowDtl["pago"].ToString();
                        string vdocumento = dRowDtl["Documento"].ToString();
                        DateTime vfecha = Convert.ToDateTime(dRowDtl["fecha"].ToString());
                        Decimal vtotal = Convert.ToDecimal(dRowDtl["Total"].ToString());
                        www.aquarella.com.pe.bll.Reports.VentaPagoSubInforme objLiqpagoformaReport = new www.aquarella.com.pe.bll.Reports.VentaPagoSubInforme(vpago, vdocumento, vfecha, vtotal);
                        pagoforma.Add(objLiqpagoformaReport);
                    }
                }             
            }           
        }
    }
}