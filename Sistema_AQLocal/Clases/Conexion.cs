using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sistema_AQLocal
{
    class Conexion
    {
        public static string conexion_local
        {
            //get { return "Server=10.10.10.206;Database=BdAquarella;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            //get { return "Server=10.10.10.232;Database=BdAquarella;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            //get { return "Server=10.10.10.206;Database=BdAquarella;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            get { return "Server=172.28.7.14;Database=BdAquarella;User ID=sis_aquarella;Password=Bata2018**;Trusted_Connection=False;"; }
            // get { return "Server=172.19.7.169;Database=BdAquarellaFE;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
        }
        public static string mensaje
        {
            get { return "Aviso del Sistema"; }
        }
        public static string kCulture
        {
            get { return "es-PE"; }
        }
        public static string kdirlogoInvoce
        {
            get { return "\\Assets\\images\\logoInvoce.bmp"; }
        }
        public static string kSeriePosBoleta
        {
            get { return "FFGF100708"; }
        }
        public static string kPosConcepts_VISA_UNICA
        {
            get { return "9NE"; }
        }
        public static string kCompany
        {
            get { return "1"; }
        }
        public static string KCountDecimal
        {
            get { return "C2"; }
        }
        public static string kPosImpresoraFactura
        {
            get { return "PosTicket"; }
        }
        public static string kPosImpresora
        {
            get { return "PosTicket"; }
        }
        private static Int32 form_activo;
        public static Int32 _form_activo
        {
            set { form_activo = value; }
            get { return form_activo; }
        }
        public static Int32 _bas_id_codigo { set; get; }
    }
}
