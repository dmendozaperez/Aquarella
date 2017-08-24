using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Reports;
using System.Data;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using System.Collections;
namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class reportventasemanafz : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
  //      private ReportDocument ventasObjectsReport = new ReportDocument();
    //    private string _pathFile = "reportsventasemanalfz.rpt";
        // private string _pathFile = "~//Reports//Ventas//ReporteVenta.rpt";
   //     private string _nombreSession = "Valoresventa";
    //    private ArrayList ventaValues;

        //private string reportPath; 

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

                // sbconsultar();
            }
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }

        protected DataTable get_dtgrupo(DataTable dt)
        {
            DataTable dtgrupo;
            try
            {
                string vped = "";
                dtgrupo=new DataTable();
                dtgrupo.Columns.Add("ped",typeof(string));
                DataRow[] vfila = dt.Select("len(ped)>0");

                for (Int32 i=0 ; i<vfila.Length;++i)
                {
                    if (vped.Length == 0)
                    {
                        vped = vfila[i]["ped"].ToString();
                        dtgrupo.Rows.Add(vped);
                    }
                    else
                    {
                        if (!(vped == vfila[i]["ped"].ToString()))
                        {
                            vped = vfila[i]["ped"].ToString();
                            dtgrupo.Rows.Add(vped);
                        }
                        else
                        {
                          vped = vfila[i]["ped"].ToString();
                        }
                    }
                }
                return dtgrupo;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected DataTable get_dtventasemana()
        {
            //string vped = "";
            Int32 vnroboubcp = 0;
            Int32 vnrovisa = 0;
            Int32 vnronc = 0;
            decimal vmonto = 0;
            DataTable dtsemana=Documents_Trans.get_reportsemana(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text)).Tables[0];// (DataTable)Session[_nameSessionData];
            DataTable dtsemanacopy = new DataTable();
            dtsemanacopy = dtsemana.Clone();
            DataTable dtgrupo = get_dtgrupo(dtsemana);

            for (Int32 i=0 ;i<dtgrupo.Rows.Count;++i)
            {
                vnroboubcp = 0;
                vnrovisa = 0;
                vnronc = 0;
                vmonto = 0;
                DataRow[] vfila = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND MONTOFAC>0" );

                string dtv_clear = vfila[0]["dtv_clear"].ToString();
                string promotor = vfila[0]["promotor"].ToString();
                string dniruc = vfila[0]["dniruc"].ToString();
                string ped = vfila[0]["ped"].ToString();
                string bolfact = vfila[0]["bolfact"].ToString();
                string fechadoc = vfila[0]["fechadoc"].ToString();
                decimal montofac = Convert.ToDecimal(vfila[0]["montofac"].ToString());
                decimal totalpagos = Convert.ToDecimal(vfila[0]["totalpagos"].ToString());
                decimal saldofavor = 0;
                if (!(vfila[0]["totalpagos"] is DBNull))
                {
                    saldofavor=Convert.ToDecimal(vfila[0]["saldofavor"].ToString());
                }

                dtsemanacopy.Rows.Add(dtv_clear,promotor,dniruc,ped,bolfact,fechadoc,montofac);

                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["totalpagos"] = totalpagos;
                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["saldofavor"] = saldofavor;



                //forma de pago con voucher
                DataRow[] vfilanrovoubcp = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND LEN(NROVOUBCP)>0");

                if (vfilanrovoubcp.Length > 0)
                {
                    if (vfilanrovoubcp.Length > 1)
                    {
                        for (Int32 ibcp = 0; ibcp < vfilanrovoubcp.Length; ++ibcp)
                        {

                            if (ibcp == 0)
                            {
                                string nrovoubcp = vfilanrovoubcp[ibcp]["nrovoubcp"].ToString();
                                string fechavoubcp = vfilanrovoubcp[ibcp]["fechavoubcp"].ToString();
                                decimal montovoubcp = Convert.ToDecimal(vfilanrovoubcp[ibcp]["montovoubcp"].ToString());
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nrovoubcp"] = nrovoubcp;
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechavoubcp"] = fechavoubcp;
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montovoubcp"] = montovoubcp;
                            }
                            else
                            {
                                string nrovoubcp = vfilanrovoubcp[ibcp]["nrovoubcp"].ToString();
                                string fechavoubcp = vfilanrovoubcp[ibcp]["fechavoubcp"].ToString();
                                decimal montovoubcp = Convert.ToDecimal(vfilanrovoubcp[ibcp]["montovoubcp"].ToString());
                                dtsemanacopy.Rows.Add();
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nrovoubcp"] = nrovoubcp;
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechavoubcp"] = fechavoubcp;
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montovoubcp"] = montovoubcp; 
                            }

                        }
                    }
                    else
                    {
                        string nrovoubcp = vfilanrovoubcp[0]["nrovoubcp"].ToString();
                        string fechavoubcp = vfilanrovoubcp[0]["fechavoubcp"].ToString();
                        decimal montovoubcp = Convert.ToDecimal(vfilanrovoubcp[0]["montovoubcp"].ToString());
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nrovoubcp"] = nrovoubcp;
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechavoubcp"] = fechavoubcp;
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montovoubcp"] = montovoubcp;
                    }

                }

                //forma de pago con visa
                DataRow[] vfilanrovisa = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND LEN(NROVISA)>0");

                if (vfilanrovisa.Length > 0)
                {
                    if (vfilanrovisa.Length > 1)
                    {
                        for (Int32 ivisa = 0; ivisa < vfilanrovisa.Length; ++ivisa)
                        {

                        }
                    }
                    else
                    {
                        string nrovisa = vfilanrovisa[0]["nrovisa"].ToString();
                        string fechavisa = vfilanrovisa[0]["fechavisa"].ToString();
                        decimal montovisa = Convert.ToDecimal(vfilanrovisa[0]["montovisa"].ToString());
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nrovisa"] = nrovisa;
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechavisa"] = fechavisa;
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montovisa"] = montovisa;
                    }

                }

                //forma de pago con nc
                DataRow[] vfilanronc = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND LEN(NRONC)>0");

                if (vfilanronc.Length > 0)
                {
                    if (vfilanronc.Length > 1)
                    {
                        if (vfilanronc.Length <= vfilanrovoubcp.Length)
                        {

                            for (Int32 inc = 0; inc < vfilanronc.Length; ++inc)
                            {
                                string nronc = vfilanronc[inc]["nronc"].ToString();
                                string fechanc = vfilanronc[inc]["fechanc"].ToString();
                                decimal montonc = Convert.ToDecimal(vfilanronc[inc]["montonc"].ToString());


                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - (inc + 1)]["nronc"] = nronc;
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - (inc + 1)]["fechanc"] = fechanc;
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - (inc + 1)]["montonc"] = montonc;

                                //dtsemanacopy.Rows[vfilanrovoubcp.Length - (inc+1)]["nronc"] = nronc;
                                //dtsemanacopy.Rows[vfilanrovoubcp.Length - (inc + 1)]["fechanc"] = fechanc;
                                //dtsemanacopy.Rows[vfilanrovoubcp.Length - (inc + 1)]["montonc"] = montonc;

                            }
                        }
                        else
                        {
                            Int32 validanc = 0;
                            for (Int32 inc = 0; inc < vfilanronc.Length; ++inc)
                            {

                                if (validanc == 0)
                                {
                                    string nronc = vfilanronc[inc]["nronc"].ToString();
                                    string fechanc = vfilanronc[inc]["fechanc"].ToString();
                                    decimal montonc = Convert.ToDecimal(vfilanronc[inc]["montonc"].ToString());


                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nronc"] = nronc;
                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechanc"] = fechanc;
                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montonc"] = montonc;
                                    validanc += 1;
                                }
                                else
                                {
                                    dtsemanacopy.Rows.Add();
                                    string nronc = vfilanronc[inc]["nronc"].ToString();
                                    string fechanc = vfilanronc[inc]["fechanc"].ToString();
                                    decimal montonc = Convert.ToDecimal(vfilanronc[inc]["montonc"].ToString());


                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nronc"] = nronc;
                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechanc"] = fechanc;
                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montonc"] = montonc;
                                }
                            }

                        }

                        
                    }
                    else
                    {
                        string nronc = vfilanronc[0]["nronc"].ToString();
                        string fechanc = vfilanronc[0]["fechanc"].ToString();
                        decimal montonc = Convert.ToDecimal(vfilanronc[0]["montonc"].ToString());
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nronc"] = nronc;
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechanc"] = fechanc;
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montonc"] = montonc;
                    }

                }


                //forma de pago con saldoant
                DataRow[] vfilasa = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND MONTOSALDOANT>0");

                if (vfilasa.Length > 0)
                {
                    if (vfilasa.Length > 1)
                    {
                        if (vfilasa.Length >= vfilanrovoubcp.Length || vfilasa.Length >= vfilanronc.Length)
                        {
                            Int32 validasa = 0;
                            for (Int32 isa = 0; isa < vfilasa.Length; ++isa)
                            {
                                if (validasa == 0)
                                {
                                    string fechasaldoant = vfilasa[isa]["fechasaldoant"].ToString();
                                    decimal montosaldoant = Convert.ToDecimal(vfilasa[isa]["montosaldoant"].ToString());


                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechasaldoant"] = fechasaldoant;
                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montosaldoant"] = montosaldoant;
                                    validasa += 1;
                                }
                                else
                                {
                                    dtsemanacopy.Rows.Add();
                                    string fechasaldoant = vfilasa[isa]["fechasaldoant"].ToString();
                                    decimal montosaldoant = Convert.ToDecimal(vfilasa[isa]["montosaldoant"].ToString());


                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechasaldoant"] = fechasaldoant;
                                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montosaldoant"] = montosaldoant;
                                }

                            }
                        }
                        else
                        {
                            for (Int32 isa = 0; isa < vfilasa.Length; ++isa)
                            {
                                string fechasaldoant = vfilasa[isa]["fechasaldoant"].ToString();
                                decimal montosaldoant = Convert.ToDecimal(vfilasa[isa]["montosaldoant"].ToString());
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - isa]["fechasaldoant"] = fechasaldoant;
                                dtsemanacopy.Rows[dtsemanacopy.Rows.Count - isa]["montosaldoant"] = montosaldoant;
                            }

                        }
                    }
                    else
                    {
                        string fechasaldoant = vfilasa[0]["fechasaldoant"].ToString();
                        decimal montosaldoant = Convert.ToDecimal(vfilasa[0]["montosaldoant"].ToString());
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechasaldoant"] = fechasaldoant;
                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montosaldoant"] = montosaldoant;
                    }

                }    
            }

            



            //DataTable dtsemanacopy=new DataTable();
            //dtsemanacopy = dtsemana.Clone();

            //if (dtsemana.Rows.Count > 0)
            //{
            //    for (Int32 i = 0; i< dtsemana.Rows.Count ;++i )
            //    {
            //        if (vped.Length > 0)
            //        {
                        
                       

            //        }
            //    }
            //}
            DataRow[] vfilatotal = dtsemana.Select("bolfact='TOTAL'");
            dtsemanacopy.Rows.Add(DBNull.Value, vfilatotal[0]["promotor"].ToString(), DBNull.Value, DBNull.Value, vfilatotal[0]["bolfact"].ToString(), DBNull.Value, Convert.ToDecimal(vfilatotal[0]["montofac"].ToString()));


            return dtsemanacopy;

        }

        protected void sbconsultar()
        {
            try
            {
                //Session[_nameSessionData]= Lider.Lider.fget_afiliados(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text)).Tables[0];
                msnMessage.Visible = false;

                gvReturns.DataSource = get_dtventasemana();
                //gvReturns.DataSource = Documents_Trans.get_reportsemana(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text)).Tables[0];// (DataTable)Session[_nameSessionData];
                gvReturns.DataBind();
                Session[_nameSessionData] = gvReturns.DataSource;
                MergeRows(gvReturns, 4);
            }
            catch(Exception ex)   
            {
               msnMessage.Visible = true;
               msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
               
            }
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
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {

            gvReturns.DataSource = (DataTable)Session[_nameSessionData];
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "VentaSemanalFz";

            Decimal[] _columna_caracter = { 1};

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns, false,_columna_caracter);

            //this.Session[this._nombreSession] = null;
           //sbcargarcrystal();
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable)Session[_nameSessionData];

            gvReturns.DataBind();

            MergeRows(gvReturns,4);
        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
               

                e.Row.Cells[3].ColumnSpan = 3;
                e.Row.Cells[3].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>Datos del Documento Boleta o Factura</b></td></tr><tr><td>Numero</td><td>Fecha</td><td>Monto</td></tr></table>";

                //'Agrupando las dos ultimas columnas (col=3 y col=4)
                

                //e.Row.Cells[3].Visible = false;
                //e.Row.Cells[4].Visible = false;
                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;


                //'Opcion 2: Agregando una tabla generAQUARELLA dinamicamente
                e.Row.Cells[4].ColumnSpan = 3;
                e.Row.Cells[4].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>Deposito del Banco de Credito</b></td></tr><tr><td>Numero</td><td>Fecha</td><td>Monto</td></tr></table>";

                e.Row.Cells[5].ColumnSpan = 3;
                e.Row.Cells[5].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>Deposito de Visa Unica</b></td></tr><tr><td>Numero</td><td>Fecha</td><td>Monto</td></tr></table>";

                e.Row.Cells[6].ColumnSpan = 3;
                e.Row.Cells[6].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>Datos de la Nota de Credito</b></td></tr><tr><td>Numero</td><td>Fecha</td><td>Monto</td></tr></table>";

                e.Row.Cells[7].ColumnSpan = 2;
                e.Row.Cells[7].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=2><b>Saldo Anterior</b></td></tr><tr><td>Fecha</td><td>Monto</td></tr></table>";


              
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "TOTAL")
                {
                    e.Row.BackColor = System.Drawing.Color.Khaki;
                    e.Row.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                }
            }
        }


        #region <METODOS DEL CRYSTAL>


        //private void sbcargarcrystal()
        //{
        //    try
        //    {
        //        this.PopulateValueCrystalReportI();
        //        this.reportPath = this.Server.MapPath(this._pathFile);
        //        //this.reportPath = this._pathFile;
        //        this.ventasObjectsReport = new ReportDocument();
        //        this.ventasObjectsReport.Load(this.reportPath);

        //        this.ventasObjectsReport.SetDataSource((IEnumerable)this.ventaValues);

        //        //if (this.Request.Params["ShowReportOnWeb"] == null)
                
        //        this.ventasObjectsReport.ExportToHttpResponse(ExportFormatType.ExcelRecord , this.Response, true, "VentasSemanal");

        //        // this.ventasObjectsReport.ExportToDisk(ExportFormatType.WordForWindows,"C:/VentasLider.doc");


        //        //this.crvreport.ReportSource = (object)this.ventasObjectsReport;
        //    }
        //    catch
        //    {

        //    }

        //}
        //public void PopulateValueCrystalReportI()
        //{
        //    if (this.Session[this._nombreSession] == null)
        //    {
        //        DataTable dtventa = (DataTable)Session[_nameSessionData]; 
        //        if (dtventa.Rows.Count > 0)
        //        {
        //            this.ventaValues = new ArrayList();

        //            string VDesde = Convert.ToDateTime(txtDateStart.Text).ToShortDateString();
        //            string VHasta = Convert.ToDateTime(txtDateEnd.Text).ToShortDateString();
        //            string vrandofecha = "REPORTE VENTAS SEMANA DEL " + VDesde + " HASTA EL " + VHasta ;
                   
        //            foreach (DataRow dataRow in (InternalDataCollectionBase)dtventa.Rows)
        //            {
        //                string vpromotor = dataRow["promotor"].ToString();
        //                string vdniruc = dataRow["dniruc"].ToString();
        //                string vped = dataRow["ped"].ToString();
        //                string vbolfact = dataRow["bolfact"].ToString();
        //                DateTime vfechadoc = Convert.ToDateTime(dataRow["fechadoc"].ToString());
        //                Decimal vmontofac = Convert.ToDecimal(dataRow["montofac"].ToString());
        //                string vnrovoubcp = dataRow["nrovoubcp"].ToString();
        //                string vfechavoubcp = dataRow["fechavoubcp"].ToString();
        //                decimal vmontovoubcp = Convert.ToDecimal(dataRow["montovoubcp"].ToString()); ;
        //                string vnrovisa = dataRow["nrovisa"].ToString();
        //                string vfechavisa = dataRow["fechavisa"].ToString();
        //                decimal vmontovisa = Convert.ToDecimal(dataRow["montovisa"].ToString());
        //                string vnronc = dataRow["nronc"].ToString();
        //                string vfechanc = dataRow["fechanc"].ToString();
        //                decimal vmontonc = Convert.ToDecimal(dataRow["montonc"].ToString());
        //                string vfechasaldoant = dataRow["fechasaldoant"].ToString();
        //                decimal vmontosaldoant = Convert.ToDecimal(dataRow["montosaldoant"].ToString());
        //                decimal vtotalpagos = Convert.ToDecimal(dataRow["totalpagos"].ToString());
        //                decimal vsaldofavor = Convert.ToDecimal(dataRow["saldofavor"].ToString());
        //                string vdtvclear = dataRow["dtv_clear"].ToString();
        //                this.ventaValues.Add((object)new ReportVentaSemanal(vpromotor,vdniruc,vped,vbolfact,vfechadoc,vmontofac,vnrovoubcp,vfechavoubcp,vmontovoubcp,vnrovisa,vfechavisa,vmontovisa,
        //                    vnronc, vfechanc, vmontonc, vfechasaldoant, vmontosaldoant, vtotalpagos, vsaldofavor, vrandofecha,vdtvclear));
        //            }
        //        }
        //        this.Session[this._nombreSession] = (object)this.ventaValues;
        //    }
        //    else
        //        this.ventaValues = (ArrayList)this.Session[this._nombreSession];
        //}

        protected void Page_Unload(object sender, EventArgs e)
        {
            //if (this.ventasObjectsReport == null || !this.ventasObjectsReport.IsLoaded)
            //    return;
            //this.ventasObjectsReport.Close();
        }
        #endregion
    }
}
