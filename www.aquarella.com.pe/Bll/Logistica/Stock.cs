using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll
{
    public class Stock
    {
        #region < Atributos >

        public string _codeItem { get; set; }
        public string _nameItem { get; set; }
        public string _brandItem { get; set; }

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < Métodos estaticos >

        /// <summary>
        /// Obtiene el Articulo determinado del Storage determinado y el Warehouse detereminado.
        /// </summary>
        /// <param name="arv_article"></param>
        /// <returns>Todos los datos del Artículo</returns>
        public static DataSet getByArticle(String company, string arv_article, string warehouse, String storage)
        {
            DataSet ds = new DataSet();
            return ds;
            ///
            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "logistica.SP_GETARTICLE_STOCKV_ART";
            /////
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, arv_article, warehouse, storage, results);
            //DataSet ret = db.ExecuteDataSet(dbCommandWrapper);
            //return ret;
        }

        /// <summary>
        /// Obtiene el Articulo determinado con todas sus tallas en cero y costo y precio publico.
        /// </summary>
        /// <param name="arv_article"></param>
        /// <returns>Todos los datos del Artículo</returns>
        /// 
        public static string recalcular_stock()
        {
            string sqlquery = "[USP_Actualizar_Stock]";
            string vmensaje = "";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();                
            }
            catch (Exception exc)
            {
                vmensaje = exc.Message;
            }
            return vmensaje;
        }


        public static DataSet getByArticle(String company, string arv_article)
        {
            DataSet ds = new DataSet();
            return ds;
            ///
            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "MAESTROS.SP_GETVARTICLE_FOR_SIZES";
            /////
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, arv_article, results);
            //DataSet ret = db.ExecuteDataSet(dbCommandWrapper);
            //return ret;
        }


        /// <summary>
        /// Consultar el stock de un articulo; Se muestran todas las tallas del articulo y las cantidades de estas en el stock
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idWare"></param>
        /// <param name="_idSto"> Puede enviar el _idStorage en blanco y se tomara por defecto el storage de estanterias</param>
        /// <param name="_art"></param>
        /// <returns></returns>
        public static DataSet getStockByArtWithAllSizes(string _co, string _idWare, string _idSto, string _art, string noOrder)
        {
            string sqlquery = "USP_Leer_StkArticuloTalla";
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
                cmd.Parameters.AddWithValue("@art_id", _art);
                cmd.Parameters.AddWithValue("@liq_id", noOrder);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_idWarehouse"></param>
        /// <param name="_idStorage"></param>
        /// <returns></returns>
        public static DataTable getStockOnlyArticlesByWareAndStorage(String _company, String _idWarehouse, String _idStorage)
        {

            DataTable ds = new DataTable();
            return ds;
            //try
            //{
            //    ///
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "logistica.sp_getstockarticlessbyware";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _idWarehouse, _idStorage, results);
            //    DataTable ret = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    return ret;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_idWarehouse"></param>
        /// <param name="_idStorage"></param>
        /// <param name="_article"></param>
        /// <returns></returns>
        public static DataTable getStockArticleByWareAndStorage(String _company, String _idWarehouse, String _idStorage, String _article)
        {
            DataTable ds = new DataTable();
            return ds;
            //try
            //{
            //    ///
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "logistica.sp_getstockarticlexwarestorage";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _idWarehouse, _idStorage, _article, results);
            //    DataTable ret = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    return ret;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar las cantidades en stock de una articulo en una talla especifica
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_code"></param>
        /// <param name="_size"></param>
        /// <param name="_ware"></param>
        /// <returns></returns>
        public static DataSet getStockArticleSize(string _code, string _size,string _liquid="")
        {
            string sqlquery = "USP_Leer_Stock_ArtTalla";
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
                cmd.Parameters.AddWithValue("@art_id", _code);
                cmd.Parameters.AddWithValue("@Tal_Id",_size);
                cmd.Parameters.AddWithValue("@liq_id", _liquid);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        // <summary>
        /// Obtiene la cantidad del Articulo por tallas y todos sus datos.
        /// </summary>
        /// <param name="sov_co">Codigo de la Compañia</param>
        /// <param name="sov_warehouse">Codigo de la Bodega</param>
        /// <param name="sov_article">Codigo del articulo, puede utilizar el comodin %</param>
        /// <param name="stv_publicacces">T storages de acceso publico, F storages no publicos, TF ambos</param>
        /// <returns>Todos los datos del Artículo, tallas y cantidad</returns>
        public static DataSet getArticleStock(string _art)
        {
            string sqlquery = "USP_Leer_Articulo_Stock";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                ///
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@art_id", _art);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static DataSet getAllcategoria()
        {
            string sqlquery = "USP_Leer_CategoriaPrincipal";
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
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet get_stockhiguereta(DateTime fe_ini,DateTime fec_fin)
        {
            DataSet ds = null;
            string sqlquery = "USP_ConsultaStkHVenta";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.myconexion()))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FECHA_INI", fe_ini);
                        cmd.Parameters.AddWithValue("@FECHA_FIN", fec_fin);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);
                        }

                    }
                }
            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }

        public static DataSet getAlltemporada()
        {
            string sqlquery = "USP_Leer_Temporada";
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
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataSet getstockcategoria(string _idcategoria,string _temporada)
        {
            string sqlquery = "USP_LeerStk_CateDet";
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
                cmd.Parameters.AddWithValue("@cat_pri_id", _idcategoria);
                cmd.Parameters.AddWithValue("@tempo", _temporada);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion

    }
}