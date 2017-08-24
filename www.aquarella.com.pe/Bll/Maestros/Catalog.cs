using System;
using System.Data;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll
{
    public class Catalog
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        /// <summary>
        /// Id catalogo
        /// </summary>
        public string _idCatalog { get; set; }
        /// <summary>
        /// Nombre o descripcion
        /// </summary>
        public string _description { get; set; }
        /// <summary>
        /// Número de paginas que componen el catalogo
        /// </summary>
        public int _pages { get; set; }
        /// <summary>
        /// Fecha de inicio del catalogo
        /// </summary>
        public DateTime _initDate { get; set; }
        /// <summary>
        /// Fecha de finalizacion del catalogo
        /// </summary>
        public DateTime _endDate { get; set; }
        /// <summary>
        /// Descripción adicional
        /// </summary>
        public string _descAdd { get; set; }

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Consulta todos los catalogos existentes
        /// </summary>
        /// <returns></returns>
        public static DataSet getAllCalatogues()
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "maestros.sp_loadall_catalog";
            //    // CURSOR REF
            //    object results = new object[1];
            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///                
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion
    }
}