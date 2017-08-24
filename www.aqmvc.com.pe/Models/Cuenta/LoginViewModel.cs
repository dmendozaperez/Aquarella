using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.aqmvc.com.pe.Models.Cuenta
{
    public class LoginViewModel
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool Recordar { get; set; }
    }
}