using System;
using System.Data;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace www.aquarella.com.pe.bll
{
    public class Payments
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion


        #region < Metodos estaticos >

        /// <summary>
        /// Realizar el registro de un recaudo financiero
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_custId"></param>
        /// <param name="_bank"></param>
        /// <param name="_noConsig"></param>
        /// <param name="_datePay"></param>
        /// <param name="_amount"></param>
        /// <param name="_comm"></param>        
        /// <returns></returns>
        /// 

        public static DataSet archivo_data_ingreso(DateTime _fechaini,DateTime _fechafin)
        {
            string sqlquery = "USP_Leer_Archivo_Banco_Ing";
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
                cmd.Parameters.AddWithValue("@fecha_ini", _fechaini);
                cmd.Parameters.AddWithValue("@fecha_fin", _fechafin);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }

        public static DataSet archivo_data_banco()
        {
            string sqlquery = "USP_Leer_Archivo_Banco";
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
            catch
            {
                return null;
            }
        }

        public static bool existe_op(string _op)
        {
            string sqlquery = "USP_Existe_OP";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            Int32 _valor;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@n_op", _op);
                cmd.Parameters.Add("@existe", SqlDbType.Int);
                cmd.Parameters["@existe"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                _valor = Convert.ToInt32(cmd.Parameters["@existe"].Value);
                if (_valor == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }
            catch
            {
                return false;
            }
        }

        public static string valida_pedido_pago(Decimal _bas_id, string _liq)
        {
            string _valida = "";
            string sqlquery = "USP_Verificar_Pago_Pedido";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", _bas_id);
                cmd.Parameters.AddWithValue("@liq_id", _liq);
                cmd.Parameters.Add("@valida", SqlDbType.VarChar, 1);
                cmd.Parameters["@valida"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _valida = cmd.Parameters["@valida"].Value.ToString();
            }
            catch
            {
                 _valida = "-1";
            }
            return _valida;
        }

        public static bool savePayment(decimal _custId, string _bank, string _noConsig, DateTime _datePay, decimal _amount,
            string _typePay, string _comm,decimal _usuario,string _pedido="")
        {
            string sqlquery = "USP_Insertar_Pago";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            bool result = false;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", _custId);
                cmd.Parameters.AddWithValue("@pag_banid", _bank);
                cmd.Parameters.AddWithValue("@pag_num_consignacion", _noConsig);
                cmd.Parameters.AddWithValue("@pag_fecha", _datePay);
                cmd.Parameters.AddWithValue("@Total", _amount);

                cmd.Parameters.AddWithValue("@Con_Id", _typePay);
                cmd.Parameters.AddWithValue("@pag_comentario", _comm);
                cmd.Parameters.AddWithValue("@pag_usu_creacion", _usuario);
                cmd.Parameters.AddWithValue("@pedido", _pedido);

                

                cmd.ExecuteNonQuery();
                result = true;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
            return result;           
        }

        /// <summary>
        /// Consultar pagos por estado, bodega y area
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_status"></param>
        /// <param name="_idWare"></param>
        /// <returns></returns>
        public static DataSet loadPaymentsByWarehouseAndStatus(string _status, string _idArea)
        {            
            string sqlquery = "USP_Leer_PagosYEstado";
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
                cmd.Parameters.AddWithValue("@est_id", _status);
                cmd.Parameters.AddWithValue("@are_id", _idArea);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataSet loadpagoalcredito()
        {
            string sqlquery = "USP_Leer_Liquidacion_Credito";
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

        public static void sbactualizar()
        {
            //string sqlcommand = "FINANCIERA.SP_UPDATE_PAYMENTS_AUTO_BANK";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlcommand);
            //db.ExecuteNonQuery(dbCommandWrapper);
        }

        /// <summary>
        /// Actualizar estado de un pago
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_paymentId"></param>
        /// <param name="_status"></param>
        /// <returns></returns>
        /// 
        //en este procedure vamos a validar pagos al credito

        public static bool updatepagocredito(string _liquidacion, string _status,Decimal _usu_ing)
        {
            bool _valor = false;
            string sqlquery="USP_Generar_Liquidacion_Credito";
            SqlConnection cn=null;
            SqlCommand cmd=null;
            try
            {
                cn=new SqlConnection(Conexion.myconexion());
                if (cn.State==0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _liquidacion);
                cmd.Parameters.AddWithValue("@liq_estado", _status);
                cmd.Parameters.AddWithValue("@Usu_ingreso", _usu_ing);
                cmd.ExecuteNonQuery();
                _valor = true;
            }
            catch
            {
                _valor = false;
            }
            return _valor;            
        }



        public static bool updatePayment(string _paymentId, string _status)
        {
            string sqlquery = "USP_Modificar_Pago_Estado";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pag_id", _paymentId);
                cmd.Parameters.AddWithValue("@est_id", _status);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch(Exception)
            {
                return false;
            }

            //return false;
            //bool result = false;
            //string sqlCommand = "financiera.sp_update_payments";

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co,
            //     _paymentId, _status);
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //        // Commit the transaction.
            //        transaction.Commit();

            //        result = true;
            //    }
            //    catch (Exception)
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //    }
            //    connection.Close();
            //    return result;
            //}
        }

        /// <summary>
        /// Consultar el listado de pagos de un cliente
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idCust"></param>
        /// <returns></returns>
        static public DataSet getPaymentsByCoordinator(int _idCust)
        {
            string sqlquery = "USP_Leer_PagoXPersona";
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
                cmd.Parameters.AddWithValue("@basid", _idCust);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        static public DataSet getPayments_Update(int _idCust)
        {
            string sqlquery = "USP_Leer_PagoXPersona";
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
                cmd.Parameters.AddWithValue("@basid", _idCust);
                cmd.Parameters.AddWithValue("@estado", 1);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                           
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        static public DataSet get_Montos(DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_Leer_PagoXPersona";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {

                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout=0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaini", _date_start.ToString("d/MM/yy"));
                cmd.Parameters.AddWithValue("@fechafinal", _date_end.ToString("d/MM/yy"));
                cmd.Parameters.AddWithValue("@estado", 2);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
                                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        public static DataSet getSalesCoordByMonth(String _co, int _idCust, DateTime _date_start, DateTime _date_end)
        {
            DataSet ds = new DataSet();
            return ds;
            //         
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    //
            //    string sqlCommand = "ventas.sp_getsalescoordbymonth";
            //    //
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _idCust, _date_start, _date_end, results);
            //    //
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet leer_saldo_cliente(Decimal _bas_id, string _conid, DateTime _date_start, DateTime _date_end, string _usu_tipo)
        {
            string sqlquery = "USP_Leer_Saldos_Pendientes";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                if (_usu_tipo == null) _usu_tipo = "";

                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BAS_ID", _bas_id);
                cmd.Parameters.AddWithValue("@CON_ID", _conid);
                cmd.Parameters.AddWithValue("@fecha_ini", _date_start);
                cmd.Parameters.AddWithValue("@fecha_fin", _date_end);
                cmd.Parameters.AddWithValue("@Usu_Tipo", _usu_tipo);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getSalesCoorByMonthPctg(int _are_id, String _asesor, DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_Leer_ComisionPersona";
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
                cmd.Parameters.AddWithValue("@are_id", _are_id);
                cmd.Parameters.AddWithValue("@fecha_ini", _date_start);
                cmd.Parameters.AddWithValue("@fecha_fin", _date_end);
                cmd.Parameters.AddWithValue("@asesor", _asesor);
                //if (cn.State == 0) cn.Open();
                //cmd.ExecuteNonQuery();
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        public static DataSet get_ventasPago(String _nfactura, String _dtvconceptid, Int32 _dtncoord, Int32 _rangofecha, DateTime _fechaini, DateTime _fechafin)
        {
            DataSet ds = new DataSet();
            return ds;

            //try
            //{
            //    if (_dtvconceptid == "-1") { _dtvconceptid = ""; }
            //    if (_dtncoord == -1) { _dtncoord = 0; }

            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "ventas.sp_ventasMovimiento";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _nfactura, _dtvconceptid, _dtncoord, _rangofecha, _fechaini, _fechafin, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getventaunmo( int _idcust, DateTime _date_start, DateTime _date_end,string _asesor)
        {
            string sqlquery = "USP_Leer_Venta_UniMon";
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
                cmd.Parameters.AddWithValue("@are_id", _idcust);
                cmd.Parameters.AddWithValue("@fecha_inicio", _date_start);
                cmd.Parameters.AddWithValue("@fecha_final", _date_end);
                cmd.Parameters.AddWithValue("@asesor", _asesor);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataSet getsaldocliente(DateTime _date_start, DateTime _date_end)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "FINANCIERA.USP_SaldoClientes";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _date_start, _date_end, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getsaldoclientedet(DateTime _fecha, string _dniruc)
        {
            DataSet ds = new DataSet();
            return ds;

            //try
            //{
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "FINANCIERA.USP_SaldoClientes_dtl";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _fecha, _dniruc, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getclientedniruc(string var_rucdni)
        {
            string sqlquery = "USP_Leer_Clientes_Finanzas";
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
                cmd.Parameters.AddWithValue("@p_dniruc", var_rucdni);
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