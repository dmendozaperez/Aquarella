using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace www.aquarella.pe.Data.Util
{
    public class Conexion
    {
        static string strconexion = ConfigurationManager.ConnectionStrings["ConexionSql"].ConnectionString;
        public static string conexion_sql
        {
            get { return strconexion; }
        }
    }
}