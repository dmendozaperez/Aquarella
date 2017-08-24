using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
//using System.Data;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;
namespace www.aquarella.com.pe.bll.Control
{
    public class ApplicationFunctions
    {
        public int _id { set; get; }
        public string _name { set; get; }
        public string _description { set; get; }
        public int _idpadre { set; get; }
        public string _image { set; get; }
        public string _url { set; get; }
        public string _comments { set; get; }

        //private static DataSet _data;
        /// <summary>
        /// Retorna todos los roles del Usurio especificado.
        /// </summary>
        /// <param name="USN_USERID"></param>
        /// <param name="USV_CO"></param>
        /// <returns></returns>
        public static List<ApplicationFunctions> getFunctions_tree(decimal _bas_id)
        {
            DataTable dt = null;
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            string sqlcommand = "USP_Leer_Funcion_Arbol";
            try
            {

                List<ApplicationFunctions> colappfunctions = new List<ApplicationFunctions>();
                //_data = new DataSet();
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlcommand, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@bas_id", _bas_id);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

                //// CURSOR REF
                //object results = new object[1];
                /////
                //Database db = DatabaseFactory.CreateDatabase(_conn);
                /////
                //string sqlCommand = "CONTROL.sp_getfunctions_tree";
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, p_type, p_co, p_userid, results);
                ////return ALL APPLICATIONS COMPANY AND FUCTION REQUIRED 
                //_data = db.ExecuteDataSet(dbCommandWrapper);
                foreach (DataRow row in  dt.Rows)
                {

                    colappfunctions.Add(
                        new ApplicationFunctions
                        {
                            _id = Int32.Parse(row["fun_id"].ToString()),
                            _name = row["fun_nombre"].ToString(),
                            _description = row["Fun_Descripcion"].ToString(),
                            _idpadre = Int32.Parse(row["fun_padre"].ToString()),
                            //_image = row["fun_image"].ToString(),
                            _url = row["apl_url"].ToString(),
                            _comments = row["apl_comentario"].ToString()
                        }
                        );
                }

                return colappfunctions;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
    }



}