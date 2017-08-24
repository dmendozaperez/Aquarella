using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad.Bll.Util
{
    public class Ent_Tarjeta
    {
        public string idtarjeta { get; set; }
        public string tarnom { get; set; }
        public byte[] tarimagen { get; set; }
    }
    public class Ent_Pines_Tarjeta
    {
        public string bin_tar_ser { get; set; }
        public string bin_tar_cod { get; set; }
        public string bin_tar_des { get; set; }
        public Boolean existe_bines { get; set; }
    }
}
