using informativa.aquarella.com.oe.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Data
{
    public class PasarelaDA
    {

        public List<Ent_PasarelaDetalle> get_listaPasarelaDetalle()
        {
            string sqlquery = "USP_GetPasarelaDetalle";
            List<Ent_PasarelaDetalle> listar = null;
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
                            listar = new List<Ent_PasarelaDetalle>();
                            listar = (from DataRow dr in dt.Rows
                                      select new Ent_PasarelaDetalle()
                                      {
                                          PasarelaDet_id = Convert.ToInt32(dr["IdPasarelaDetalle"]),
                                          Pasarela_id = Convert.ToInt32(dr["IdPasarela"]),
                                          PasarelaDet_Ruta = dr["Ruta"].ToString(),
                                          PasarelaDet_Nombre = (dr["NombreImagen"]).ToString(),
                                          PasarelaDet_Orden = Convert.ToInt32(dr["Orden"]),
                                          PasarelaDet_Estado = dr["Est_Id"].ToString(),

                                      }).ToList();

                        }
                    }
                }
            }
            catch (Exception exc)
            {

                listar = null;
            }
            return listar;
        }

        public List<Ent_Pasarela> get_listaPasarela()
        {
            string sqlquery = "USP_GetLisPasarela";
            List<Ent_Pasarela> listar = null;
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
                            listar = new List<Ent_Pasarela>();
                            listar = (from DataRow dr in dt.Rows
                                      select new Ent_Pasarela()
                                      {
                                          Pasarela_id = Convert.ToInt32(dr["IdPasarela"]),
                                          Pasarela_Titulo = dr["Titulo"].ToString(),
                                          Pasarela_Descripcion = dr["Descripcion"].ToString(),
                                          Pasarela_Estado = (dr["Est_id"]).ToString(),

                                      }).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                listar = null;
            }
            return listar;
        }

        public int InsertarPasarela(Ent_Pasarela pasarela)
        {
            //string sqlquery = "USP_Insertar_pasarela";
            int idPasarela = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    if (cn.State == 0) cn.Open();

                    SqlCommand oComando = new SqlCommand("USP_Insertar_pasarela", cn);
                    oComando.CommandType = CommandType.StoredProcedure;

                    SqlParameter oPasarela_id = oComando.Parameters.Add("@Pasarela_id", SqlDbType.Int);
                    oPasarela_id.Direction = ParameterDirection.Input;
                    oPasarela_id.Value = pasarela.Pasarela_id;

                    SqlParameter oPasarela_titulo = oComando.Parameters.Add("@Pasarela_titulo", SqlDbType.VarChar);
                    oPasarela_titulo.Direction = ParameterDirection.Input;
                    oPasarela_titulo.Value = pasarela.Pasarela_Titulo;

                    SqlParameter oPasarela_descripcion = oComando.Parameters.Add("@Pasarela_descripcion", SqlDbType.VarChar);
                    oPasarela_descripcion.Direction = ParameterDirection.Input;
                    oPasarela_descripcion.Value = pasarela.Pasarela_Descripcion;

                    SqlParameter oPasarela_estado = oComando.Parameters.Add("@Pasarela_estado", SqlDbType.VarChar);
                    oPasarela_estado.Direction = ParameterDirection.Input;
                    oPasarela_estado.Value = pasarela.Pasarela_Estado;

                    SqlParameter oPasarela_strDetalle = oComando.Parameters.Add("@Pasarela_strDetalle", SqlDbType.VarChar);
                    oPasarela_strDetalle.Direction = ParameterDirection.Input;
                    oPasarela_strDetalle.Value = pasarela.Pasarela_strDetalle;

                    SqlParameter oPasarela_strRuta = oComando.Parameters.Add("@Pasarela_strRuta", SqlDbType.VarChar);
                    oPasarela_strRuta.Direction = ParameterDirection.Input;
                    oPasarela_strRuta.Value = pasarela.Pasarela_strRuta;

                    SqlParameter oPasarela_UsuCrea = oComando.Parameters.Add("@Pasarela_usuCrea", SqlDbType.VarChar);
                    oPasarela_UsuCrea.Direction = ParameterDirection.Input;
                    oPasarela_UsuCrea.Value = pasarela.Pasarela_UsuCrea;

                    SqlParameter oPasarela_IpCrea = oComando.Parameters.Add("@Pasarela_IpCrea", SqlDbType.VarChar);
                    oPasarela_IpCrea.Direction = ParameterDirection.Input;
                    oPasarela_IpCrea.Value = pasarela.Pasarela_Ip;

                    SqlParameter oIdRespuesta = oComando.Parameters.Add("@Pasarela_id", SqlDbType.Int);
                    oIdRespuesta.Direction = ParameterDirection.ReturnValue;

                    oComando.ExecuteNonQuery();
                    idPasarela = (int)oIdRespuesta.Value;
                    
                }

            }
            catch (Exception exc)
            {
                idPasarela = -1;
                //valida = false;
            }
            return idPasarela;


        }
            

        public Ent_Pasarela GetPasarela(string strId)
        {
            Ent_Pasarela Pasarela = null;
            List<Ent_PasarelaDetalle> lisDetalle = null;
            Int32 IdPasarela = 0;
            string Titulo = "";
            string Descripcion = "";
            string Estado = "";
           
            string sqlquery = "USP_Pasarela_IND";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Pasarela_id", strId);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            Pasarela = new Ent_Pasarela();
                            lisDetalle = new List<Ent_PasarelaDetalle>();



                            while (dr.Read())
                            {
                                Ent_PasarelaDetalle pasarelaDetalle = new Ent_PasarelaDetalle();

                                IdPasarela = Int32.Parse(dr["IdPasarela"].ToString());
                                Titulo = dr["Titulo"].ToString();
                                Descripcion = dr["Descripcion"].ToString();
                                Estado = dr["Est_Id"].ToString();

                                pasarelaDetalle.PasarelaDet_id = Int32.Parse(dr["IdPasarelaDetalle"].ToString());
                                pasarelaDetalle.PasarelaDet_Ruta = dr["Ruta"].ToString();
                                pasarelaDetalle.PasarelaDet_Nombre = dr["NombreImagen"].ToString();
                                pasarelaDetalle.PasarelaDet_Orden = Int32.Parse(dr["Orden"].ToString());

                               lisDetalle.Add(pasarelaDetalle);

                            }

                            Pasarela.Pasarela_ListDetalle = lisDetalle;
                            Pasarela.Pasarela_id = Int32.Parse(strId);
                            Pasarela.Pasarela_Titulo = Titulo;
                            Pasarela.Pasarela_Descripcion = Descripcion;
                            Pasarela.Pasarela_Estado = Estado;


                        }
                    }
                }
            }
            catch (Exception)
            {

                Pasarela = null;
            }
            return Pasarela;
        }
  
    }
}