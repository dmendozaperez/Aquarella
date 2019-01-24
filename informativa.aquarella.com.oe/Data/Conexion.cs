using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Data
{
    public class Conexion
    {
        public static string conexion_data = Encripta.encryption.RijndaelDecryptString(ConfigurationManager.ConnectionStrings["MyConexionSql"].ConnectionString);//; "Server=10.10.10.206;Database=BdAquarella;UID=dmendoza;Password=Bata2013";

        public static string Str_RutaImg = "assets/img/slider/";

        public static string Str_RutaCatalogoFotos = "CatalogoVirtual/";

        public static string Str_RutaCatalogo = "/CatalogoVirtual/";

        public static string Str_RutaPlantilla = "/CatalogoVirtual/";

        public static string Str_FolderPlantilla = "catalogo_prueba_html";
    }
}