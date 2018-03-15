using informativa.aquarella.com.oe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Data
{
    public class CatalogoDA
    {

        public List<Ent_Catalogo> get_listaCatalogo()
        {
            string sqlquery = "USP_GetCatalogo";
            List<Ent_Catalogo> lista = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            lista = new List<Ent_Catalogo>();
                            lista = (from DataRow dr in dt.Rows
                                     select new Ent_Catalogo()
                                     {
                                         Catalogo_Descripcion = dr["Descripcion"].ToString(),
                                         Catalogo_Titulo = dr["Titulo"].ToString(),
                                         Catalogo_strNroPag = dr["NroPagina"].ToString(),
                                         Catologo_strRuta = dr["Ruta"].ToString(),
                                         Catologo_Carpeta = dr["Carpeta"].ToString(),


                                     }).ToList();

                        }
                    }
                }
            }
            catch (Exception exc)
            {

                lista = null;
            }
            return lista;
        }

        public List<Ent_Catalogo> listarCatalogoMantenedor()
        {
            string sqlquery = "USP_ListarCatalogo";
            List<Ent_Catalogo> lista = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            lista = new List<Ent_Catalogo>();
                            lista = (from DataRow dr in dt.Rows
                                     select new Ent_Catalogo()
                                     {
                                         Catalogo_Id = Int32.Parse(dr["IdCatalogo"].ToString()),
                                         Catalogo_Descripcion = dr["Descripcion"].ToString(),
                                         Catalogo_Titulo = dr["Titulo"].ToString(),
                                         Catalogo_strNroPag = dr["NroPagina"].ToString(),
                                         Catologo_Orden = dr["Orden"].ToString(),
                                         Catologo_strRuta = dr["Ruta"].ToString(),
                                         Catalogo_Estado = dr["Est_Id"].ToString(),
                                         Catalogo_chkEstado = dr["checked"].ToString(),



                                     }).ToList();

                        }
                    }
                }
            }
            catch (Exception exc)
            {

                lista = null;
            }
            return lista;
        }


        public int InsertarCatalogo(Ent_Catalogo catalogo)
        {

            int idCatalogo = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    if (cn.State == 0) cn.Open();

                    SqlCommand oComando = new SqlCommand("USP_Insertar_Catalogo", cn);
                    oComando.CommandType = CommandType.StoredProcedure;

                    SqlParameter oCatalogo_id = oComando.Parameters.Add("@Catalogo_id", SqlDbType.Int);
                    oCatalogo_id.Direction = ParameterDirection.Input;
                    oCatalogo_id.Value = catalogo.Catalogo_Id;

                    SqlParameter oCatalogo_titulo = oComando.Parameters.Add("@Catalogo_titulo", SqlDbType.VarChar);
                    oCatalogo_titulo.Direction = ParameterDirection.Input;
                    oCatalogo_titulo.Value = catalogo.Catalogo_Titulo;

                    SqlParameter oCatalogo_descripcion = oComando.Parameters.Add("@Catalogo_descripcion", SqlDbType.VarChar);
                    oCatalogo_descripcion.Direction = ParameterDirection.Input;
                    oCatalogo_descripcion.Value = catalogo.Catalogo_Descripcion;

                    SqlParameter oCatalogo_strRuta = oComando.Parameters.Add("@Catalogo_strRuta", SqlDbType.VarChar);
                    oCatalogo_strRuta.Direction = ParameterDirection.Input;
                    oCatalogo_strRuta.Value = catalogo.Catologo_strRuta;

                    SqlParameter Catalogo_Carpeta = oComando.Parameters.Add("@Catalogo_Carpeta", SqlDbType.VarChar);
                    Catalogo_Carpeta.Direction = ParameterDirection.Input;
                    Catalogo_Carpeta.Value = catalogo.Catologo_Carpeta;

                    SqlParameter oCatalogo_NroPagina = oComando.Parameters.Add("@Catalogo_NroPagina", SqlDbType.Int);
                    oCatalogo_NroPagina.Direction = ParameterDirection.Input;
                    oCatalogo_NroPagina.Value = Convert.ToInt32(catalogo.Catalogo_strNroPag);

                    SqlParameter oCatalogo_Orden = oComando.Parameters.Add("@Catalogo_Orden", SqlDbType.Int);
                    oCatalogo_Orden.Direction = ParameterDirection.Input;
                    oCatalogo_Orden.Value = Convert.ToInt32(catalogo.Catologo_Orden);

                    SqlParameter oCatalogo_estado = oComando.Parameters.Add("@Catalogo_estado", SqlDbType.VarChar);
                    oCatalogo_estado.Direction = ParameterDirection.Input;
                    oCatalogo_estado.Value = catalogo.Catalogo_Estado;

                    SqlParameter oCatalogo_usuCrea = oComando.Parameters.Add("@Catalogo_usuCrea", SqlDbType.VarChar);
                    oCatalogo_usuCrea.Direction = ParameterDirection.Input;
                    oCatalogo_usuCrea.Value = catalogo.UsuCrea;

                    SqlParameter oCatalogo_ipCrea = oComando.Parameters.Add("@Catalogo_ipCrea", SqlDbType.VarChar);
                    oCatalogo_ipCrea.Direction = ParameterDirection.Input;
                    oCatalogo_ipCrea.Value = catalogo.Ip;

                    SqlParameter oIdRespuesta = oComando.Parameters.Add("@Catalogo_id", SqlDbType.Int);

                    oIdRespuesta.Direction = ParameterDirection.ReturnValue;

                    oComando.ExecuteNonQuery();
                    idCatalogo = (int)oIdRespuesta.Value;

                }

            }
            catch (Exception exc)
            {
                idCatalogo = -1;
                //valida = false;
            }
            return idCatalogo;

        }
        
        public Ent_Catalogo GetCatalogo(string strId)
        {
            Ent_Catalogo Catalogo = null;
         
     
            string sqlquery = "USP_Catalogo_IND";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Catalogo_id", strId);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            Catalogo = new Ent_Catalogo();
                           

                            while (dr.Read())
                            {

                                Catalogo.Catalogo_Id = Int32.Parse(dr["IdCatalogo"].ToString());
                                Catalogo.Catologo_strRuta = dr["Ruta"].ToString();
                                Catalogo.Catologo_Carpeta = dr["Carpeta"].ToString();
                                Catalogo.Catalogo_Descripcion = dr["Descripcion"].ToString();
                                Catalogo.Catalogo_Titulo = dr["Titulo"].ToString();
                                Catalogo.Catalogo_strNroPag = dr["NroPagina"].ToString();
                                Catalogo.Catologo_Orden = dr["Orden"].ToString();
                                Catalogo.Catalogo_Estado = dr["Est_Id"].ToString();


                            }
                       
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Catalogo = null;
            }
            return Catalogo;
        }

        public int ActualizarListCatalogo(string strListCatalogo)
        {
   
            int idRespuesta = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    if (cn.State == 0) cn.Open();

                    SqlCommand oComando = new SqlCommand("USP_Insertar_ListCatalogo", cn);
                    oComando.CommandType = CommandType.StoredProcedure;
                 
                    SqlParameter oCatalogo_strLista = oComando.Parameters.Add("@Catalogo_strList", SqlDbType.VarChar);
                    oCatalogo_strLista.Direction = ParameterDirection.Input;
                    oCatalogo_strLista.Value = strListCatalogo;

                    SqlParameter oIdRespuesta = oComando.Parameters.Add("@Respuesta_id", SqlDbType.Int);
                    oIdRespuesta.Direction = ParameterDirection.ReturnValue;

                    oComando.ExecuteNonQuery();
                    idRespuesta = (int)oIdRespuesta.Value;

                }

            }
            catch (Exception ex)
            {
                idRespuesta = -1;
                //valida = false;
            }
            return idRespuesta;
        }

    }
}