using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Collections;
using System.Data;
using Integrado.Bll;
using CapaDato.Bll.Venta;
using CapaEntidad.Bll.Util;
using Integrado.Reportes;

namespace Integrado.Sistemas.Logistica
{
    /// <summary>
    /// Lógica de interacción para Reporte_Guia_Remision.xaml
    /// </summary>
    public partial class Reporte_Guia_Remision : MetroWindow
    {
        private ArrayList _invoiceData;
        private ArrayList _invoiceDataSummary;


        public static string _idv_invoice { set; get; }
        public static string _idv_liquidation { set; get; }


        private DataSet ds_paquete;
        public Reporte_Guia_Remision()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoiceData = new ArrayList();
                _invoiceDataSummary = new ArrayList();
                //String varNumGuia = _invHdrVM.updateNumeroGuia(_co, _idv_invoice, _user._usn_pon_pos_id);
                this.LoadDataInvoiceReport();
                this.LoadDataInvoiceReport_Summary();
                ReportGuiaRem report = new ReportGuiaRem();
                //////ReportInvoice report = new ReportInvoice();
                //report.SetDataSource(_invoiceData);

                report.Database.Tables[0].SetDataSource(_invoiceData);
                report.Database.Tables[1].SetDataSource(_invoiceDataSummary);

                crReportInvoiceGuia.ViewerCore.ReportSource = report;
            }
            catch (Exception ex)
            {                
                MessageBox.Show("Error en la generacion del reporte, detalle :" + ex.Message.ToString(), Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadDataInvoiceReport()
        {
            ///
            DataSet dsInvoiceHdr =Dat_Venta.leer_venta_guia(_idv_invoice);



            if (dsInvoiceHdr != null && dsInvoiceHdr.Tables[0].Rows.Count > 0)
            {

                ds_paquete = new DataSet();
                ds_paquete.Tables.Add(dsInvoiceHdr.Tables[2].Copy());


                DataTable dsInvHdr = dsInvoiceHdr.Tables[0];
                String destinatario = dsInvHdr.Rows[0]["nombres"].ToString();
                ///
                String idPerson = dsInvHdr.Rows[0]["Ven_Id"].ToString();
                ///
                String ubicacionCustomer = dsInvHdr.Rows[0]["direccion"].ToString();
                ///
                String telefono = dsInvHdr.Rows[0]["Bas_Telefono"].ToString();
                ///
                String celular = dsInvHdr.Rows[0]["Bas_Celular"].ToString();
                ///
                String transportadora = dsInvHdr.Rows[0]["Tra_Descripcion"].ToString();
                ///
                String guia = dsInvHdr.Rows[0]["Tra_Gui_No"].ToString();

                String lider = dsInvHdr.Rows[0]["lider"].ToString();
                String lider_dir = dsInvHdr.Rows[0]["direccion_lider"].ToString();

                string agencia= dsInvHdr.Rows[0]["agencia"].ToString();

                string agencia_ruc= dsInvHdr.Rows[0]["agencia_ruc"].ToString();
                ///
                Decimal iva = 0;//Convert.ToDecimal(dsInvHdr.Rows[0]["ihn_taxes"].ToString());
                ///
                Decimal flete = 0;//Convert.ToDecimal(dsInvHdr.Rows[0]["ihn_handling"].ToString());
                /// Fecha de remision
                /// 
                DateTime fechaRemision = Convert.ToDateTime(dsInvHdr.Rows[0]["Ven_Fecha"].ToString());
                ///
                String esCopia = "";// ((Convert.ToDecimal(dsInvHdr.Rows[0]["IHN_PRT_CNT"].ToString())) > 1) ? " *** COPIA *** " : "";

                /// Descuento general
                Decimal dsctoGnral = 0;//Convert.ToDecimal(dsInvHdr.Rows[0]["ion_disscount"].ToString());

                ///
                String typeresolution = "";//dsInvHdr.Rows[0]["nuv_typeresolution"].ToString();


                ///

                string direccionf = "";
                string ubicalugar = dsInvHdr.Rows[0]["ubicalugar"].ToString();


                String resolucion = "";//"Facturación " + typeresolution + " por Res. Dian " + dsInvHdr.Rows[0]["nuv_resolution"].ToString() +
                                       //" De " + (String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(dsInvHdr.Rows[0]["nud_date"].ToString()))) +
                                       // " Del Pref " + dsInvHdr.Rows[0]["NUV_CODE"].ToString() + "-" + dsInvHdr.Rows[0]["NUN_INVOICE_START"].ToString()
                                       //  + " Al Pref " + dsInvHdr.Rows[0]["NUV_CODE"].ToString() + "-" + dsInvHdr.Rows[0]["nun_invoice_end"].ToString();
                                       ///

                String puntoLlegada = "";// dsInvHdr.Rows[0]["OHV_SHIP_TO"].ToString();

                String msgs = ""; //dsInvHdr.Rows[0]["imv_text"].ToString();
                ///
                DataSet dsInvDtl = new DataSet();// null;// _invDtlVM.getInvoiceDtl(_co, _idv_invoice);
                dsInvDtl.Tables.Add(dsInvoiceHdr.Tables[1].Copy());

                /// Id Bodega, para consultar la info de la bodega donde se genera la factura
                /// 
                //String stvWarehouse = dsInvHdr.Rows[0]["stv_warehouse"].ToString();
                //DataTable dtInfoWarehouse = _wareVM.loadWareByPk(_co, stvWarehouse).Tables[0];
                String wavDescription = "";
                String wavAddress = "";
                String wavPhone = "";
                String wavUbication = "";
                /////
                //if (dtInfoWarehouse != null && dtInfoWarehouse.Rows.Count > 0)
                //{
                //    ///
                //    wavDescription = dtInfoWarehouse.Rows[0]["wav_description"].ToString().ToUpper();
                //    wavAddress = dtInfoWarehouse.Rows[0]["wav_address"].ToString();
                //    wavPhone = dtInfoWarehouse.Rows[0]["telefonofijo"].ToString();
                //    wavUbication = dtInfoWarehouse.Rows[0]["ubication"].ToString();
                //}
                ////////////////////////////////////////////////////////////////////////////////////////////////////////


                ///
                foreach (DataRow drLine in dsInvDtl.Tables[0].Rows)
                {
                    ///                    
                    String numeroFactura = drLine["Ven_Det_Id"].ToString();
                    ///
                    String respCopia = esCopia;
                    ///

                    /// Detalle de la factura
                    /// 
                    String codigoArticulo = drLine["Ven_Det_ArtId"].ToString();
                    ///
                    String nomArticulo = drLine["Art_Descripcion"].ToString();
                    ///
                    String marca = drLine["Mar_Descripcion"].ToString();
                    ///
                    Decimal cantidad = Convert.ToDecimal(drLine["Ven_Det_Cantidad"].ToString());
                    ///
                    String talla = drLine["Ven_Det_TalId"].ToString(); ;
                    ///
                    Decimal valorVentaArticulo = 0;// Convert.ToDecimal(drLine["idn_sellprice"].ToString());
                    ///
                    Decimal valorLinea = 0;// Convert.ToDecimal(drLine["articlevalue"].ToString());
                    ///
                    Decimal dsctoArticulo = 0;// Convert.ToDecimal((drLine["idn_disscount"].ToString().Equals(String.Empty)) ? "0" : (drLine["idn_disscount"].ToString()));
                    ///
                    Decimal comisionArticulo = 0;// Convert.ToDecimal(drLine["idn_commission"].ToString());
                    ///
                    String color = drLine["Col_Descripcion"].ToString();


                    ReportInvoiceClass objRI = new ReportInvoiceClass(destinatario, ubicacionCustomer, telefono, celular, "", idPerson, "",
                        _idv_liquidation, numeroFactura, fechaRemision, resolucion, "",
                        respCopia, typeresolution, codigoArticulo, nomArticulo, color,
                        cantidad, talla, valorVentaArticulo,
                        dsctoArticulo, comisionArticulo, marca, valorLinea, iva, flete, guia, transportadora, msgs, dsctoGnral,
                        wavDescription, wavAddress, wavPhone, wavUbication, puntoLlegada, ubicalugar, direccionf, lider, lider_dir,agencia,agencia_ruc);



                    _invoiceData.Add(objRI);
                }/// End foreach
            }
        }
        public void LoadDataInvoiceReport_Summary()
        {
            DataSet dsInvSummary = ds_paquete;// _invSummary.getInvoiceDtl_Summary(_co, _idv_invoice);

            foreach (DataRow drLineSumm in dsInvSummary.Tables[0].Rows)
            {
                /// pdv_co, pdn_package,ldv_liquidation, count(pdn_qty) cantidad        
                String varId_Package = drLineSumm["Paq_Id"].ToString();
                /// 
                String varNunPaquete = drLineSumm["Paq_No"].ToString();
                ///
                Decimal cantidadBultos = Convert.ToDecimal(drLineSumm["cantidad"].ToString());
                ///

                ReportInvoiceSummary objSummary = new ReportInvoiceSummary(varId_Package, varNunPaquete, cantidadBultos);

                _invoiceDataSummary.Add(objSummary);
            }/// End foreach

        }
    }
}
