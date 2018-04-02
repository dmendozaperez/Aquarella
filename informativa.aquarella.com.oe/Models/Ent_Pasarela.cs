using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Models
{
    public class Ent_Pasarela
    {
        public Int32 Pasarela_id { get; set; }
        public string Pasarela_Descripcion { get; set; }
        public string Pasarela_Titulo { get; set; }
        public string Pasarela_Tipo { get; set; }
        public string Pasarela_Estado { get; set; }
        public string Pasarela_strDetalle { get; set; }
        public string Pasarela_strRuta { get; set; }
        public string Pasarela_UsuCrea { get; set; }
        public string Pasarela_Ip { get; set; }
        public List<Ent_PasarelaDetalle> Pasarela_ListDetalle { get; set; }
    }
}