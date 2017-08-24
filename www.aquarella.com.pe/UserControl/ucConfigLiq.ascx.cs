using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace www.aquarella.com.pe.UserControl
{
    public partial class ucConfigLiq : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Retorna un bool que indica si el radio buton de tipo de liquidacion recoleccion cedi esta chequeado
        /// </summary>
        /// <returns></returns>
        public bool getTypeLiqRc()
        {
            return rbTypePRCS.Checked;
        }

        /// <summary>
        /// Retorna un bool que indica si el cliente configuro una direccion de envio de un tercero
        /// </summary>
        /// <returns></returns>
        public bool getConfigShipping()
        {
            return chkbInfoShipp.Checked;
        }
    }
}