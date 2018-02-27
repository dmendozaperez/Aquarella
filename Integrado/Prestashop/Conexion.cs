using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrado.Prestashop
{
    public class Conexion
    {
        public MySqlConnection getConexionMySQL()
        {
            MySqlConnection mysql = new MySqlConnection(ConfigurationManager.ConnectionStrings["mysql"].ConnectionString);
            return mysql;
        }
    }
}
