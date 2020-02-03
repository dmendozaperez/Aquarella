using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace www.aquarella.com.pe.bll.Control
{
    public class Conexion
    {
        //static string strconexion =Encripta.encryption.RijndaelDecryptString(ConfigurationManager.ConnectionStrings["MyConexionSql"].ConnectionString);
        static string strconexion = ConfigurationManager.ConnectionStrings["MyConexionSql"].ConnectionString;

        public static string myconexion()
        {
            //return "Server=172.28.7.14;Database=BdAquarella;User ID=pos_oracle;Password=Bata2018**;Trusted_Connection=False;"; 
            return strconexion;
        }
    }
}