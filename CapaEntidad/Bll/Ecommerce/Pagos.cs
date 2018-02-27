using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad.Bll.Ecommerce
{
    public class Pagos
    {
        public string pag_metodo { get; set; }
        public string pag_nro_trans { get; set; }
        public DateTime pag_fecha { get; set; }
        public Decimal pag_monto { get; set; }
    }
}
