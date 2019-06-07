using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll
{
    public class formapago
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;
        public string _cov_des { get; set; }
        public string _cov_conid { get; set; }

        #endregion

        #region <REGION DE VARIABLES>
        private string _cov_description;
        private string _cov_conceptid;
        #endregion

        #region <REGION PROPIEDADES>

        public string cov_description
        {
            get { return _cov_description; }
            set { _cov_description = value; }
        }
        public string cov_conceptid
        {
            get { return _cov_conceptid; }
            set { _cov_conceptid = value; }
        }

        #endregion
        #region <CONSTRUCTORES>
        public formapago(string cov_description, string cov_conceptid)
        {
            _cov_description = cov_description;
            _cov_conceptid = cov_conceptid;

        }
        #endregion

        #region <METODOS PUBLICOS>
        public static DataSet Get_CARGAR_POS( string _postpago, int _basid, decimal _idCust)
        {
            //Se agrega 2 variables: _basid, _idCust - 06-06-2019
            //Funcionalidad: Para que se observe la opción Op. Gratuitas solo si el usuario realiza una compra a su nombre, mas no a nombre de otras promotoras.

            string sqlquery = "USP_Leer_MedioPagoCondicion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@post", _postpago);
                cmd.Parameters.AddWithValue("@bas_id", _basid);
                cmd.Parameters.AddWithValue("@cust_id", _idCust);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
                
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}