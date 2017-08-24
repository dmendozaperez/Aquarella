using System;
using System.Data;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll
{
    public class Article_Catalog
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Consultar articulos segun catalogo, pagina y bodega para el stock
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_catalogId"></param>
        /// <param name="_page"></param>
        /// <param name="_ware"></param>
        /// <returns></returns>
        public static DataSet getItemsByCatalog(string _co, string _catalogId, int _page, string _ware)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "maestros.sp_getitemsbycatalog";
            //    // CURSOR REF
            //    object results = new object[1];
            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    //
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _catalogId, _page, _ware, results);
            //    // DataSet that will hold the returned results
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion
    }
}