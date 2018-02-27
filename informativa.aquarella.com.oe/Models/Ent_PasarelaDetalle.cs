using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Models
{
    public class Ent_PasarelaDetalle
    {

        public Int32 PasarelaDet_id { get; set; }
        public Int32 Pasarela_id { get; set; }
        public string PasarelaDet_Ruta { get; set; }
        public string PasarelaDet_Nombre { get; set; }
        public Int32 PasarelaDet_Orden { get; set; }
        public string PasarelaDet_Estado { get; set; }

        public string PasarelaDeta_NombreImagen { get; set; }
        public byte[] PasarelaDet_Imagen { get; set; }
        public HttpPostedFileBase PasarelaDet_archivo { get; set; }
        
    }
}