﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Models
{
    public class Ent_Usuario
    {
        public string usu_nombre { get; set; }
        public string usu_apPaterno { get; set; }
        public string usu_apMaterno { get; set; }
        public string usu_login { get; set; }
        public string usu_contraseña { get; set; }
        public string usu_est_id { get; set; }
        public decimal usu_id { get; set; }
        public string usu_tip_id { get; set; }

        public string usu_tip_nom { get; set; }
        public string usu_ip { get; set; }
    }
}