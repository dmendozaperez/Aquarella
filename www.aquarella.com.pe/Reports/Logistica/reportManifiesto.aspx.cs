using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Logistica;
namespace www.aquarella.com.pe.Reports.Logistica
{
    public partial class reportManifiesto : System.Web.UI.Page
    {
        Users _user;
        string _noManifiesto;
        string _nameSessionData = "manifiestoValues";
        private ArrayList _ManValsReport;
        string reportPath;
        string _nameFileCrystalReport = "manifiestoReport.rpt";
        private ReportDocument _ManObjReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Vencimiento de sesion
                if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
                else
                    _user = (Users)Session[Constants.NameSessionUser];

                _noManifiesto = Request.Params["NoMan"];

                //
                if (!string.IsNullOrEmpty(_noManifiesto))
                {
                    // REPORTE GENERALIZADO DE LIQUIDACION
                    PopulateValueCrystalReport();

                    //// Ubicacion del reporte crystal
                    reportPath = Server.MapPath(_nameFileCrystalReport);

                    ////Instanciar el objeto de reporte de crystal
                    _ManObjReport = new ReportDocument();

                    ////Enlazar el archivo del reporte y el objeto instanciado
                    _ManObjReport.Load(reportPath);

                    ////Establecer el dataSource dirigido al reporte crystal
                    _ManObjReport.SetDataSource(_ManValsReport);

                    ////Objeto crystal reports presente en la pagina aspx
                    crvmanifiesto.ReportSource = _ManObjReport;



                }
            }
            catch (Exception exc)
            { }
        }
        protected void PopulateValueCrystalReport()
        {
            if (Session[_nameSessionData] == null)
            {
                _ManValsReport = new ArrayList();

                DataSet dsManInfo = ManifiestoBll._reporte_manifiesto(Convert.ToDecimal(_noManifiesto));

                if (dsManInfo == null)
                    return;

                //DataSet dsLiqDtl =  Liquidation_Dtl.getLiquidationDtl(_user._usv_co, _noLiq);
                DataSet dsManDtl = new DataSet();
                dsManDtl.Tables.Add(dsManInfo.Tables[1].Copy());

                if (dsManDtl == null)
                    return;

                DataRow dRow = dsManInfo.Tables[0].Rows[0];

                foreach (DataRow dRowDtl in dsManDtl.Tables[0].Rows)
                {
                    Decimal idman = Convert.ToDecimal(dRow["IdManifiesto"].ToString());
                    DateTime fechaman= Convert.ToDateTime(dRow["Man_Fecha"]);
                    string guiaman= dRowDtl["guia"].ToString();
                    string docman= dRowDtl["doc"].ToString();
                    string liderman= dRowDtl["lider"].ToString();
                    string promotorman= dRowDtl["promotor"].ToString();
                    Int32 paresman=Convert.ToInt32(dRowDtl["pares"]);
                    string agenciaman= dRowDtl["agencia"].ToString();
                    string destinoman= dRowDtl["destino"].ToString();
                    string tipo_despacho= dRowDtl["Desp_Des"].ToString();

                    www.aquarella.com.pe.bll.Reports.Manifiesto_Reports objManReport = new www.aquarella.com.pe.bll.Reports.Manifiesto_Reports(idman, fechaman, guiaman,
                        docman, liderman, promotorman, paresman, agenciaman, destinoman, tipo_despacho);

                    _ManValsReport.Add(objManReport);
                }
                           
              
            
            }
            else
            {
                _ManValsReport = (ArrayList)Session[_nameSessionData];               
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            /// Reporte generalizado
            if ((_ManObjReport != null) && _ManObjReport.IsLoaded)
            {
                _ManObjReport.Close();
                _ManObjReport.Dispose();
            }
        }
    }
}