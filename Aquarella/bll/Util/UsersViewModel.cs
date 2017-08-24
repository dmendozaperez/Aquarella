using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aquarella.bll;
using System.Data;
using Variables;
namespace Aquarella.bll.Util
{
    class UsersViewModel
    {
        public Usuario getAndLoadUserByUserName(String usv_username)
        {
            try
            {
                ///
                DataTable dtusuario = Acceso.F_LeerUsuario(usv_username);
                if (dtusuario == null || dtusuario.Rows.Count <= 0)
                {
                    return null;
                }
                DataRow dr = dtusuario.Rows[0];
                Global._bas_id_codigo = Convert.ToInt32(dr["bas_id"].ToString());
                Usuario u = new Usuario
                {
                    _bas_id = Convert.ToInt32(dr["bas_id"].ToString()),
                    _usu_nombre = dr["usu_nombre"].ToString(),
                    _usu_contraseña = dr["usu_contraseña"].ToString(),
                    _usu_est_id = dr["usu_est_id"].ToString(),
                    _nombre = dr["nombre"].ToString(),
                    _usu_tip_id = dr["usu_tip_id"].ToString(),
                    _usu_tip_nombre = dr["usu_tip_nombre"].ToString(),
                    _usv_area = dr["bas_Are_id"].ToString(),
                    _usn_userid = Convert.ToInt32(dr["bas_id"].ToString()),
                    _usv_username = dr["usu_nombre"].ToString(),                 
                    _usd_creation = System.DateTime.Parse(dr["usu_fecha_cre"].ToString()),                    
                    _usv_postpago = dr["postpago"].ToString()
                };

                return u;
            }
            catch { return null; }
        }
    }
}
