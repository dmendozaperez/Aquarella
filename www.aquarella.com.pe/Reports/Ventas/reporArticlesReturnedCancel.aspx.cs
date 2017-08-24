using System;
using System.Collections;
using System.Data;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Logistica;
//using Bata.Aquarella.BLL.Logistica;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll.Ventas;
//using Bata.Aquarella.BLL.Ventas;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace www.aquarella.com.pe.Reports.Ventas
{
    public partial class reporArticlesReturnedCancel : System.Web.UI.Page
    {
        Users _user;

        /// <summary>
        /// Array que contienen los objetos de la clase que resivira el reporte crystal
        /// </summary>
        private ArrayList articlesReturnedValues;

        /// <summary>
        /// Instanciar reporte crystal
        /// </summary>
        private ReportDocument articlesReturnedObjectsReport;


        /// <summary>
        /// Numero de la factura
        /// </summary>
        string _noReturn;
        string _noNota;


        /// <summary>
        /// Direccion del archivo de crystal report
        /// </summary>
        string reportPath;

        /// <summary>
        /// Consulta de devoluciones DEA devolucion espera de aprobacion
        /// </summary>
        string _status;

        /// <summary>
        /// Nombre de la session q contiene los datos de la facturacion
        /// </summary>
        string _nombreSession = "ArticlesReturnedValues";

        /// <summary>
        /// Direccion del archivo crystal
        /// </summary>
        //string _pathFile = "ArticlesReturnedReport.rpt";
        String _pathFile = "reportArticlesReturnedDev.rpt";

        /// <summary>
        /// Inicio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Vencimiento de sesion
                if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
                else
                    _user = (Users)Session[Constants.NameSessionUser];

                ///
                if (!IsPostBack)
                {
                    Session[_nombreSession] = null;
                }

                //
                if (Request.Params["noReturn"] != null && Request.Params["noNota"] != null)
                {
                    _noReturn = Request.Params["noReturn"].ToString();
                    _noNota = Request.Params["noNota"].ToString();
                }
                else
                    _noReturn = "-1";

                // Estado de devolucion
                if (Request.Params["st"] != null)
                    _status = Request.Params["st"].ToString();
                else
                    _status = string.Empty;

                ///exportOpts.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableD ocFormat

                /// Realizar la consulta de informacion que se presentara en el reporte
                PopulateValueCrystalReportI();

                /// Ubicacion del reporte crystal
                reportPath = Server.MapPath(_pathFile);

                /// Instanciar el objeto de reporte de crystal
                articlesReturnedObjectsReport = new ReportDocument();

                ///// Enlazar el archivo del reporte y el objeto instanciado
                articlesReturnedObjectsReport.Load(reportPath);
                ///// Establecer el dataSource dirigido al reporte crystal
                articlesReturnedObjectsReport.SetDataSource(articlesReturnedValues);

                // if (Request.Params["ShowReportOnWeb"] != null) { }
                // else
                //{
                //string nombreArchivo = "ReportArticlesReturned_" + _noReturn;
                //String nombreArchivo = "ReportArticlesReturnedDev_" + _noReturn;
                String nombreArchivo = "ReportArticlesReturnedDev_" + _noReturn;

                /// Abrir automaticamente el reporte.
                ///  ExportFormatType.NoFormat
                //articlesReturnedObjectsReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, nombreArchivo);

                //articlesReturnedObjectsReport.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, nombreArchivo);
                //articlesReturnedObjectsReport.ExportToDisk(ExportFormatType.WordForWindows, nombreArchivo);

                //--------------------------------------------------------------------------------------------------------

                // -------Report rpt = new Report("", "systema", "oracle", "batadb", articlesReturnedObjectsReport);
                //articlesReturnedObjectsReport.PrintOptions.PrinterName = "\\\\pc161-Rsilva\\GuiaNota2";
                //articlesReturnedObjectsReport.PrintOptions.PrinterName = "\\\\10.10.10.161\\GuiaNota2";
                //articlesReturnedObjectsReport.PrintToPrinter(1, false, 0, 0);

                articlesReturnedObjectsReport.ExportToHttpResponse(ExportFormatType.WordForWindows, Response, true, nombreArchivo);

                //--------------------------------------------------------------------------------------------------------

                //}

                /// Objeto crystal que hace q se visualice en crystal en la  pagina aspx
                // CrystalReportArticlesReturned.ReportSource = articlesReturnedObjectsReport;
            }
            catch (Exception)
            {
                //throw new Exception(msgErr.Message, msgErr.InnerException);
                //Response.Redirect("panelReturns.aspx");
            }
        }

        /// <summary>
        /// Liberar el objeto de crystal para evitar errores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Unload(object sender, EventArgs e)
        {
            if ((articlesReturnedObjectsReport != null) && articlesReturnedObjectsReport.IsLoaded)
            { 
                articlesReturnedObjectsReport.Close();
                articlesReturnedObjectsReport.Dispose();
            }
        }


        public void PopulateValueCrystalReportI()
        {
            if (Session[_nombreSession] == null)
            {

                //**crear nota de credito la copia 
                string nc_id = Returns_Hdr.ejecuta_duplica_nc(_user._bas_id, Convert.ToDecimal(_noReturn));
                DataSet ds_nc = Returns_Hdr.getRetunrHdr(nc_id);

                // Cosultar la cabecera de la factura
                DataTable dtReturnHdr = new DataTable();

                if (string.IsNullOrEmpty(_status))
                    //dtReturnHdr = Returns_Hdr.getRetunrHdr(_user._usv_co, _noReturn);
                    dtReturnHdr = ds_nc.Tables[0].Copy();
                else if (_status.Equals(Constants.StatusReturnForAprob))
                    dtReturnHdr = Returns_Hdr.getRetunrHdrDea(_user._usv_co, _noReturn);

                if (dtReturnHdr.Rows.Count > 0)
                {
                    /// Crear el array list con el cual se le va a pasar toda la informacion al archivo de crystal
                    articlesReturnedValues = new ArrayList();

                    //
                    string idCoordinador = Convert.ToString(dtReturnHdr.Rows[0]["Not_BasId"]);
                    //
                    string coordEmail = Convert.ToString(dtReturnHdr.Rows[0]["Bas_Correo"]);
                    //
                    string nameCoord = Convert.ToString(dtReturnHdr.Rows[0]["nombres"]);
                    //
                    string coordDocument = Convert.ToString(dtReturnHdr.Rows[0]["Documento"]);
                    //
                    string coordAdress = Convert.ToString(dtReturnHdr.Rows[0]["Bas_Direccion"]);
                    //
                    string coordPhone = Convert.ToString(dtReturnHdr.Rows[0]["Bas_Telefono"]);
                    //
                    string coordUbication = Convert.ToString(dtReturnHdr.Rows[0]["ubicacion"]);
                    //
                    DateTime dateReturn = Convert.ToDateTime(dtReturnHdr.Rows[0]["Not_Fecha"]);
                    //
                    string rhvTransaction = "";// dtReturnHdr.Rows[0]["rhv_transaction"].ToString();
                    //
                    string rhnPerson = dtReturnHdr.Rows[0]["Not_BasId"].ToString();
                    //
                    string rhnEmployee = "";// dtReturnHdr.Rows[0]["rhn_employee"].ToString();
                    //PERCEPCION
                    Decimal percepcion = 0;//Convert.ToDecimal(dtReturnHdr.Rows[0]["percepcion"]);
                    //
                    Decimal porc_percepcion = 0;//Convert.ToDecimal(dtReturnHdr.Rows[0]["porc_percepcion"]);

                    string _direccionfiscal = "";//dtReturnHdr.Rows[0]["direccionf"].ToString();

                    // Id Bodega, para consultar la info de la bodega donde se genera la factura
                    //string stvWarehouse = "";// dtReturnHdr.Rows[0]["stv_warehouse"].ToString();
                    String mcpConcepto = dtReturnHdr.Rows[0]["Con_Descripcion"].ToString();
                    String varNotaCredito = dtReturnHdr.Rows[0]["notaCredito"].ToString();

                    decimal varSubTotal = Convert.ToDecimal(dtReturnHdr.Rows[0]["SUBTOTAL"]);
                    decimal varIGV = Convert.ToDecimal(dtReturnHdr.Rows[0]["IGV"]);
                    decimal varTotal = Convert.ToDecimal(dtReturnHdr.Rows[0]["TOTAL"]);

                    ///
                    //warehouses objW = new warehouses(_user._usv_co, stvWarehouse);
                    //DataTable dtInfoWarehouse = objW.getWarehouseByPk();

                    string wavDescription = "";
                    string wavAddress = "";
                    string wavPhone = "";
                    string wavUbication = "";
                    //
                    //if (dtInfoWarehouse != null && dtInfoWarehouse.Rows.Count > 0)
                    //{
                    ///
                    wavDescription = dtReturnHdr.Rows[0]["Alm_Descripcion"].ToString().ToUpper();
                    wavAddress = dtReturnHdr.Rows[0]["Alm_Direccion"].ToString();
                    wavPhone = dtReturnHdr.Rows[0]["Alm_Telefono"].ToString();
                    wavUbication = "";// dtReturnHdr.Rows[0]["ubication"].ToString();
                    //}

                    // Consultar los datos de la persona a quien se le facturo
                    //DataTable dtInfoPersonInvoiced = Basic_Data.searchPerson(_user._usv_co, "-1", "", rhnPerson, "", "").Tables[0];

                    string facturadoDestinatario = "";
                    string facturadoUbicacion = "";
                    string facturadoTelefono = "";

                    ///
                    //if (dtInfoPersonInvoiced != null && dtInfoPersonInvoiced.Rows.Count > 0)
                    // {
                    ///
                    facturadoDestinatario = dtReturnHdr.Rows[0]["nombres"].ToString();
                    facturadoUbicacion = dtReturnHdr.Rows[0]["ubicacion"].ToString();
                    facturadoTelefono = dtReturnHdr.Rows[0]["Bas_Telefono"].ToString();
                    // }

                    // DETALLE DE LA DEVOLUCION
                    //DataTable dtReturnDtl = Returns_Dtl.getRetunrDtl(_user._usv_co, _noReturn);
                    DataTable dtReturnDtl = new DataTable();
                    dtReturnDtl = ds_nc.Tables[1].Copy();

                    //
                    foreach (DataRow drLine in dtReturnDtl.Rows)
                    {
                        //
                        string rhvInvoice = drLine["ven_id"].ToString();
                        //
                        string article = drLine["Not_Det_ArtId"].ToString();
                        //
                        string articleName = drLine["articulo"].ToString();
                        //
                        string articleColor = drLine["color"].ToString();
                        //
                        string size = drLine["Not_Det_TalId"].ToString();
                        //
                        decimal qty = Convert.ToDecimal(drLine["Not_Det_Cantidad"]);
                        //
                        decimal sellPrice = Convert.ToDecimal(drLine["Not_Det_Precio"]);
                        //
                        decimal disscountLin = 0;// Convert.ToDecimal(drLine["rdn_disscount_lin"]);
                        //
                        decimal commision = Convert.ToDecimal(drLine["Not_Det_ComisionM"]);
                        //
                        decimal handling = 0;// Convert.ToDecimal(drLine["rdn_handling"]);
                        //
                        decimal disscountGen = 0;// Convert.ToDecimal(drLine["rdn_disscount_gen"]);
                        //
                        decimal taxes = Convert.ToDecimal(drLine["igvm"]);

                        DateTime FecEmisionFactura = Convert.ToDateTime(drLine["FecEmisionFactura"]);

                        //
                        ReporteDevolucion objAR = new ReporteDevolucion(idCoordinador, coordEmail, nameCoord,
                            coordDocument, coordAdress, coordPhone, coordUbication, facturadoDestinatario,
                            facturadoUbicacion, facturadoTelefono, wavDescription, wavAddress,
                            wavPhone, wavUbication, _user._usv_co, _noReturn, dateReturn, rhvTransaction,
                            rhnPerson, rhnEmployee, rhvInvoice, article, articleName, articleColor, size, qty, sellPrice, disscountLin,
                            commision, handling, disscountGen, taxes, mcpConcepto, FecEmisionFactura, varNotaCredito, varSubTotal, varIGV, varTotal, percepcion, porc_percepcion, _direccionfiscal);

                        // Adicionar el nuevo objeto al arreglo
                        articlesReturnedValues.Add(objAR);
                    }
                }
                //
                Session[_nombreSession] = articlesReturnedValues;
            }
            else
            {
                articlesReturnedValues = (ArrayList)Session[_nombreSession];
            }
        }
    }
}