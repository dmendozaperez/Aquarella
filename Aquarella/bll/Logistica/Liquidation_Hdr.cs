using System;
using System.Data;
using System.Data.Common;

namespace Aquarella.bll
{
    class Liquidation_Hdr
    {
        //variables estaticos de la globales
        public static string _liq_id { set; get; }
        public static decimal _liq_bas_id { set; get; }
        public static DateTime _liq_fecha_ing { set; get; }
        public static decimal _liq_guia { set; get; }
        public static decimal _liq_total { set; get; }

        //


        public String _ldv_liquidation_no { set; get; }
        public DateTime _lhd_date { set; get; }
        public Decimal _lhn_customer { set; get; }
        public String _lhv_customer_name { set; get; }
        public String _lhv_ware { set; get; }
        public Decimal _qtystotals { set; get; }
        public Decimal _pdn_qty { set; get; }
        public Decimal _lhn_guide { set; get; }
        public String _lhv_trans_type { set; get; }
        public String _lhv_transporter { set; get; }
        public String _lhv_guide_no { set; get; }
        public String _lhv_status { set; get; }
        public String _lhv_customer_ubication { set; get; }
        public String _lhv_area_desc { set; get; }

        public int _idAgencia { set; get; }
        public String _AGENCIA_TRANSP { set; get; }
        public int _CANT_GUIAS { set; get; }
        public DateTime _fecInvoice { set; get; }

        public String _pdv_co { set; get; }
        public int _BDN_ID { set; get; }
        public String _nombre { set; get; }
        public String _LHV_LIQUIDATION_NO { set; get; }
        public String _IHV_INVOICE_NO { set; get; }
        public int _PAN_NO { set; get; }
        public int _PREFIJO { set; get; }
        public int _NUMGUIA { set; get; }
        public int _cantidad { set; get; }
        public Decimal _costo { set; get; }
        public String _NUMCOMPLETO { set; get; }
        public String _RangoGuia { set; get; }
        public int _varCheck { set; get; }

        public String _Ag_transporte { set; get; }
        public int _SECUENCIALTRANSPORTISTA { set; get; }


        public string _bas_documento { set; get; }
        public string _bas_direccion { set; get; }

        #region < METODOS PUBLICOS >

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="lhv_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <returns></returns>
        public static DataSet getLiquidationHdr(String lhv_liquidation_no)
        {
            try
            {
              
                ///
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Consultar liquidaciones segun el estado
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_status"></param>
        /// <param name="_idWarehouse"></param>
        /// <returns></returns>
        public static DataSet getLiquidationsByStatus()
        {
            try
            {
               
                ///
                return null;
            }
            catch { return null; }
        }


        public static DataSet getTransportistaGuiaDB(DateTime fechIni, DateTime fechFin)
        {
            try
            {                
                ///
                return null;
            }
            catch { return null; }
        }

        public static DataSet getGuiaSecuencialDB(int TGN_TRANSPORT, DateTime fechFin)
        {
            try
            {

                return null;
            }
            catch { return null; }
        }



        public static DataSet getChooiseEditGuiaDB( int varSECUENCIALTRANSPORTISTA)
        {
            try
            {
                
                ///
                return null;
            }
            catch { return null; }
        }



        public static DataSet getViewGuiaforAddDB(int TGN_TRANSPORT, String varNUMCOMPLETO, int varSECUENCIALTRANSPORTISTA)
        {
            try
            {

                return null;
            }
            catch { return null; }
        }


        public static DataSet getViewGuiaEditDB(int TGN_TRANSPORT, DateTime fechFin)
        {
            try
            {
               
                ///
                return null;
            }
            catch { return null; }
        }


        public static object getNumGuiaDB()
        {
            try
            {
                ///


                return null;
                ///
                //return db.ExecuteScalar(dbCommandWrapper);

                // return db.ExecuteScalar(dbCommandWrapper);
            }
            catch { return null; }
        }    

        /// <summary>
        /// Actualizar un numero de guia a una determinada liquidacion      
        public static String updateGuideLiquidation(String lhv_liquidation_no,
            String tgn_guide_id)
        {
            try
            {                             
                ///
                return "1";
            }
            catch { return "-1"; }
        }

        #endregion
    }
}
