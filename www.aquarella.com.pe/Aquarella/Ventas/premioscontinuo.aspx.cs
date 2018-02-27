﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class premioscontinuo : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnDataPremios";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                //DateTime _fechaactual = DateTime.Now;
                ////DateTime vpri = DateTime.MinValue;

                //DateTime fecha_ini;

                //fecha_ini = new DateTime(_fechaactual.Year, _fechaactual.Month + 1, 1).AddMonths(-1);
                DateTime fechatemp;
                DateTime fecha1;
               
                fechatemp = DateTime.Today;
                fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

                //fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

                ////vpri = vprimerdia.AddDays(1 - Convert.ToDouble(vprimerdia.DayOfWeek));
                ////DateTime Vult = vpri.AddDays(6);

                //txtDateStart.Text = fecha_ini.ToString("dd/MM/yyyy");
                txtDateStart.Text = fecha1.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

                consultar();

            }
        }
        private void consultar()
        {
            try
            {
                msnMessage.Visible = false;

                Session[_nameSessionData] = invoice.get_PremiosContinuo(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text),chkvalida.Checked);

                DataSet ds = (DataSet)Session[_nameSessionData];

                if (ds.Tables.Count != 0)
                {
                    gvReturns.DataSource = (DataSet)Session[_nameSessionData];

                    gvReturns.DataBind();
                }
                else
                {
                    msnMessage.Visible = true;
                    msnMessage.LoadMessage("No hay datos para mostrar", UserControl.ucMessage.MessageType.Error);
                }


            }
            catch (Exception ex)
            {
                msnMessage.Visible = true;
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);

            }
        }
        protected void btConsult_Click(object sender, EventArgs e)
        {
            consultar();
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataSource = (DataSet)Session[_nameSessionData];
            gvReturns.DataBind();

            string nameFile = "PremiosContinuo";
            decimal[] columna = { 3 };
            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns,false,columna);
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataSet)Session[_nameSessionData];

            gvReturns.DataBind();
        }

        protected void gvReturns_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvReturns_DataBinding(object sender, EventArgs e)
        {

        }

        protected void chkresumen_CheckedChanged(object sender, EventArgs e)
        {
            consultar();
        }
    }
}