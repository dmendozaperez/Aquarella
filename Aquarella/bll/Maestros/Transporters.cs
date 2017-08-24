using System;
using System.Data;
using System.Data.Common;

namespace Aquarella.bll
{
    class Transporters
    {
        public String _trv_co { set; get; }
        public String _trv_transporters_id { set; get; }
        public String _trv_name { set; get; }
        public String _trv_address { set; get; }
        public String _trv_phone { set; get; }

        #region < METODOS ESTATICOS - PUBLICOS >

        /// <summary>
        /// Consultar todas las transportadoras 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public static DataSet getAllTransportsByCompany()
        {
            try
            {
                //// CURSOR REF
                //object results = new object[1];
                /////
                //Database db = DatabaseFactory.CreateDatabase(_nameConn);
                //String sqlCommand = "maestros.sp_getalltransporters";
                /////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, results);
                ////DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
                //return db.ExecuteDataSet(dbCommandWrapper);
                return null;
            }
            catch { return null; }
        }

        #endregion
    }
}
