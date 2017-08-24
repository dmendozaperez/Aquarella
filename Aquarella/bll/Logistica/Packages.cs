using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
namespace Aquarella.bll
{
    class Packages
    {

        //variables globales

        public static decimal _paq_id { set; get; }
        public static string _paq_no { set; get; }

        public String _pan_employee { set; get; }
        public String _pan_no { set; get; }
        public Decimal _pdn_packageid { set; get; }
        public Decimal _pan_qty_total { set; get; }


        #region < METODOS PUBLICOS >

        /// <summary>
        /// Consultar Numero del siguiente paquete para una determinada liquidacion
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="pav_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <param name="_idUser"></param>
        /// <returns></returns>
        public static String addOrGetPackage(String lhv_liquidation_no, String _idUser)
        {
            ///
            try
            {
                String _idPaquete = "";
                ///
                //Database db = DatabaseFactory.CreateDatabase(_nameConn);
                /////
                //String sqlCommand = "logistica.sp_addorgetpackage";
                /////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);
                /////
                //db.AddInParameter(dbCommandWrapper, "p_ldv_co", DbType.String, pav_co);
                /////
                //db.AddInParameter(dbCommandWrapper, "p_ldv_liquidation", DbType.String, lhv_liquidation_no);
                /////
                //db.AddInParameter(dbCommandWrapper, "p_pan_employee", DbType.String, _idUser);
                /////
                ///// Output parameters specify the size of the return data.            
                ///// 
                //db.AddOutParameter(dbCommandWrapper, "p_pdn_package", DbType.String, 12);

                //db.ExecuteNonQuery(dbCommandWrapper);
                /////
                //_idPaquete = (String)db.GetParameterValue(dbCommandWrapper, "p_pdn_package");
                /////
                return _idPaquete;
            }
            catch
            {
                return "-1";
            }
        }

        /// <summary>
        /// Consultar el numero total de paquetes para una liquidacion determinada
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="pav_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <returns></returns>
        public static DataSet getMaxNoPackageByLiqui(String lhv_liquidation_no)
        {
            try
            {
                //// CURSOR REF
                //object results = new object[1];
                /////
                //Database db = DatabaseFactory.CreateDatabase(_nameConn);
                /////
                //String sqlCommand = "logistica.sp_getmaxnopackagebyliqui";
                /////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, pav_co, lhv_liquidation_no, results);
                /////
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Consultar informacion del paquete, mediante el id del paquete y el numero del pedido al cual pertenece
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="pav_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <param name="pdn_packageid"></param>
        /// <returns></returns>
        public static DataSet getPackagesByPrimaryKey(String lhv_liquidation_no, Decimal pdn_packageid)
        {
            try
            {
                // CURSOR REF
                //object results = new object[1];
                //Database db = DatabaseFactory.CreateDatabase(_nameConn);
                /////
                //String sqlCommand = "logistica.sp_getpackagesbyprimarykey";
                /////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, pav_co, lhv_liquidation_no, pdn_packageid, results);
                ///
                return null;// db.ExecuteDataSet(dbCommandWrapper);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Consultar toda la informacion asociada a un paquete
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="pav_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <param name="pdn_packageid"></param>
        /// <returns></returns>
        public static DataSet getPackage( String lhv_liquidation_no, Decimal pdn_packageid)
        {
            try
            {
                // CURSOR REF
                //object results = new object[1];
                //Database db = DatabaseFactory.CreateDatabase(_nameConn);
                /////
                //String sqlCommand = "logistica.sp_getpackage";

                /////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, pav_co, lhv_liquidation_no, pdn_packageid, results);
                ///
                return null;// db.ExecuteDataSet(dbCommandWrapper);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Actualizar el peso de un paquete
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="pav_co"></param>
        /// <param name="pdn_packageid"></param>
        /// <param name="pan_weight"></param>
        /// <returns></returns>
        public static String updatePackageWeigth(Decimal pdn_packageid, Decimal pan_weight)
        {
            ///
            try
            {
                String _idPaquete = "";
                ///
                //Database db = DatabaseFactory.CreateDatabase(_nameConn);
                /////
                //String sqlCommand = "logistica.sp_updatepackageweigth";
                /////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);
                /////
                //db.AddInParameter(dbCommandWrapper, "p_pav_co", DbType.String, pav_co);
                /////
                //db.AddInParameter(dbCommandWrapper, "p_pan_packageid", DbType.Decimal, pdn_packageid);
                /////
                //db.AddInParameter(dbCommandWrapper, "p_pan_weight", DbType.Decimal, pan_weight);

                /////
                //db.ExecuteNonQuery(dbCommandWrapper);
                ///
                return _idPaquete;
            }
            catch
            {
                return "-1";
            }
        }


        #endregion
    }
}
