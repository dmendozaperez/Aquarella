using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Models
{
    public class LoginModel
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool Recordar { get; set; }
        public string returnUrl { get; set; }
    }
}