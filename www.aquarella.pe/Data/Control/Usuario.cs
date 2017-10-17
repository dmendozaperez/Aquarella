using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aquarella.pe.Data.Util;

namespace www.aquarella.pe.Data.Control
{
    public class Usuario
    {
        public decimal _usu_id { get; set; }
        public string _usu_nombre { get; set; }
        public string _usu_password { get; set; }
        public string _usu_est_id { get; set; }
        public string _usu_nom_ape { get; set; }
        public string _usu_tip { get; set; }
        public string _usu_tip_nom { get; set; }
        public string _usu_lid { get; set; }
        public string _error_con { get; set; }
        public DateTime _usu_fec_cre { get; set; }



        public Usuario(string _usuario="")
        {
            DataTable dt = Login_User(_usuario);
            try
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _usu_id = Convert.ToDecimal(dt.Rows[0]["bas_id"]);
                        _usu_nombre = dt.Rows[0]["Usu_Nombre"].ToString();
                        _usu_password = Cryptographic.decrypt(dt.Rows[0]["Usu_Contraseña"].ToString());
                        _usu_est_id = dt.Rows[0]["Usu_Est_id"].ToString();
                        _usu_nom_ape = dt.Rows[0]["Nombre"].ToString();
                        _usu_tip = dt.Rows[0]["Usu_Tip_Id"].ToString();
                        _usu_tip_nom = dt.Rows[0]["Usu_Tip_Nombre"].ToString();
                        _usu_lid = dt.Rows[0]["bas_Are_id"].ToString();
                        _usu_fec_cre = DateTime.Parse(dt.Rows[0]["usu_fecha_cre"].ToString());
                        _error_con = "1";
                    }
                    else
                    {
                        _error_con = "0";
                    }
                }
            }
            catch
            {

            }
        }
        #region<PRODIEDADES DATA LOCAL>
        private DataTable Login_User(string _usuario)
        {
            string sqlquery = "USP_Leer_Usuario";
            DataTable dt = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usu_Nombre", _usuario);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch
            {

                dt = null;
            }
            return dt;
        }
        #endregion
        #region<REGION DE PROPIEDADES PUBLICA>
        public Boolean UpdateUsuario()
        {
            Boolean _upd = false;
            string sqlquery = "USP_Modificar_Usuario";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usu_id", _usu_id);
                        cmd.Parameters.AddWithValue("@usu_nombre", _usu_nombre);
                        cmd.Parameters.AddWithValue("@contraseña", _usu_password);
                        cmd.Parameters.AddWithValue("@usu_fecha_cre", _usu_fec_cre);
                        cmd.Parameters.AddWithValue("@usu_est_id", _usu_est_id);
                        cmd.ExecuteNonQuery();
                        _upd = true;
                    }

                }
            }
            catch
            {
                _upd = false;
            }
            return _upd;
        }
        
        public List<UsuarioModel> GetUserByName(string _username)
        {
            string sqlquery = "USP_Leer_Usuario_Nombre";
            List<UsuarioModel> listar = null; 
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    //if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_primer_nombre", _username);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<UsuarioModel>();
                            listar = (from DataRow dr in dt.Rows
                                           select new UsuarioModel()
                                           {
                                               usu_id=Convert.ToInt32(dr["usu_id"]),
                                               usu_nombre = dr["usu_nombre"].ToString(),
                                               usu_password = dr["usu_contraseña"].ToString(),
                                               usu_fecha_cre =Convert.ToDateTime(dr["usu_fecha_cre"]),
                                               usu_est_id = dr["usu_est_id"].ToString(),
                                               nombre = dr["nombre"].ToString()
                                           }).ToList();
                        }

                        //SqlDataReader dr = cmd.ExecuteReader();

                        //if (dr.HasRows)
                        //{
                        //    listar = new List<Usuario>();
                        //    while (dr.Read())
                        //    {

                        //    }
                        //}
                    }
                }
            }
            catch
            {
                listar = null;
            }
            return listar;
        }

        #endregion
    }
    public class UsuarioModel
    {
        public Int32 usu_id;
        public string usu_nombre;
        public string usu_password;
        public DateTime usu_fecha_cre;
        public string usu_est_id;
        public string nombre;
        public string buscar_nom;
    }
    public class UsuarioRoles
    {
        public string rol_id { get; set; }
        public string rol_nombre { get; set; }
        public string rol_descripcion { get; set; }

        public List<UsuarioRoles> get_lista(decimal usu_id)
        {
            string sqlquery = "USP_Leer_Roles_Usuario";
            List<UsuarioRoles> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usu_id", usu_id);
                        SqlDataReader dr = cmd.ExecuteReader();
                        list = new List<UsuarioRoles>();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                UsuarioRoles fila = new UsuarioRoles();
                                fila.rol_id = dr["rol_id"].ToString();
                                fila.rol_nombre = dr["rol_nombre"].ToString();
                                list.Add(fila);
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {

                list = null;
            }
            return list;
        }
    }
}