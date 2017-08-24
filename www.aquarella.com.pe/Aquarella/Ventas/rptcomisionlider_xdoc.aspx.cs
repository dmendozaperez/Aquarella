using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.Bll.Ventas;

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class rptcomisionlider_xdoc : System.Web.UI.Page
    {
        Users _user;
        string _bdv_area_id;
        string _fechainicio;
        string _fechafinal;
        string _lider;
        string _asesor;
        string _comision;
        string _nameSessionData = "comisionValues";
        private ArrayList _ComValsReport;
        string reportPath;
        string _nameFileCrystalReport = "comisionxdocReport.rpt";
        private ReportDocument _ComObjReport;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Vencimiento de sesion
                if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
                else
                    _user = (Users)Session[Constants.NameSessionUser];

                _bdv_area_id = Request.Params["bdv_area_id"];
                _fechainicio = Request.Params["fechainicio"];
                _fechafinal = Request.Params["fechafinal"];
                _lider = Request.Params["lider"];
                _comision= Request.Params["comision"];
                _asesor = Request.Params["asesor"];
                //
                if (!string.IsNullOrEmpty(_bdv_area_id))
                {
                    // REPORTE GENERALIZADO DE LIQUIDACION
                    PopulateValueCrystalReport();

                    //// Ubicacion del reporte crystal
                    reportPath = Server.MapPath(_nameFileCrystalReport);

                    ////Instanciar el objeto de reporte de crystal
                    _ComObjReport = new ReportDocument();

                    ////Enlazar el archivo del reporte y el objeto instanciado
                    _ComObjReport.Load(reportPath);

                    ////Establecer el dataSource dirigido al reporte crystal
                    _ComObjReport.SetDataSource(_ComValsReport);

                    ////Objeto crystal reports presente en la pagina aspx
                    crvcomisionlider.ReportSource = _ComObjReport;
                    


                }
            }
            catch (Exception exc)
            { }
        }
        protected void PopulateValueCrystalReport()
        {
            if (Session[_nameSessionData] == null)
            {
                _ComValsReport = new ArrayList();

                DataSet dsComInfo = ReporteComisionDetXDoc._reportecomision_XDoc(_bdv_area_id, Convert.ToDateTime(_fechainicio), Convert.ToDateTime(_fechafinal));

                if (dsComInfo == null)
                    return;

                //DataSet dsLiqDtl =  Liquidation_Dtl.getLiquidationDtl(_user._usv_co, _noLiq);
                //DataSet dsManDtl = new DataSet();
                //dsManDtl.Tables.Add(dsComInfo.Tables[1].Copy());

                //if (dsManDtl == null)
                //    return;

                //DataRow dRow = dsComInfo.Tables[0].Rows[0];

                foreach (DataRow dRow in dsComInfo.Tables[0].Rows)
                {                  
                    string cliente = dRow["cliente"].ToString();
                    string ndoc = dRow["ndoc"].ToString();
                    DateTime fechadoc =Convert.ToDateTime(dRow["fechadoc"]);
                    string articulo = dRow["articulo"].ToString();
                    string talla = dRow["talla"].ToString();
                    Int32 tpares = Convert.ToInt32(dRow["tpares"]);
                    Decimal vtotal = Convert.ToDecimal(dRow["vtotal"]);
                    Decimal comlider = Convert.ToDecimal(dRow["comlider"]);
                    string tipodoc= dRow["tipodoc"].ToString();
                    www.aquarella.com.pe.Bll.Ventas.ReporteComisionDetXDoc objComReport =
                    new www.aquarella.com.pe.Bll.Ventas.ReporteComisionDetXDoc(_lider, cliente, ndoc, fechadoc, articulo, talla, tpares, vtotal, comlider,Convert.ToDateTime(_fechainicio),Convert.ToDateTime(_fechafinal), tipodoc,Convert.ToDecimal(_comision),_asesor);
                                            
                    _ComValsReport.Add(objComReport);
                }



            }
            else
            {
                _ComValsReport = (ArrayList)Session[_nameSessionData];
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            /// Reporte generalizado
            if ((_ComObjReport != null) && _ComObjReport.IsLoaded)
            {
                _ComObjReport.Close();
                _ComObjReport.Dispose();
            }
        }
    }
}