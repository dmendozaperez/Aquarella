using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace www.aquarella.com.pe
{
    
    public partial class report_form : System.Web.UI.Page
    {
        private ArrayList _liqValsReport;

        private ArrayList _liqValsSubReport;

        private ArrayList _liqValsPagoSubReport;

        private ReportDocument _liqObjReport;

        string reportPath;

        string _nameSessionData = "liquidationValues";

        /// <summary>
        /// Nombre del archivo de reporte de crystal
        /// </summary>
        string _nameFileCrystalReport = "liquidationReport.rpt";
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateValueCrystalReport();

            // Ubicacion del reporte crystal
            reportPath = Server.MapPath(_nameFileCrystalReport);

            //Instanciar el objeto de reporte de crystal
            _liqObjReport = new ReportDocument();

            //Enlazar el archivo del reporte y el objeto instanciado
            _liqObjReport.Load(reportPath);

            //Establecer el dataSource dirigido al reporte crystal
            _liqObjReport.SetDataSource(_liqValsReport);



            crvLiquidation.ReportSource = _liqObjReport;
        }
        protected void PopulateValueCrystalReport()
        {

            _liqValsReport = new ArrayList();

            //DataSet dsLiqInfo = Liquidations_Hdr.getLiquidationHdrInfo(_noLiq);

            //if (dsLiqInfo == null)
            //    return;

            //DataSet dsLiqDtl =  Liquidation_Dtl.getLiquidationDtl(_user._usv_co, _noLiq);
            //DataSet dsLiqDtl = new DataSet();
            //dsLiqDtl.Tables.Add(dsLiqInfo.Tables[1].Copy());

            //if (dsLiqDtl == null)
            //    return;

            //DataRow dRow = dsLiqInfo.Tables[0].Rows[0];

            //foreach (DataRow dRowDtl in dsLiqDtl.Tables[0].Rows)
            //{
            //    string vncredito = ""; decimal VtotalcreditoTotal = 0;
            //    string vfecha = DateTime.Today.ToString("dd/MM/yyyy");



            //Bata.Aquarella.BLL.Reports.Liquidation objLiqReport = new BLL.Reports.Liquidation(dRow["ohv_warehouseid"].ToString(), dRow["wav_description"].ToString(),
            //    dRow["wav_address"].ToString(), dRow["wav_telephones"].ToString(), dRow["ubicationwav"].ToString(), dRow["con_coordinator_id"].ToString(), dRow["bdv_document_no"].ToString(),
            //    dRow["name"].ToString(), dRow["bdv_address"].ToString(), dRow["bdv_phone"].ToString(), dRow["bdv_movil_phone"].ToString(), dRow["bdv_email"].ToString(),
            //    dRow["ubicationcustomer"].ToString(), dRow["lhv_liquidation_no"].ToString(), Convert.ToDateTime(dRow["lhd_date"]), Convert.ToDateTime(dRow["lhd_expiration_date"].ToString()),
            //    dRow["stv_description"].ToString(), Convert.ToDecimal(dRow["lon_disscount"]), Convert.ToDecimal(dRow["tax_rate"]), Convert.ToDecimal(dRow["lhn_tax_rate"]), Convert.ToDecimal(dRow["lhn_handling"]),
            //    dRowDtl["ldv_article"].ToString(), dRowDtl["brv_description"].ToString(), dRowDtl["cov_description"].ToString(), dRowDtl["arv_name"].ToString(), dRowDtl["ldv_size"].ToString(), Convert.ToDecimal(dRowDtl["ldn_qty"]),
            //    Convert.ToDecimal(dRowDtl["ldn_sell_price"]), Convert.ToDecimal(dRowDtl["ldn_commission"]), Convert.ToDecimal(dRowDtl["ldn_disscount"]), Convert.ToDecimal(dRow["percepcion"]), Convert.ToDecimal(dRow["porc_percepcion"]),
            //    Convert.ToDecimal(dRow["ncredito"]), vncredito, Convert.ToDateTime(vfecha), VtotalcreditoTotal, _noLiq, Convert.ToDecimal(dRow["totalop"]));


            www.aquarella.com.pe.bll.Reports2.Liquidation objLiqReport = new www.aquarella.com.pe.bll.Reports2.Liquidation("prueba");

            _liqValsReport.Add(objLiqReport);
            //}


        }
    }
}