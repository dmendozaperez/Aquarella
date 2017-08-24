using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class VentaAnualEstadistica : System.Web.UI.Page
    {

        Users _user;
        string _nameSessionData = "InfoSales", _nameSessionDataFiltered = "InfoSalesFilter";
        DataSet _dsResult;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = null;
                Session[_nameSessionDataFiltered] = null;
                optmonto.Checked = true;
                if ((_user._usu_tip_id == "02"))
                {
                    Utilities.logout(Page.Session, Page.Response);
                }
                else
                {
                       sbinicio();
                }
                
            }
        }
        protected void cargaranio()
        {
            // Mostrar Panel de Seleccion de Coordinador
            
           
            dwanio.Focus();
            dwanio.DataSource = Area.getAllaño();
            dwanio.DataBind();
        }
        protected void sbinicio()
        {
            cargaranio();
            dwanio.SelectedValue = DateTime.Today.Year.ToString();
            sbconsultar();
        }
        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }
        protected void calculateTotals(DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    var t = (from x in dt.AsEnumerable()
                             group x by x.Table into y
                             select new
                             {
                                 x1 = y.Count(),
                              
                                 x5 = y.Sum(x => x.Field<decimal>("total")),
                              
                             }).FirstOrDefault();
                    if (optmonto.Checked)
                    {
                        lblTotSales1.Text = t.x5.ToString("C2");
                    }
                    else
                    {
                        lblTotSales1.Text = t.x5.ToString("N0");
                    }
                }
                else
                {
                    decimal Val = 0;
                    if (optmonto.Checked)
                    {
                        lblTotSales1.Text = Val.ToString("C2");
                    }
                    else
                    {
                        lblTotSales1.Text = Val.ToString("N0");
                    }
                }
            }
            catch { }
        }
        protected void sbconsultar()
        {
            
            msnMessage.Visible = false;
            Int32 vanio = Convert.ToInt32(dwanio.SelectedValue);

            if (vanio == -1)
              { 
                 msnMessage.LoadMessage("Seleccione el año por favor...", UserControl.ucMessage.MessageType.Error);
               }


            setParametersDataSources(vanio);
            buildGraph(1, 1);
            calculateTotals(getSource());

            if (optmonto.Checked)
            {
                chartSales.Titles["Title1"].Text = "Ventas Neto Anual " + vanio.ToString();
                lblletra.Text = "Ventas Neto";
            }
            else
            {
                chartSales.Titles["Title1"].Text = "Ventas Unidad Anual " + vanio.ToString();
                lblletra.Text = "Ventas Unidad";
            }


            DataTable dt = getSource();
            if (dt.Rows.Count == 0)
            {
                chartSales.Visible = false;
            }
            else
            {
                chartSales.Visible = true;
            }
            
        }
        protected DataTable getSource()
        {
                return (DataTable)Session[_nameSessionData];

        }
        public void buildGraph(int bars, int pie)
        {
            DataTable dt = (DataTable)Session[_nameSessionData];

            var sales =
            from p in dt.AsEnumerable()
            group p by p.Field<string>("mescaracter") into g
            select new { category = g.Key, sales = g.Sum(p => p.Field<decimal>("total")) };

            sales.OrderByDescending(x => x.sales);

            if (bars > 0)
            {
                chartSales.DataSource = sales;
                // Show as 2D or 3D
                if (CheckboxShow3D.Checked)
                    chartSales.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                else
                    chartSales.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

                chartSales.Series[0].ToolTip = "#LEGENDTEXT: #VAL{C0} - #PERCENT";
                chartSales.Series[0].LegendToolTip = "#PERCENT";

                if (optmonto.Checked)
                {
                    chartSales.Series[0].LabelFormat = "{N2}";
                }
                else
                {
                    chartSales.Series[0].LabelFormat = "{N0}";
                }

                chartSales.Series[0].IsValueShownAsLabel = true;
                // Set series members names for the X and Y values 
                chartSales.Series[0].XValueType = System.Web.UI.DataVisualization.Charting.ChartValueType.String;

                // Set axis labels angle                
                chartSales.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = int.Parse(FontAngleList.SelectedItem.Text);
                // Set offset labels style
                chartSales.AntiAliasing = System.Web.UI.DataVisualization.Charting.AntiAliasingStyles.All;

                // Paint labels of all bars
                chartSales.ChartAreas[0].AxisX.Interval = 1;

                chartSales.DataBind();
            }

        }

        protected void CheckboxShow3D_CheckedChanged(object sender, EventArgs e)
        {
            buildGraph(1, 0);
        }

        protected void FontAngleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(1, 0);
        }

        #region < Eventos sobre data source >

        /// <summary>
        /// Settear el objeto fuente de datos
        /// </summary>
        /// <param name="co"></param>
        /// <param name="ware"></param>
        /// <param name="area"></param>
        protected void setParametersDataSources(Int32 _anio)
        {
            string valor = "";
            if (optmonto.Checked)
            {
                valor = "0";
            }
            else
            {
                valor = "1";
            }

            _dsResult = www.aquarella.com.pe.bll.Ventas.Facturacion.getestadisticaventasneta(_anio,valor);
            Session[_nameSessionData] = _dsResult.Tables[0];
        }

        #endregion
    }
}