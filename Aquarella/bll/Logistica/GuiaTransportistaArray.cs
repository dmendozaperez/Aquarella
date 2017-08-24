using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aquarella.bll
{
    class GuiaTransportistaArray
    {
        public object getSetArray_Co { set; get; }
        public object getSetArray_CodCliente { set; get; }
        public object getSetArray_Nombre { set; get; }
        public object getSetArray_LIQUIDATION_NO { set; get; }
        public object getSetArray_INVOICE_NO { set; get; }
        public object getSetArray_PAN_NO { set; get; }
        public object getSetArray_PREFIJO { set; get; }
        public object getSetArray_NUMGUIA { set; get; }
        public object getSetArray_Cantidad { set; get; }
        public object getSetArray_Costo { set; get; }
        public object getSetArray_NUMCOMPLETO { set; get; }
        public object getSetArray_RangoGuia { set; get; }

        public int  getSetSecuencialTransportista { set; get; }
        public bool Ok { get; set; }
        public string Mensaje { get; set; }

    }

}
