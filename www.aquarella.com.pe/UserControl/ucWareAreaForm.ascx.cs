using System;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace www.aquarella.com.pe.UserControl
{
    public partial class ucWareAreaForm : System.Web.UI.UserControl
    {
        /// <summary>
        /// Load de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Deshabilitar el combo de areas
        /// </summary>
        public void unableArea()
        {
            this.dwArea.Enabled = false;
            this.dwArea.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "click", "hideControl('tblArea')", true);
        }

        /// <summary>
        /// Carga de dropdown list de bodegas
        /// </summary>
        /// <param name="co"></param>
        public void setDwWare(string co)
        {
            //this.dwWare.DataSource = Bata.Aquarella.BLL.Logistica.warehouses.getWarehousesByCo(co);
            //this.dwWare.DataBind();
        }

        /// <summary>
        /// Carga de dropdownlist de area
        /// </summary>
        /// <param name="co"></param>
        public void setDwArea(string co)
        {
            //this.dwArea.DataSource = Bata.Aquarella.BLL.Area.getAllAreas(co);
            //this.dwArea.DataBind();
        }

        /// <summary>
        /// Carga de ambos dropdownlist
        /// </summary>
        /// <param name="co"></param>
        public void wareAreaForm(string co)
        {
            setDwArea(co);
            setDwWare(co);
        }

        public void wareAreaForm(string co, string region)
        {
            setDwWare(co);
            //setDwArea(co);

            //this.dwArea.DataSource = Bata.Aquarella.BLL.Area.getAreasByRegion(co, region);
            //this.dwArea.DataBind();
        }

        /// <summary>
        /// Retorna el valor seleccionado en el dropdownlist de bodegas
        /// </summary>
        /// <returns></returns>
        public string getDwWareValue()
        {
            return this.dwWare.SelectedValue;
        }

        /// <summary>
        /// Retorna el valor seleccionado en el dropdownlist de areas
        /// </summary>
        /// <returns></returns>
        public string getDwAreaValue()
        {
            return this.dwArea.SelectedValue;
        }

        /*public string getLbAreaValue()
        {
            string areas = string.Empty;
            int i = 0;

            foreach (ListItem item in this.lbArea.Items)
            {
                if (item.Selected)
                {
                    if (i > 0)
                        areas += ",";

                    if (!areas.Contains(item.Value + ","))
                        areas += item.Value;
                    i++;
                }
            }

            return areas;
        }*/

        /// <summary>
        /// Valores dropdownlist
        /// </summary>
        /// <param name="wareValue"></param>
        /// <param name="areaValue"></param>
        public void getDws(out string wareValue, out string areaValue)
        {
            wareValue = getDwWareValue();
            //areaValue = getLbAreaValue();
            areaValue = getDwAreaValue();
        }

        /// <summary>
        /// Seleccionar el valor del dropdownlist de bodega
        /// </summary>
        /// <param name="value"></param>
        public void setDwWareValue(string value)
        {
            this.dwWare.SelectedValue = value;
        }

        /// <summary>
        /// Seleccionar el valor del dropdownlist de area
        /// </summary>
        /// <param name="value"></param>
        public void setDwAreaValue(string value)
        {
            this.dwArea.SelectedValue = value;
        }

        /// <summary>
        /// Habilitar o deshabilitar el dropdownlist de area
        /// </summary>
        /// <param name="enabled"></param>
        public void enabledDwArea(bool enabled)
        {
            this.dwArea.Enabled = enabled;
        }

        /// <summary>
        /// Habilitar o deshabilitar el dropdownlist de bodega
        /// </summary>
        /// <param name="enabled"></param>
        public void enabledDwWare(bool enabled)
        {
            this.dwWare.Enabled = enabled;
        }

        /// <summary>
        /// Segun el usuario determinar si se le permite o no seleccionar area y/o bodega
        /// </summary>
        /// <param name="user"></param>
        //public void setFormByUser(Bata.Aquarella.BLL.Users user)
        //{
        //    // Usuario nacional o regional
        //    if (!user._usv_warehouse.Equals("%%"))
        //    {
        //        setDwWareValue(user._usv_warehouse);
        //        enabledDwWare(false);
        //    }
        //    if (!user._usv_area.Equals("%%"))
        //    {
        //        setDwAreaValue(user._usv_area);
        //        enabledDwArea(false);
        //    }
        //}


    }
}