﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad.Bll.Util
{
    public class Ent_Conexion
    {
       
        public static string _server { get; set; }
        public static string _Base_Datos { set; get; }
        public static string _user { get; set; }
        public static string _password { get; set; }

        public static string conexion
        {
            //get { return "Server=www.aquarellaperu.com.pe;Database=" + _Base_Datos + ";User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
            get { return "Server=" + _server +";Database=" + _Base_Datos +";User ID=" + _user +";Password=" + _password +";Trusted_Connection=False;"; }
        }
    }
}
