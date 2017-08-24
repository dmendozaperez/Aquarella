using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaDato.Bll.Util
{
    public class Dat_Conexion
    {
        public static string conexion
        {
            //get { return "Server=(local);Database=BbIntegrado;User ID=sa;Password=123;Trusted_Connection=False;"; }            
            //get { return "Server=148.102.50.44;Database=BbIntegrado;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }            
            get { return "Server=148.102.50.45;Database=BbIntegrado;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }            
        }
    }
}
