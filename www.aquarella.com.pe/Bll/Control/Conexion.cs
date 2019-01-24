using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace www.aquarella.com.pe.bll.Control
{
    public class Conexion
    {
        static string strconexion =Encripta.encryption.RijndaelDecryptString(ConfigurationManager.ConnectionStrings["MyConexionSql"].ConnectionString);

        public static string myconexion()
        {
            return strconexion;
        }
    }
}