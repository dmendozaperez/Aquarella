using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll
{
    public class Order_Hdr
    {
        #region < Atributos >

        public int _qtys { get; set; }
        public decimal _subTotal { get; set; }
        public decimal _subTotalOPG { get; set; }
        public string  _subTotalOPGDesc { get; set; }
        public string _subTotalDesc { get; set; }
        public decimal _taxes { get; set; }
        public string _taxesDesc { get; set; }
        public decimal _grandTotal { get; set; }
        public string _grandTotalDesc { get; set; }
        public decimal _percepcion { get; set; }
        public string _percepciondesc { get; set; }
        public decimal _mtopercepcion { get; set; }
        public string _mtopercepciondesc { get; set; }

        public string _namecompleto { get; set; }
        public string _estadoliqui { get; set; }

        public decimal _mtoncredito { get; set; }
        public string _mtoncreditodesc { get; set; }

        public Int32 _estadoboton { get; set; }

        public string _estadocredito { get; set; }

        public string _premio { get; set; }

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        /// <summary>
        /// Guardar una lista de pedido, cabecera y detalle
        /// </summary>
        /// <param name="_co">Comapñia</param>
        /// <param name="_idCust">Id promotor asociado a la lista de pedido</param>
        /// <param name="_reference"></param>
        /// <param name="_discCommPctg"></param>
        /// <param name="_discCommValue"></param>
        /// <param name="_shipTo"></param>
        /// <param name="_specialInstr"></param>
        /// <param name="_itemsDetail"></param>
        /// <returns></returns>
        public static string saveCompleteOrder(decimal _usu, decimal _idCust, string _reference, decimal _discCommPctg,
                                                decimal _discCommValue, string _shipTo, string _specialInstr, List<Order_Dtl> _itemsDetail, 
                                                decimal _varpercepcion,Int32 _estado,string _ped_id="",Decimal _porc_percepcion=0)
        {
            string sqlquery = "USP_Insertar_Modificar_Pedido";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Ped_Det_Id", typeof(string));
                dt.Columns.Add("Ped_Det_Items", typeof(Int32));
                dt.Columns.Add("Ped_Det_ArtId", typeof(string));
                dt.Columns.Add("Ped_Det_TalId", typeof(string));
                dt.Columns.Add("Ped_Det_Cantidad", typeof(Int32));
                dt.Columns.Add("Ped_Det_Costo", typeof(decimal));
                dt.Columns.Add("Ped_Det_Precio", typeof(decimal));
                dt.Columns.Add("Ped_Det_ComisionP", typeof(decimal));
                dt.Columns.Add("Ped_Det_ComisionM", typeof(decimal));
                dt.Columns.Add("Ped_Det_OfertaP", typeof(Decimal));
                dt.Columns.Add("Ped_Det_OfertaM", typeof(Decimal));
                dt.Columns.Add("Ped_Det_OfeID", typeof(Decimal));
                dt.Columns.Add("Ped_Det_PremID", typeof(Int32));
                //,Ped_Det_OfertaM,Ped_Det_OfeID



                int i = 1;
                // Recorrer todas las lineas adicionAQUARELLAs al detalle
                foreach (Order_Dtl item in _itemsDetail)
                {
                    dt.Rows.Add(_ped_id, i, item._code, item._size, item._qty, 0,item._price, item._commissionPctg, item._commission,item._ofe_porc,item._dscto,item._ofe_id, Convert.ToInt32(item._premId));
                    i++;
                }

                //grabar pedido
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Estado", _estado);
                cmd.Parameters.AddWithValue("@Ped_Id", _ped_id);
                cmd.Parameters.AddWithValue("@Ped_BasId", _idCust);
                cmd.Parameters.AddWithValue("@Ped_Por_Com", _discCommPctg);
                cmd.Parameters.AddWithValue("@Ped_Mto_Perc", _varpercepcion);
                cmd.Parameters.AddWithValue("@Ped_Usu", _usu);
                cmd.Parameters.AddWithValue("@Ped_Por_Perc", _porc_percepcion);
                cmd.Parameters.AddWithValue("@Detalle_Pedido", dt);
                cmd.ExecuteNonQuery();
            
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
            return "";           
        }

        public static string modifyCompleteOrder(string _co, string _noOrder, string _reference, decimal _discCommPctg,
            decimal _discCommValue, string _shipTo, string _specialInstr, List<Order_Dtl> _itemsDetail, decimal _MontoPercepcion)
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string resultDoc = "";
            //string sqlCommand = "logistica.sp_modifyorder";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_odv_co", DbType.String, _co);

            //db.AddInParameter(dbCommandWrapper, "p_odv_order_no", DbType.String, _noOrder);

            //db.AddInParameter(dbCommandWrapper, "p_ohv_reference", DbType.String, _reference);
            //db.AddInParameter(dbCommandWrapper, "p_OHN_DISC_COMM_PCTG", DbType.Decimal, _discCommPctg);
            //db.AddInParameter(dbCommandWrapper, "p_OHN_DISC_COMM_VALUE", DbType.Decimal, _discCommValue);


            //db.AddInParameter(dbCommandWrapper, "p_OHV_SHIP_TO", DbType.String, _shipTo);
            //db.AddInParameter(dbCommandWrapper, "p_OHV_SPECIAL_INSTR", DbType.String, _specialInstr);
            //db.AddInParameter(dbCommandWrapper, "p_ohv_Percepcion", DbType.Decimal, _MontoPercepcion);


            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        int i = 1;
            //        // Recorrer todas las lineas adicionAQUARELLAs al detalle
            //        foreach (Order_Dtl item in _itemsDetail)
            //        {
            //            sqlCommand = "logistica.SP_ADD_ORDER_DTL";
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                _co,
            //                _noOrder,
            //                i,
            //                item._code,
            //                item._size,
            //                item._qty,
            //                item._qtyCancel,
            //                item._dsctoPerc,// % de descuento
            //                item._dscto,// Valor de descuento
            //                item._commissionPctg,// % de comision
            //                item._commission//valor de comision
            //                );
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            i++;
            //        }

            //        // Commit the transaction.
            //        transaction.Commit();
            //        resultDoc = "1";
            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        resultDoc = "0";
            //    }
            //    connection.Close();
            //}
            //return resultDoc;
        }

        //actualizacion de liquidacion
        public static string modifyCompleteliquidacion(string _co, string _noOrder, string _reference, decimal _discCommPctg,
    decimal _discCommValue, string _shipTo, string _specialInstr, List<Order_Dtl> _itemsDetail, decimal _MontoPercepcion)
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string resultDoc = "";
            //string sqlCommand = "LOGISTICA.USP_modifyliquidacion";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_odv_co", DbType.String, _co);

            //db.AddInParameter(dbCommandWrapper, "p_odv_order_no", DbType.String, _noOrder);

            //db.AddInParameter(dbCommandWrapper, "p_ohv_reference", DbType.String, _reference);
            //db.AddInParameter(dbCommandWrapper, "p_OHN_DISC_COMM_PCTG", DbType.Decimal, _discCommPctg);
            //db.AddInParameter(dbCommandWrapper, "p_OHN_DISC_COMM_VALUE", DbType.Decimal, _discCommValue);


            //db.AddInParameter(dbCommandWrapper, "p_OHV_SHIP_TO", DbType.String, _shipTo);
            //db.AddInParameter(dbCommandWrapper, "p_OHV_SPECIAL_INSTR", DbType.String, _specialInstr);
            //db.AddInParameter(dbCommandWrapper, "p_ohv_Percepcion", DbType.Decimal, _MontoPercepcion);


            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        int i = 1;
            //        // Recorrer todas las lineas adicionAQUARELLAs al detalle
            //        foreach (Order_Dtl item in _itemsDetail)
            //        {
            //            sqlCommand = "logistica.SP_ADD_ORDER_DTL";
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                _co,
            //                _noOrder,
            //                i,
            //                item._code,
            //                item._size,
            //                item._qty,
            //                item._qtyCancel,
            //                item._dsctoPerc,// % de descuento
            //                item._dscto,// Valor de descuento
            //                item._commissionPctg,// % de comision
            //                item._commission//valor de comision
            //                );
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            i++;
            //        }

            //        // Commit the transaction.
            //        transaction.Commit();
            //        resultDoc = "1";
            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        resultDoc = "0";
            //    }
            //    connection.Close();
            //}
            //return resultDoc;
        }
        //

        //vamos a pagar un pedido con pago POS
        public static string[] saveOrder_And_ped_POS_BL(string _co, decimal _idCust, string _reference, decimal _discCommPctg,
                                             decimal _discCommValue, string _shipTo, string _specialInstr, List<Order_Dtl> _itemsDetail,
                                             Transporters_Guides shipping, string _newStatus, string varNumTarjeta, string varNumVoucher,
                                             decimal varMonto, decimal usuCre, String TipoPago, decimal _varpercepcion, string listdoc,string vped)
        {
            string[] resultDoc = new string[3];
            return resultDoc;
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    //string resultDoc = "";

            //    string sqlCommand = "FINANCIERA.sp_POS_automatic_clear_UNICA";


            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);
            //    resultDoc[0] = vped;

            //    // Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //    using (DbConnection connection = db.CreateConnection())
            //    {
            //        connection.Open();
            //        DbTransaction transaction = connection.BeginTransaction();

            //        try
            //        {
                        

            //            //-- liquidation
                      
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, resultDoc[0], 0, _newStatus, varNumTarjeta, varNumVoucher, varMonto, TipoPago, usuCre, _varpercepcion, listdoc);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            resultDoc[1] = db.GetParameterValue(dbCommandWrapper, "p_lhv_liquidation_no").ToString();

            //            //** Quitar cuando el sistema funcione con los cambios en la tabla transporters_guides
            //            if (shipping._configShipping)
            //            {
            //                /* Solo para cuando este activo la nueva creacion de guias*/
            //                // Registro info destinatario
            //                sqlCommand = "logistica.sp_addguide_shipping";
            //                //
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, 12, DBNull.Value, shipping._tgn_transport, shipping._tgv_name_cust, shipping._tgv_phone_cust,
            //                    shipping._tgv_movil_cust, shipping._tgv_shipp_add, shipping._tgv_shipp_block, shipping._tgv_city, shipping._tgv_depto, resultDoc[1]);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            }


            //            // Commit the transaction.
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            // Roll back the transaction. 
            //            transaction.Rollback();
            //            resultDoc[0] = "-1";
            //            resultDoc[1] = "-1";
            //            resultDoc[2] = ex.Message;//"-1";
            //            //return ex.Message;
            //        }
            //        connection.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    resultDoc[0] = "-1";
            //    resultDoc[1] = "-1";
            //    resultDoc[2] = ex.Message;
            //}
            //return resultDoc;
        }



        //



        public static string[] saveOrder_And_Liquidation_POS_BL(string _co, decimal _idCust, string _reference, decimal _discCommPctg,
                                              decimal _discCommValue, string _shipTo, string _specialInstr, List<Order_Dtl> _itemsDetail,
                                              Transporters_Guides shipping, string _newStatus, string varNumTarjeta, string varNumVoucher,
                                              decimal varMonto, decimal usuCre, String TipoPago, decimal _varpercepcion, string listdoc)
        {
             
            string[] resultDoc = new string[3];
            return resultDoc;
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    //string resultDoc = "";

            //    string sqlCommand = "logistica.sp_add_order_hdr";

            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //    // Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //    db.AddInParameter(dbCommandWrapper, "p_ohv_co", DbType.String, _co);

            //    // Output parameters specify the size of the return data.            
            //    db.AddOutParameter(dbCommandWrapper, "p_ohv_order_no", DbType.String, 12);

            //    // Id del promotor
            //    db.AddInParameter(dbCommandWrapper, "p_ohn_customer_id", DbType.Decimal, _idCust);

            //    db.AddInParameter(dbCommandWrapper, "p_ohv_reference", DbType.String, _reference);
            //    db.AddInParameter(dbCommandWrapper, "p_ohn_disc_comm_pctg", DbType.Decimal, _discCommPctg);
            //    db.AddInParameter(dbCommandWrapper, "p_ohn_disc_comm_value", DbType.Decimal, _discCommValue);

            //    db.AddInParameter(dbCommandWrapper, "p_ohv_ship_to", DbType.String, _shipTo);
            //    db.AddInParameter(dbCommandWrapper, "p_ohv_special_instr", DbType.String, _specialInstr);

            //    db.AddInParameter(dbCommandWrapper, "p_ohv_Percepcion", DbType.Decimal, _varpercepcion);

            //    // Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //    using (DbConnection connection = db.CreateConnection())
            //    {
            //        connection.Open();
            //        DbTransaction transaction = connection.BeginTransaction();

            //        try
            //        {
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            resultDoc[0] = (string)db.GetParameterValue(dbCommandWrapper, "p_ohv_order_no");

            //            int i = 1;
            //            // Recorrer todas las lineas adicionAQUARELLAs al detalle
            //            foreach (Order_Dtl item in _itemsDetail)
            //            {
            //                sqlCommand = "logistica.sp_add_order_dtl";
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                    _co,
            //                    resultDoc[0],
            //                    i,// Linea
            //                    item._code,
            //                    item._size,
            //                    item._qty,
            //                    item._qtyCancel,
            //                    item._dsctoPerc,// % de descuento
            //                    item._dscto,// Valor de descuento
            //                    item._commissionPctg,// % de comision
            //                    item._commission//valor de comision
            //                    );
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //                i++;
            //            }

            //            //-- liquidation
            //            sqlCommand = "FINANCIERA.sp_POS_automatic_clear_UNICA";
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, resultDoc[0], 0, _newStatus, varNumTarjeta, varNumVoucher, varMonto, TipoPago, usuCre, _varpercepcion, listdoc);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            resultDoc[1] = db.GetParameterValue(dbCommandWrapper, "p_lhv_liquidation_no").ToString();

            //            //** Quitar cuando el sistema funcione con los cambios en la tabla transporters_guides
            //            if (shipping._configShipping)
            //            {
            //                /* Solo para cuando este activo la nueva creacion de guias*/
            //                // Registro info destinatario
            //                sqlCommand = "logistica.sp_addguide_shipping";
            //                //
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, 12, DBNull.Value, shipping._tgn_transport, shipping._tgv_name_cust, shipping._tgv_phone_cust,
            //                    shipping._tgv_movil_cust, shipping._tgv_shipp_add, shipping._tgv_shipp_block, shipping._tgv_city, shipping._tgv_depto, resultDoc[1]);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            }


            //            // Commit the transaction.
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            // Roll back the transaction. 
            //            transaction.Rollback();
            //            resultDoc[0] = "-1";
            //            resultDoc[1] = "-1";
            //            resultDoc[2] = ex.Message;//"-1";
            //            //return ex.Message;
            //        }
            //        connection.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    resultDoc[0] = "-1";
            //    resultDoc[1] = "-1";
            //    resultDoc[2] = ex.Message;
            //}
            //return resultDoc;
        }


        public static string Eliminar_Order_DTL(string _co, string _noOrder,
            decimal _discCommValue, string _shipTo, string _specialInstr)
        {
            return "";
            //try
            //{
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "logistica.sp_POS_DeleteOrder";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _noOrder, _discCommValue, _shipTo, _specialInstr);
            //    ///
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    ///
            //    return "1";
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message, e.InnerException);
            //}
        }

        public static DataSet fbuscarpedido_dniruc(String _dniruc)
        {
            string sqlquery = "USP_Leer_Consulta_LiqDoc";
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
                cmd.Parameters.AddWithValue("@bas_documento", _dniruc);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static Int32 fvalidastock(string article, string talla, Int32 cantidad,string nroliqui="")
        {
            //return 0;
            //Int32 vdevolvercantidad = 0;
            string sqlquery = "USP_VerificaStockArticulo";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            Int32 _devolver = 0;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@art_id", article);
                cmd.Parameters.AddWithValue("@Tal_Id", talla);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Liq_Id", nroliqui);
                cmd.Parameters.Add("@ValidaStock",SqlDbType.Decimal);
                cmd.Parameters["@ValidaStock"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _devolver = Convert.ToInt32(cmd.Parameters["@ValidaStock"].Value);

                
            }
            catch (Exception e)
            {
                if (cn!=null )
                if (cn.State == ConnectionState.Open) cn.Close();
                throw new Exception(e.Message, e.InnerException);
            }
            if (cn != null)
                if (cn.State == ConnectionState.Open) cn.Close();
            return _devolver;
        }
    }
}