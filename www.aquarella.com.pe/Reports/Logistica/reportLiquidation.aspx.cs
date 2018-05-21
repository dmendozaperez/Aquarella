using System;
using System.Collections;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using CrystalDecisions.Shared;
//using Microsoft.Practices.EnterpriseLibrary.Data;
//using Bata.Aquarella.BLL.Conexion;

namespace www.aquarella.com.pe.Reports.Logistica
{
    public partial class reportLiquidation : System.Web.UI.Page
    {
        Users _user;

        /// <summary>
        /// Array que contienen los objetos de la clase que resivira el reporte crystal
        /// </summary>
        private ArrayList _liqValsReport;

        private ArrayList _liqValsSubReport;

        private ArrayList _liqValsPagoSubReport;

        /// <summary>
        /// Instanciar Datasource de reporte crystal
        /// </summary>
        private ReportDocument _liqObjReport;

        //private ReportDocument articlesReturnedObjectsReport;

        /// <summary>
        /// Numero de pedido de liquidacion
        /// </summary>
        string _noLiq;

        /// <summary>
        /// Direccion del archivo de crystal report
        /// </summary>
        string reportPath;        

        string _nameSessionData = "liquidationValues";

        /// <summary>
        /// Nombre del archivo de reporte de crystal
        /// </summary>
        string _nameFileCrystalReport = "liquidationReport.rpt";

        /// <summary>
        /// Load de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        //public static string _conn = Constants.OrcleStringConn;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session,Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            _noLiq = Request.Params["NoLiq"];

            //
            if (!string.IsNullOrEmpty(_noLiq))
            {
                // REPORTE GENERALIZADO DE LIQUIDACION
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
                   // return;

                    //Database db = DatabaseFactory.CreateDatabase(_conn);
                    // Microsoft.Practices.EnterpriseLibrary.Data.Database db = DatabaseFactory.CreateDatabase(_conn);

                    //  string[] vconexion= rptconexion.fconexion(db.ConnectionString);

                    // _liqObjReport.OpenSubreport("pagonc");

                    _liqObjReport.OpenSubreport("pagonc").SetDataSource(_liqValsSubReport);
                _liqObjReport.OpenSubreport("pagoforma").SetDataSource(_liqValsPagoSubReport);


                    // _liqObjReport.SetDatabaseLogon(vconexion[0], vconexion[1], vconexion[2], "");

                    //ConnectionInfo vconexion=new ConnectionInfo();
                    //liquidationReport rpt=new liquidationReport();
                    //Tables mytables = _liqObjReport.Database.Tables;
                    //vconexion.ServerName = "(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=dbdesarrollo.cp2ge5nh5bkk.us-east-1.rds.amazonaws.com)(PORT=1833)))            (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=DBDVLP)))";
                    //vconexion.DatabaseName = "";
                    //vconexion.UserID = "userinternet12";
                    //vconexion.Password = "userinternet12";

                    //foreach (CrystalDecisions.CrystalReports.Engine.Table mytable in mytables)
                    //{
                    //    TableLogOnInfo myTableLogonInfo = mytable.LogOnInfo;
                    //    myTableLogonInfo.ConnectionInfo = vconexion;
                    //    mytable.ApplyLogOnInfo(myTableLogonInfo);
                    //}






                    //Objeto crystal reports presente en la pagina aspx
                   // _liqObjReport = null;
                crvLiquidation.ReportSource = _liqObjReport;
          
                //crvLiquidation.RefreshReport();
                    
                

                //---------------------------------------------------------------------------
                //articlesReturnedObjectsReport = new ReportDocument();
                //articlesReturnedObjectsReport.Load(reportPath);
                //articlesReturnedObjectsReport.SetDataSource(_liqValsReport);
                //// //articlesReturnedObjectsReport.PrintOptions.PrinterName = "\\\\pc161-Rsilva\\GuiaNota2";
                //articlesReturnedObjectsReport.PrintOptions.PrinterName = "\\\\10.10.10.161\\GuiaNota2";
                //articlesReturnedObjectsReport.PrintToPrinter(1, false, 0, 0);
                //-----------------------------------------------------------------------------

                //String nombreArchivo = "ReportArticlesReturnedDev_" + _noLiq;
                //articlesReturnedObjectsReport.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, nombreArchivo);

                //PrintDocument pdoc = new PrintDocument();
                //pdoc.PrintPage += new PrintPageEventHandler();
                //pdoc.PrinterSettings.PrinterName = "\\\\pc161-Rsilva\\GuiaNota2";
                //pdoc.Print();       

                //---------------------------------------------------------------------------



                //articlesReturnedObjectsReport = new ReportDocument();
                //articlesReturnedObjectsReport.Load(reportPath, OpenReportMethod.OpenReportByDefault);
                //articlesReturnedObjectsReport.SetDatabaseLogon("awsuser", "awsBataPWD", "dbaux.cp2ge5nh5bkk.us-east-1.rds.amazonaws.com", "BATAPERU");
                //articlesReturnedObjectsReport.SetDataSource(_liqValsReport);
                //articlesReturnedObjectsReport.PrintToPrinter(1, false, 0, 0);

                //----------------------------------------------------------------------------

            }
                }
            catch(Exception exc)
            {
                //msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }

        }

        protected void PopulateValueCrystalReport()
        {
            if (Session[_nameSessionData] == null)
            {
                _liqValsReport = new ArrayList();

                DataSet dsLiqInfo = Liquidations_Hdr.getLiquidationHdrInfo( _noLiq);

                if (dsLiqInfo == null)
                    return;

                //DataSet dsLiqDtl =  Liquidation_Dtl.getLiquidationDtl(_user._usv_co, _noLiq);
                DataSet dsLiqDtl = new DataSet();
                dsLiqDtl.Tables.Add(dsLiqInfo.Tables[1].Copy());

                if (dsLiqDtl == null)
                    return;

                DataRow dRow = dsLiqInfo.Tables[0].Rows[0];

                foreach (DataRow dRowDtl in dsLiqDtl.Tables[0].Rows)
                {
                    string vncredito = ""; decimal VtotalcreditoTotal = 0;
                    string vfecha = DateTime.Today.ToString("dd/MM/yyyy");



                    //Bata.Aquarella.BLL.Reports.Liquidation objLiqReport = new BLL.Reports.Liquidation(dRow["ohv_warehouseid"].ToString(), dRow["wav_description"].ToString(),
                    //    dRow["wav_address"].ToString(), dRow["wav_telephones"].ToString(), dRow["ubicationwav"].ToString(), dRow["con_coordinator_id"].ToString(), dRow["bdv_document_no"].ToString(),
                    //    dRow["name"].ToString(), dRow["bdv_address"].ToString(), dRow["bdv_phone"].ToString(), dRow["bdv_movil_phone"].ToString(), dRow["bdv_email"].ToString(),
                    //    dRow["ubicationcustomer"].ToString(), dRow["lhv_liquidation_no"].ToString(), Convert.ToDateTime(dRow["lhd_date"]), Convert.ToDateTime(dRow["lhd_expiration_date"].ToString()),
                    //    dRow["stv_description"].ToString(), Convert.ToDecimal(dRow["lon_disscount"]), Convert.ToDecimal(dRow["tax_rate"]), Convert.ToDecimal(dRow["lhn_tax_rate"]), Convert.ToDecimal(dRow["lhn_handling"]),
                    //    dRowDtl["ldv_article"].ToString(), dRowDtl["brv_description"].ToString(), dRowDtl["cov_description"].ToString(), dRowDtl["arv_name"].ToString(), dRowDtl["ldv_size"].ToString(), Convert.ToDecimal(dRowDtl["ldn_qty"]),
                    //    Convert.ToDecimal(dRowDtl["ldn_sell_price"]), Convert.ToDecimal(dRowDtl["ldn_commission"]), Convert.ToDecimal(dRowDtl["ldn_disscount"]), Convert.ToDecimal(dRow["percepcion"]), Convert.ToDecimal(dRow["porc_percepcion"]),
                    //    Convert.ToDecimal(dRow["ncredito"]), vncredito, Convert.ToDateTime(vfecha), VtotalcreditoTotal, _noLiq, Convert.ToDecimal(dRow["totalop"]));


                    www.aquarella.com.pe.bll.Reports.Liquidation objLiqReport = new www.aquarella.com.pe.bll.Reports.Liquidation("1", dRow["almacen"].ToString(),
                     dRow["alm_direccion"].ToString(), dRow["Alm_Telefono"].ToString(), "", dRow["Bas_Id"].ToString(), dRow["Bas_Documento"].ToString(),
                     dRow["nombres"].ToString(), dRow["Bas_Direccion"].ToString(), dRow["Bas_Telefono"].ToString(), dRow["Bas_Celular"].ToString(), dRow["Bas_Correo"].ToString(),
                     dRow["ubicacion"].ToString(), dRow["Liq_Id"].ToString(), Convert.ToDateTime(dRow["Liq_FechaIng"]), Convert.ToDateTime(dRow["Liq_Fecha_Expiracion"].ToString()),
                     dRow["estado"].ToString(), 0, Convert.ToDecimal(dRow["igvporc"]), Convert.ToDecimal(dRow["igvmonto"]), 0,
                     dRowDtl["Art_Id"].ToString(), dRowDtl["Mar_Descripcion"].ToString(), dRowDtl["Col_Descripcion"].ToString(), dRowDtl["art_descripcion"].ToString(), dRowDtl["Liq_Det_TalId"].ToString(), Convert.ToDecimal(dRowDtl["Liq_Det_Cantidad"]),
                     Convert.ToDecimal(dRowDtl["Liq_Det_Precio"]), Convert.ToDecimal(dRowDtl["Liq_Det_Comision"]), 0, Convert.ToDecimal(dRow["Percepcionm"]), Convert.ToDecimal(dRow["Percepcionp"]),
                     Convert.ToDecimal(dRow["ncredito"]), vncredito, Convert.ToDateTime(vfecha), VtotalcreditoTotal, _noLiq, Convert.ToDecimal(dRow["totalop"]),Convert.ToDecimal(dRowDtl["Liq_Det_OfertaM"]), dRow["Opg"].ToString());

                    _liqValsReport.Add(objLiqReport);
                }




                _liqValsSubReport = new ArrayList();

                //DataSet dsLiqpagoInfo = Liquidations_Hdr.getpagoncreditoliqui(_noLiq);
                DataSet dsLiqpagoInfo = new DataSet();
                dsLiqpagoInfo.Tables.Add(dsLiqInfo.Tables[2].Copy());

                if (dsLiqpagoInfo == null)
                    return;

                foreach (DataRow dRowDtl in dsLiqpagoInfo.Tables[0].Rows)
                {
                    string vncredito = dRowDtl["ncredito"].ToString();
                    decimal VtotalcreditoTotal = Convert.ToDecimal(dRowDtl["Total"].ToString());
                    DateTime vfecha = Convert.ToDateTime(dRowDtl["fecha"].ToString());




                    www.aquarella.com.pe.bll.Reports.LiqNcSubinforme objLiqpagoReport = new www.aquarella.com.pe.bll.Reports.LiqNcSubinforme("", vncredito, vfecha, VtotalcreditoTotal);

                    _liqValsSubReport.Add(objLiqpagoReport);
                }


                _liqValsPagoSubReport = new ArrayList();
                //DataSet dsLiqpagoformainfo = Liquidations_Hdr.getpagonformaliqui(_noLiq);
                DataSet dsLiqpagoformainfo = new DataSet();
                dsLiqpagoformainfo.Tables.Add(dsLiqInfo.Tables[3].Copy());
                if (dsLiqpagoformainfo == null)
                    return;
                foreach (DataRow drowdtl in dsLiqpagoformainfo.Tables[0].Rows)
                {
                    string vpago = drowdtl["pago"].ToString();
                    string vdocumento = drowdtl["Documento"].ToString();
                    DateTime vfecha = Convert.ToDateTime(drowdtl["fecha"].ToString());
                    Decimal vtotal = Convert.ToDecimal(drowdtl["Total"].ToString());
                    www.aquarella.com.pe.bll.Reports.VentaPagoSubInforme objLiqpagoformaReport = new bll.Reports.VentaPagoSubInforme(vpago, vdocumento, vfecha, vtotal);
                    _liqValsPagoSubReport.Add(objLiqpagoformaReport);
                }
            
            }
            else
            {
                _liqValsReport = (ArrayList)Session[_nameSessionData];
                _liqValsSubReport = (ArrayList)Session[_nameSessionData];
                _liqValsPagoSubReport = (ArrayList)Session[_nameSessionData];
            }
        }

        /// <summary>
        /// Liberar el objeto de crystal para evitar errores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Unload(object sender, EventArgs e)
        {
            /// Reporte generalizado
            if ((_liqObjReport != null) && _liqObjReport.IsLoaded)
            {
                _liqObjReport.Close();
                _liqObjReport.Dispose();
            }
        }
    }
}