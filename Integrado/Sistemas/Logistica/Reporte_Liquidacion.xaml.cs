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
using CrystalDecisions.CrystalReports.Engine;

namespace Integrado.Sistemas.Logistica
{
    /// <summary>
    /// Lógica de interacción para Reporte_Liquidacion.xaml
    /// </summary>
    public partial class Reporte_Liquidacion : MetroWindow
    {
        private ArrayList _invoiceData;
        private ArrayList _invoiceDataSummary;

        private ArrayList _pickValsReport;
        private ReportDocument _pickObjReport;
        /// <summary>
        /// Numero de liquidacion y marcador
        /// </summary>
        string _noLiq, _empPick, _excel;

        /// <summary>
        /// Direccion del archivo de crystal report
        /// </summary>
        string reportPath;

        public static string _id_invoice { set; get; }
        public static string _id_liquidation { set; get; }


        private DataSet ds_paquete;
        public Reporte_Liquidacion()
        {
            InitializeComponent();
            _noLiq = _id_invoice;
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _invoiceData = new ArrayList();
                _invoiceDataSummary = new ArrayList();
                //String varNumGuia = _invHdrVM.updateNumeroGuia(_co, _idv_invoice, _user._usn_pon_pos_id);
                this.LoadDataInvoiceReport();
                liquidationReport report = new liquidationReport();
                //////ReportInvoice report = new ReportInvoice();
                //report.SetDataSource(_invoiceData);

                report.Database.Tables[0].SetDataSource(_invoiceData);

                crReportInvoiceLiquidacion.ViewerCore.ReportSource = report;
            }
            catch (Exception ex)
            {                
                MessageBox.Show("Error en la generacion del reporte, detalle :" + ex.Message.ToString(), Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LoadDataInvoiceReport()
        {
            DataSet dsLiqInfo = Dat_Venta.getLiquidationHdrInfo(_noLiq);

            if (dsLiqInfo == null || dsLiqInfo.Tables[0].Rows.Count == 0)
                return;

            DataSet dsLiqDtl = Dat_Venta.getDtlPicking(_noLiq);

            if (dsLiqDtl == null || dsLiqDtl.Tables[0].Rows.Count == 0)
                return;

            DataRow dRow = dsLiqInfo.Tables[0].Rows[0];

            foreach (DataRow dRowDtl in dsLiqDtl.Tables[0].Rows)
            {
                Integrado.Sistemas.Control.Liquidation objPickReport = new Integrado.Sistemas.Control.Liquidation("", dRow["almacen"].ToString(),
                dRow["alm_direccion"].ToString(), dRow["Alm_Telefono"].ToString(), "", dRow["Bas_Id"].ToString(),
                dRow["Bas_Documento"].ToString(), dRow["nombres"].ToString(), dRow["Bas_Direccion"].ToString(), dRow["Bas_Telefono"].ToString(),
                dRow["Bas_Celular"].ToString(), dRow["Bas_Correo"].ToString(), dRow["ubicacion"].ToString(), dRow["Liq_Id"].ToString(),
                dRow["estado"].ToString(), dRowDtl["tdv_article"].ToString(), dRowDtl["brv_description"].ToString(),
                string.Empty, dRowDtl["arv_name"].ToString(), dRowDtl["tdv_size"].ToString(), Convert.ToDecimal(dRowDtl["tdn_qty"]), dRowDtl["stv_descriptions"].ToString(),
                dRowDtl["po"].ToString(), _empPick, dRowDtl["instrucciones"].ToString(), dRow["lider"].ToString());

                _invoiceData.Add(objPickReport);
            }
        }
    }
    
}
