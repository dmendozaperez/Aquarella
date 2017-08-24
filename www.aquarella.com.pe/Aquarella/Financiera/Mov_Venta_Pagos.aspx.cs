using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class Mov_Venta_Pagos : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        protected void Page_Load(object sender, EventArgs e)
        {
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
                dtgrupo = new DataTable();
                dtgrupo.Columns.Add("ped", typeof(string));
                DataRow[] vfila = dt.Select("len(ped)>0");

                for (Int32 i = 0; i < vfila.Length; ++i)
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
            DataTable dtsemana = Documents_Trans.get_movimiento_pago(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text)).Tables[0];// (DataTable)Session[_nameSessionData];
            //DataTable dtsemanacopy = new DataTable();
            //dtsemanacopy = dtsemana.Clone();
            //DataTable dtgrupo = get_dtgrupo(dtsemana);

            //for (Int32 i = 0; i < dtgrupo.Rows.Count; ++i)
            //{
            //    vnroboubcp = 0;
            //    vnrovisa = 0;
            //    vnronc = 0;
            //    vmonto = 0;
            //    DataRow[] vfila = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND MONTOFAC>0");

            //    string dtv_clear = vfila[0]["dtv_clear"].ToString();
            //    string promotor = vfila[0]["promotor"].ToString();
            //    string dniruc = vfila[0]["dniruc"].ToString();
            //    string ped = vfila[0]["ped"].ToString();
            //    string bolfact = vfila[0]["bolfact"].ToString();
            //    string fechadoc = vfila[0]["fechadoc"].ToString();
            //    decimal montofac = Convert.ToDecimal(vfila[0]["montofac"].ToString());
            //    decimal totalpagos = Convert.ToDecimal(vfila[0]["totalpagos"].ToString());
            //    decimal saldofavor = 0;
            //    if (!(vfila[0]["totalpagos"] is DBNull))
            //    {
            //        saldofavor = Convert.ToDecimal(vfila[0]["saldofavor"].ToString());
            //    }

            //    dtsemanacopy.Rows.Add(dtv_clear, promotor, dniruc, ped, bolfact, fechadoc, montofac);

            //    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["totalpagos"] = totalpagos;
            //    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["saldofavor"] = saldofavor;



            //    //forma de pago con voucher
            //    DataRow[] vfilanrovoubcp = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND LEN(NROVOUBCP)>0");

            //    if (vfilanrovoubcp.Length > 0)
            //    {
            //        if (vfilanrovoubcp.Length > 1)
            //        {
            //            for (Int32 ibcp = 0; ibcp < vfilanrovoubcp.Length; ++ibcp)
            //            {

            //                if (ibcp == 0)
            //                {
            //                    string nrovoubcp = vfilanrovoubcp[ibcp]["nrovoubcp"].ToString();
            //                    string fechavoubcp = vfilanrovoubcp[ibcp]["fechavoubcp"].ToString();
            //                    decimal montovoubcp = Convert.ToDecimal(vfilanrovoubcp[ibcp]["montovoubcp"].ToString());
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nrovoubcp"] = nrovoubcp;
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechavoubcp"] = fechavoubcp;
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montovoubcp"] = montovoubcp;
            //                }
            //                else
            //                {
            //                    string nrovoubcp = vfilanrovoubcp[ibcp]["nrovoubcp"].ToString();
            //                    string fechavoubcp = vfilanrovoubcp[ibcp]["fechavoubcp"].ToString();
            //                    decimal montovoubcp = Convert.ToDecimal(vfilanrovoubcp[ibcp]["montovoubcp"].ToString());
            //                    dtsemanacopy.Rows.Add();
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nrovoubcp"] = nrovoubcp;
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechavoubcp"] = fechavoubcp;
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montovoubcp"] = montovoubcp;
            //                }

            //            }
            //        }
            //        else
            //        {
            //            string nrovoubcp = vfilanrovoubcp[0]["nrovoubcp"].ToString();
            //            string fechavoubcp = vfilanrovoubcp[0]["fechavoubcp"].ToString();
            //            decimal montovoubcp = Convert.ToDecimal(vfilanrovoubcp[0]["montovoubcp"].ToString());
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nrovoubcp"] = nrovoubcp;
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechavoubcp"] = fechavoubcp;
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montovoubcp"] = montovoubcp;
            //        }

            //    }

            //    //forma de pago con visa
            //    DataRow[] vfilanrovisa = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND LEN(NROVISA)>0");

            //    if (vfilanrovisa.Length > 0)
            //    {
            //        if (vfilanrovisa.Length > 1)
            //        {
            //            for (Int32 ivisa = 0; ivisa < vfilanrovisa.Length; ++ivisa)
            //            {

            //            }
            //        }
            //        else
            //        {
            //            string nrovisa = vfilanrovisa[0]["nrovisa"].ToString();
            //            string fechavisa = vfilanrovisa[0]["fechavisa"].ToString();
            //            decimal montovisa = Convert.ToDecimal(vfilanrovisa[0]["montovisa"].ToString());
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nrovisa"] = nrovisa;
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechavisa"] = fechavisa;
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montovisa"] = montovisa;
            //        }

            //    }

            //    //forma de pago con nc
            //    DataRow[] vfilanronc = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND LEN(NRONC)>0");

            //    if (vfilanronc.Length > 0)
            //    {
            //        if (vfilanronc.Length > 1)
            //        {
            //            if (vfilanronc.Length <= vfilanrovoubcp.Length)
            //            {

            //                for (Int32 inc = 0; inc < vfilanronc.Length; ++inc)
            //                {
            //                    string nronc = vfilanronc[inc]["nronc"].ToString();
            //                    string fechanc = vfilanronc[inc]["fechanc"].ToString();
            //                    decimal montonc = Convert.ToDecimal(vfilanronc[inc]["montonc"].ToString());


            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - (inc + 1)]["nronc"] = nronc;
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - (inc + 1)]["fechanc"] = fechanc;
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - (inc + 1)]["montonc"] = montonc;

            //                    //dtsemanacopy.Rows[vfilanrovoubcp.Length - (inc+1)]["nronc"] = nronc;
            //                    //dtsemanacopy.Rows[vfilanrovoubcp.Length - (inc + 1)]["fechanc"] = fechanc;
            //                    //dtsemanacopy.Rows[vfilanrovoubcp.Length - (inc + 1)]["montonc"] = montonc;

            //                }
            //            }
            //            else
            //            {
            //                Int32 validanc = 0;
            //                for (Int32 inc = 0; inc < vfilanronc.Length; ++inc)
            //                {

            //                    if (validanc == 0)
            //                    {
            //                        string nronc = vfilanronc[inc]["nronc"].ToString();
            //                        string fechanc = vfilanronc[inc]["fechanc"].ToString();
            //                        decimal montonc = Convert.ToDecimal(vfilanronc[inc]["montonc"].ToString());


            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nronc"] = nronc;
            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechanc"] = fechanc;
            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montonc"] = montonc;
            //                        validanc += 1;
            //                    }
            //                    else
            //                    {
            //                        dtsemanacopy.Rows.Add();
            //                        string nronc = vfilanronc[inc]["nronc"].ToString();
            //                        string fechanc = vfilanronc[inc]["fechanc"].ToString();
            //                        decimal montonc = Convert.ToDecimal(vfilanronc[inc]["montonc"].ToString());


            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nronc"] = nronc;
            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechanc"] = fechanc;
            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montonc"] = montonc;
            //                    }
            //                }

            //            }


            //        }
            //        else
            //        {
            //            string nronc = vfilanronc[0]["nronc"].ToString();
            //            string fechanc = vfilanronc[0]["fechanc"].ToString();
            //            decimal montonc = Convert.ToDecimal(vfilanronc[0]["montonc"].ToString());
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["nronc"] = nronc;
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechanc"] = fechanc;
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montonc"] = montonc;
            //        }

            //    }


            //    //forma de pago con saldoant
            //    DataRow[] vfilasa = dtsemana.Select("ped='" + dtgrupo.Rows[i]["ped"].ToString() + "' AND MONTOSALDOANT>0");

            //    if (vfilasa.Length > 0)
            //    {
            //        if (vfilasa.Length > 1)
            //        {
            //            if (vfilasa.Length >= vfilanrovoubcp.Length || vfilasa.Length >= vfilanronc.Length)
            //            {
            //                Int32 validasa = 0;
            //                for (Int32 isa = 0; isa < vfilasa.Length; ++isa)
            //                {
            //                    if (validasa == 0)
            //                    {
            //                        string fechasaldoant = vfilasa[isa]["fechasaldoant"].ToString();
            //                        decimal montosaldoant = Convert.ToDecimal(vfilasa[isa]["montosaldoant"].ToString());


            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechasaldoant"] = fechasaldoant;
            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montosaldoant"] = montosaldoant;
            //                        validasa += 1;
            //                    }
            //                    else
            //                    {
            //                        dtsemanacopy.Rows.Add();
            //                        string fechasaldoant = vfilasa[isa]["fechasaldoant"].ToString();
            //                        decimal montosaldoant = Convert.ToDecimal(vfilasa[isa]["montosaldoant"].ToString());


            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechasaldoant"] = fechasaldoant;
            //                        dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montosaldoant"] = montosaldoant;
            //                    }

            //                }
            //            }
            //            else
            //            {
            //                for (Int32 isa = 0; isa < vfilasa.Length; ++isa)
            //                {
            //                    string fechasaldoant = vfilasa[isa]["fechasaldoant"].ToString();
            //                    decimal montosaldoant = Convert.ToDecimal(vfilasa[isa]["montosaldoant"].ToString());
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - isa]["fechasaldoant"] = fechasaldoant;
            //                    dtsemanacopy.Rows[dtsemanacopy.Rows.Count - isa]["montosaldoant"] = montosaldoant;
            //                }

            //            }
            //        }
            //        else
            //        {
            //            string fechasaldoant = vfilasa[0]["fechasaldoant"].ToString();
            //            decimal montosaldoant = Convert.ToDecimal(vfilasa[0]["montosaldoant"].ToString());
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["fechasaldoant"] = fechasaldoant;
            //            dtsemanacopy.Rows[dtsemanacopy.Rows.Count - 1]["montosaldoant"] = montosaldoant;
            //        }

            //    }
            //}





            ////DataTable dtsemanacopy=new DataTable();
            ////dtsemanacopy = dtsemana.Clone();

            ////if (dtsemana.Rows.Count > 0)
            ////{
            ////    for (Int32 i = 0; i< dtsemana.Rows.Count ;++i )
            ////    {
            ////        if (vped.Length > 0)
            ////        {



            ////        }
            ////    }
            ////}
            //DataRow[] vfilatotal = dtsemana.Select("bolfact='TOTAL'");
            //dtsemanacopy.Rows.Add(DBNull.Value, vfilatotal[0]["promotor"].ToString(), DBNull.Value, DBNull.Value, vfilatotal[0]["bolfact"].ToString(), DBNull.Value, Convert.ToDecimal(vfilatotal[0]["montofac"].ToString()));

            return dtsemana;
            //return dtsemanacopy;

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
            catch (Exception ex)
            {
                msnMessage.Visible = true;
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);

            }
        }
        private void MergeRows(GridView gv, int rowPivotLevel)
        {
            //for (int rowIndex = gv.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            //{
            //    GridViewRow row = gv.Rows[rowIndex];
            //    GridViewRow prevRow = gv.Rows[rowIndex + 1];
            //    for (int colIndex = 0; colIndex < rowPivotLevel; colIndex++)
            //    {
            //        if (row.Cells[colIndex].Text == prevRow.Cells[colIndex].Text)
            //        {
            //            row.Cells[colIndex].RowSpan = (prevRow.Cells[colIndex].RowSpan < 2) ? 2 : prevRow.Cells[colIndex].RowSpan + 1;
            //            prevRow.Cells[colIndex].Visible = false;
            //        }
            //    }
            //}
        }
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.DataSource = (DataTable)Session[_nameSessionData];
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "pago_mov";

            Decimal[] _columna_caracter = { 1, 3,5 };
            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns,false, _columna_caracter);
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable)Session[_nameSessionData];

            gvReturns.DataBind();

            MergeRows(gvReturns, 4);

           
        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {


                e.Row.Cells[7].ColumnSpan = 3;
                e.Row.Cells[7].Text = "<table border=1 cellpadding=1 cellspacing=1><tr style='align-content:center;'><td style='align-content:center;' colspan=3><b>TICKET</b></td></tr><tr><td>FECHA&nbsp&nbsp&nbsp&nbsp&nbsp</td><td>NUMERO&nbsp&nbsp&nbsp&nbsp</td><td>IMPORTE</td></tr></table>";

                //'Agrupando las dos ultimas columnas (col=3 y col=4)


                //e.Row.Cells[3].Visible = false;
                //e.Row.Cells[4].Visible = false;
                //e.Row.Cells[5].Visible = false;
                //e.Row.Cells[7].Visible = false;
                //e.Row.Cells[8].Visible = false;
                //e.Row.Cells[9].Visible = false;
                //e.Row.Cells[10].Visible = false;
                //e.Row.Cells[11].Visible = false;
                //e.Row.Cells[12].Visible = false;

                //e.Row.Cells[13].Visible = false;
                //e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;

                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;

                e.Row.Cells[19].Visible = false;
                e.Row.Cells[20].Visible = false;
                e.Row.Cells[21].Visible = false;

                e.Row.Cells[22].Visible = false;
                e.Row.Cells[23].Visible = false;
                e.Row.Cells[24].Visible = false;

                e.Row.Cells[25].Visible = false;
                e.Row.Cells[26].Visible = false;
                e.Row.Cells[27].Visible = false;

                e.Row.Cells[28].Visible = false;
                e.Row.Cells[29].Visible = false;
                e.Row.Cells[30].Visible = false;

              


                //e.Row.Cells[16].Visible = false;
                //e.Row.Cells[17].Visible = false;
                //e.Row.Cells[18].Visible = false;
                //e.Row.Cells[19].Visible = false;
                e.Row.Cells[35].Visible = false;
                //e.Row.Cells[21].Visible = false;
                //e.Row.Cells[22].Visible = false;
                //e.Row.Cells[23].Visible = false;
                //e.Row.Cells[24].Visible = false;
                //e.Row.Cells[25].Visible = false;
                //e.Row.Cells[26].Visible = false;


                //'Opcion 2: Agregando una tabla generAQUARELLA dinamicamente
                e.Row.Cells[8].ColumnSpan = 3;
                e.Row.Cells[8].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>NOTA DE CREDITO 1</b></td></tr><tr><td>FECHA&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td><td>NUMERO&nbsp&nbsp&nbsp&nbsp&nbsp</td><td>IMPORTE</td></tr></table>";

                e.Row.Cells[9].ColumnSpan = 3;
                e.Row.Cells[9].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>NOTA DE CREDITO 2</b></td></tr><tr><td>FECHA&nbsp&nbsp&nbsp&nbsp</td><td>NUMERO&nbsp&nbsp</td><td>IMPORTE</td></tr></table>";



                e.Row.Cells[10].ColumnSpan = 3;
                e.Row.Cells[10].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>NOTA DE CREDITO 3</b></td></tr><tr><td>FECHA&nbsp&nbsp&nbsp&nbsp</td><td>NUMERO&nbsp&nbsp</td><td>IMPORTE</td></tr></table>";

                e.Row.Cells[11].ColumnSpan = 3;
                e.Row.Cells[11].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>NOTA DE CREDITO 4</b></td></tr><tr><td>FECHA&nbsp&nbsp&nbsp&nbsp</td><td>NUMERO&nbsp&nbsp</td><td>IMPORTE</td></tr></table>";

                e.Row.Cells[12].ColumnSpan = 3;
                e.Row.Cells[12].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>NOTA DE CREDITO 5</b></td></tr><tr><td>FECHA&nbsp&nbsp&nbsp&nbsp</td><td>NUMERO&nbsp&nbsp</td><td>IMPORTE</td></tr></table>";

                e.Row.Cells[13].ColumnSpan = 3;
                e.Row.Cells[13].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>NOTA DE CREDITO 6</b></td></tr><tr><td>FECHA&nbsp&nbsp&nbsp&nbsp</td><td>NUMERO&nbsp&nbsp</td><td>IMPORTE</td></tr></table>";

                e.Row.Cells[14].ColumnSpan = 3;
                e.Row.Cells[14].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>NOTA DE CREDITO 7</b></td></tr><tr><td>FECHA&nbsp&nbsp&nbsp&nbsp</td><td>NUMERO&nbsp&nbsp</td><td>IMPORTE</td></tr></table>";
                



                //e.Row.Cells[6].ColumnSpan = 3;
                //e.Row.Cells[6].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=3><b>Datos de la Nota de Credito</b></td></tr><tr><td>Numero</td><td>Fecha</td><td>Monto</td></tr></table>";

                e.Row.Cells[34].ColumnSpan = 2;
                e.Row.Cells[34].Text = "<table border=1 cellpadding=1 cellspacing=1><tr><td colspan=2><b>SALDO ANTERIOR</b></td></tr><tr><td>FECHA</td><td>IMPORTE</td></tr></table>";



            }

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (e.Row.Cells[3].Text == "TOTAL")
            //    {
            //        e.Row.BackColor = System.Drawing.Color.Khaki;
            //        e.Row.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
            //    }
            //}
        }
    }
}