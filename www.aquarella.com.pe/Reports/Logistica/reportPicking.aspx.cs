using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using System.Data;
using CrystalDecisions.Shared;

namespace www.aquarella.com.pe.Reports.Logistica
{
    public partial class reportPicking : System.Web.UI.Page
    {
        Users _user;

        /// <summary>
        /// Array que contienen los objetos de la clase que resivira el reporte crystal
        /// </summary>
        private ArrayList _pickValsReport;

        /// <summary>
        /// Instanciar Datasource de reporte crystal
        /// </summary>
        private ReportDocument _pickObjReport;

        /// <summary>
        /// Numero de liquidacion y marcador
        /// </summary>
        string _noLiq, _empPick,_excel;

        /// <summary>
        /// Direccion del archivo de crystal report
        /// </summary>
        string reportPath;

        string _nameSessionData = "pickingValues";

        /// <summary>
        /// Nombre del archivo de reporte de crystal
        /// </summary>
        string _nameFileCrystalReport = "pickingReport.rpt";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            _noLiq = Request.Params["NoLiq"];
            _empPick = Request.Params["EmpPick"];
            if (!(Request.Params["excel"]==null)) _excel = Request.Params["excel"];
            //
            if (!string.IsNullOrEmpty(_noLiq) && !string.IsNullOrEmpty(_empPick))
            {
                // REPORTE GENERALIZADO DE LIQUIDACION
                PopulateValueCrystalReport();

                // Ubicacion del reporte crystal
                reportPath = Server.MapPath(_nameFileCrystalReport);

                // Instanciar el objeto de reporte de crystal
                _pickObjReport = new ReportDocument();

                // Enlazar el archivo del reporte y el objeto instanciado
                _pickObjReport.Load(reportPath);

                // Establecer el dataSource dirigido al reporte crystal
                _pickObjReport.SetDataSource(_pickValsReport);

                // Para cargar el reporte directamente en un pdf o mostrarlo en una nueva pagina de html
                if (Request.Params["ShowReportOnWeb"] == null)
                {
                    string nombreArchivo = "Picking_" + _noLiq;
                    // Abrir en pdf automaticamente
                    ///  ExportFormatType.NoFormat
                    ///  
                    if (_excel == "1")
                    {
                        _pickObjReport.ExportToHttpResponse(ExportFormatType.Excel, Response, true, nombreArchivo);
                    }
                    else
                    {
                        _pickObjReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, nombreArchivo);
                    }
                }

                // Objeto crystal reports presente en la pagina aspx
                crvPicking.ReportSource = _pickObjReport;
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
            if ((_pickObjReport != null) && _pickObjReport.IsLoaded)
            {
                _pickObjReport.Close();
                _pickObjReport.Dispose();
            }
        }

        protected void PopulateValueCrystalReport()
        {
            if (Session[_nameSessionData] == null)
            {
                _pickValsReport = new ArrayList();

                DataSet dsLiqInfo = Liquidations_Hdr.getLiquidationHdrInfo(_noLiq);

                if (dsLiqInfo == null || dsLiqInfo.Tables[0].Rows.Count == 0)
                    return;

                DataSet dsPickDtl = Picking.getDtlPicking( _noLiq);

                if (dsPickDtl == null || dsPickDtl.Tables[0].Rows.Count == 0)
                    return;

                DataRow dRow = dsLiqInfo.Tables[0].Rows[0];

                foreach (DataRow dRowDtl in dsPickDtl.Tables[0].Rows)
                {
                    www.aquarella.com.pe.bll.Reports.Picking objPickReport = new www.aquarella.com.pe.bll.Reports.Picking("", dRow["almacen"].ToString(),
                        dRow["alm_direccion"].ToString(), dRow["Alm_Telefono"].ToString(), "", dRow["Bas_Id"].ToString(),
                        dRow["Bas_Documento"].ToString(), dRow["nombres"].ToString(), dRow["Bas_Direccion"].ToString(), dRow["Bas_Telefono"].ToString(),
                        dRow["Bas_Celular"].ToString(), dRow["Bas_Correo"].ToString(), dRow["ubicacion"].ToString(), dRow["Liq_Id"].ToString(),
                        dRow["estado"].ToString(), dRowDtl["tdv_article"].ToString(), dRowDtl["brv_description"].ToString(),
                        string.Empty, dRowDtl["arv_name"].ToString(), dRowDtl["tdv_size"].ToString(), Convert.ToDecimal(dRowDtl["tdn_qty"]), dRowDtl["stv_descriptions"].ToString(),
                        dRowDtl["po"].ToString(), _empPick, dRowDtl["instrucciones"].ToString(), dRow["lider"].ToString());

                    _pickValsReport.Add(objPickReport);
                }
            }
            else
            {
                _pickValsReport = (ArrayList)Session[_nameSessionData];
            }
        }

    }
}