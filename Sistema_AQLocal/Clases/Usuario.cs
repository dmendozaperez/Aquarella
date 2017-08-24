using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sistema_AQLocal
{
    public class Usuario
    {
        #region <REGION DE ATRIBUTOS>
        public Int32 _bas_id { set; get; }
        public string _usu_nombre { set; get; }
        public string _usu_contraseña { set; get; }
        public string _usu_est_id { set; get; }
        public string _nombre { set; get; }
        public string _usu_tip_id { set; get; }
        public string _usu_tip_nombre { set; get; }
        public string _usv_area { set; get; }
        public decimal _usn_userid { set; get; }
        public string _usv_username { set; get; }
        public string _usv_postpago { get; set; }
        public DateTime _usd_creation { set; get; }
        /// <summary>
        #endregion
    }
}
