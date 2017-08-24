using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;

namespace Aquarella.bll
{
    class Liquidation_HdrViewModel
    {       
        /// </summary>
        private ObservableCollection<Liquidation_Hdr>
            _LiquidationHdrOC = new ObservableCollection<Liquidation_Hdr>();

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Liquidation_Hdr> LiquidationHdr
        {
            get { return _LiquidationHdrOC; }
        }

        /// <summary>
        /// Consultar una cabecera de liquidacion y cargarla en un observable collection
        /// </summary>
        /// <param name="lhv_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <returns></returns>
        public ObservableCollection<Liquidation_Hdr> getLiquidationHdr(String lhv_liquidation_no)
        {
            try
            {
                ///
                //DataSet dsLiqHdr = Liquidation_Hdr.getLiquidationHdr(lhv_liquidation_no);
                /////
                //if (dsLiqHdr != null)
                //{
                //    foreach (DataRow dr in dsLiqHdr.Tables[0].Rows)
                //    {
                        ///
                        _LiquidationHdrOC.Add(new Liquidation_Hdr
                        {
                            _ldv_liquidation_no = Liquidation_Hdr._liq_id,
                            _lhn_customer = Liquidation_Hdr._liq_bas_id,
                            _lhd_date =Liquidation_Hdr._liq_fecha_ing,
                            _lhn_guide =Liquidation_Hdr._liq_guia,
                            _qtystotals =Liquidation_Hdr._liq_total
                        });
                //    }
                //}
                ///
                return _LiquidationHdrOC;
            }
            catch
            {
                return _LiquidationHdrOC;
            }
        }

        /// <summary>
        /// Consultar liquidaciones estado
        /// </summary>
        /// <param name="lhv_co"></param>
        /// <param name="lhv_status"></param>
        /// <param name="ware"></param>
        /// <returns></returns>
        public ObservableCollection<Liquidation_Hdr> getLiquidationsByStatus()
        {
            try
            {
                ///
                DataTable dtLiqHdr = Liquidacion.liquidacionXfacturar();
                ///
                if (dtLiqHdr != null)
                {
                    foreach (DataRow dr in dtLiqHdr.Rows)
                    {
                        ///
                        _LiquidationHdrOC.Add(new Liquidation_Hdr
                        {
                            _ldv_liquidation_no = dr["Liq_Id"].ToString(),
                            _lhn_customer = Convert.ToDecimal(dr["Liq_BasId"]),
                            _lhv_status = dr["Liq_EstId"].ToString(),
                            _lhv_customer_name = dr["nombres"].ToString(),
                            _lhd_date = Convert.ToDateTime(dr["Liq_FechaIng"]),
                            //_lhn_guide = (dr["lhn_guide"] is DBNull) ? 0 : Convert.ToDecimal(dr["lhn_guide"]),
                            _qtystotals = Convert.ToDecimal(dr["tpares"]),
                            _pdn_qty = Convert.ToDecimal(dr["cantidadp"]),
                            _lhv_customer_ubication = dr["ubicacion"].ToString(),
                            //_lhv_ware = dr["cov_warehouseid"].ToString(),
                            //_lhv_trans_type = (dr["htv_description"] is DBNull) ? "" : dr["htv_description"].ToString(),
                            _lhv_transporter = (dr["tra_descripcion"] is DBNull) ? "" : dr["tra_descripcion"].ToString(),
                            _lhv_area_desc = (dr["lider"] is DBNull) ? "" : dr["lider"].ToString(),
                            _lhv_guide_no = (dr["tra_gui_no"] is DBNull) ? "" : dr["tra_gui_no"].ToString(),
                            _bas_documento = (dr["bas_documento"] is DBNull) ? "" : dr["bas_documento"].ToString(),
                            _bas_direccion = (dr["bas_direccion"] is DBNull) ? "" : dr["bas_direccion"].ToString()
                        });
                    }
                }
                ///
                return _LiquidationHdrOC;
            }
            catch
            {
                return _LiquidationHdrOC;
            }
        }


        public ObservableCollection<Liquidation_Hdr> getTransportistaGuia(DateTime fechIni, DateTime fechFin)
        {
            try
            {
                ///
                DataSet dsLiqHdr = Liquidation_Hdr.getTransportistaGuiaDB( fechIni, fechFin);
                ///
                if (dsLiqHdr != null)
                {
                    foreach (DataRow dr in dsLiqHdr.Tables[0].Rows)
                    {
                        ///
                        _LiquidationHdrOC.Add(new Liquidation_Hdr
                        {

                            _idAgencia = Convert.ToInt32(dr["IDAGENCIA"]),
                            _AGENCIA_TRANSP = dr["AGENCIA_TRANSP"].ToString(),
                            _CANT_GUIAS = Convert.ToInt32(dr["CANT_GUIAS"]),
                            _fecInvoice = Convert.ToDateTime(dr["fecInvoice"])

                        });
                    }
                }
                ///
                return _LiquidationHdrOC;
            }
            catch
            {
                return _LiquidationHdrOC;
            }
        }

        public ObservableCollection<Liquidation_Hdr> getGuiaSecuencial(int idAgencia, DateTime fecInvoice)
        {
            try
            {
                ///
                DataSet dsLiqHdr = Liquidation_Hdr.getGuiaSecuencialDB( idAgencia, fecInvoice);
                ///
                if (dsLiqHdr != null)
                {
                    foreach (DataRow dr in dsLiqHdr.Tables[0].Rows)
                    {
                        ///
                        _LiquidationHdrOC.Add(new Liquidation_Hdr
                        {
                            _pdv_co = dr["IHV_CO"].ToString(),
                            _BDN_ID = Convert.ToInt32(dr["BDN_ID"]),
                            _nombre = dr["nombre"].ToString(),
                            _IHV_INVOICE_NO = dr["IHV_INVOICE_NO"].ToString(),
                            _PREFIJO = Convert.ToInt32(dr["PREFIJO"]),
                            _cantidad = Convert.ToInt32(dr["cantidad"]),
                            _RangoGuia = dr["rangoGuia"].ToString(),
                            _varCheck = 1
                        });
                    }
                }
                ///
                return _LiquidationHdrOC;
            }
            catch
            {
                return _LiquidationHdrOC;
            }
        }


        public ObservableCollection<Liquidation_Hdr> getChooiseEditGuia( int varSECUENCIALTRANSPORTISTA)
        {
            try
            {
                ///
                DataSet dsLiqHdr = Liquidation_Hdr.getChooiseEditGuiaDB( varSECUENCIALTRANSPORTISTA);
                ///
                if (dsLiqHdr != null)
                {
                    foreach (DataRow dr in dsLiqHdr.Tables[0].Rows)
                    {
                        ///
                        _LiquidationHdrOC.Add(new Liquidation_Hdr
                        {
                            _pdv_co = dr["IHV_CO"].ToString(),
                            _BDN_ID = Convert.ToInt32(dr["BDN_ID"]),
                            _nombre = dr["nombre"].ToString(),
                            // _LHV_LIQUIDATION_NO = dr["LHV_LIQUIDATION_NO"].ToString(),
                            _IHV_INVOICE_NO = dr["IHV_INVOICE_NO"].ToString(),
                            _PREFIJO = Convert.ToInt32(dr["PREFIJO"]),
                            // _NUMGUIA = Convert.ToInt32(dr["NUMGUIA"]),
                            _cantidad = Convert.ToInt32(dr["cantidad"]),
                            // _NUMCOMPLETO = dr["NUMCOMPLETO"].ToString(),
                            _RangoGuia = dr["rangoGuia"].ToString(),
                            _varCheck = 1
                        });
                    }
                }
                ///
                return _LiquidationHdrOC;
            }
            catch
            {
                return _LiquidationHdrOC;
            }
        }

        public object getNumGuiaBL()
        {
            return Basico.guiasecuencia();// Liquidation_Hdr.getNumGuiaDB();
        }


        public ObservableCollection<Liquidation_Hdr> getViewGuiaEdit(int idAgencia, DateTime fecInvoice)
        {
            try
            {
                ///
                DataSet dsLiqHdr = Liquidation_Hdr.getViewGuiaEditDB( idAgencia, fecInvoice);
                ///
                if (dsLiqHdr != null)
                {
                    foreach (DataRow dr in dsLiqHdr.Tables[0].Rows)
                    {
                        ///
                        _LiquidationHdrOC.Add(new Liquidation_Hdr
                        {
                            _pdv_co = dr["LHV_CO"].ToString(),
                            _Ag_transporte = dr["TRV_NAME"].ToString(),
                            _SECUENCIALTRANSPORTISTA = Convert.ToInt32(dr["SECUENCIALTRANSPORTISTA"]),
                            _nombre = dr["NOMBRE_TRANSPORTISTA"].ToString(),
                            _cantidad = Convert.ToInt32(dr["CANT_GUIAS"]),
                            _varCheck = 1
                        });
                    }
                }
                ///
                return _LiquidationHdrOC;
            }
            catch
            {
                return _LiquidationHdrOC;
            }
        }


        public ObservableCollection<Liquidation_Hdr> getViewGuiaforAdd(int idAgencia, String varFilterGuia, int varSECUENCIALTRANSPORTISTA)
        {
            try
            {
                ///
                DataSet dsLiqHdr = Liquidation_Hdr.getViewGuiaforAddDB( idAgencia, varFilterGuia, varSECUENCIALTRANSPORTISTA);
                ///
                if (dsLiqHdr != null)
                {
                    foreach (DataRow dr in dsLiqHdr.Tables[0].Rows)
                    {
                        ///
                        _LiquidationHdrOC.Add(new Liquidation_Hdr
                        {
                            _pdv_co = dr["IHV_CO"].ToString(),
                            _IHV_INVOICE_NO = dr["IHV_INVOICE_NO"].ToString(),
                            _PREFIJO = Convert.ToInt32(dr["PREFIJO"]),
                            _RangoGuia = dr["rangoGuia"].ToString(),
                            _SECUENCIALTRANSPORTISTA = Convert.ToInt32(dr["SECUENCIALTRANSPORTISTA"]),
                            _nombre = dr["NOMBRE_TRANSPORTISTA"].ToString()
                        });
                    }
                }
                ///
                return _LiquidationHdrOC;
            }
            catch
            {
                return _LiquidationHdrOC;
            }
        }



        /// <summary>
        /// Actualizar el numero de guia de una liquidacion
        /// </summary>
        /// <param name="lhv_co"></param>
        /// <param name="lhv_liquidation_no"></param>
        /// <param name="tgn_guide_id"></param>
        /// <returns></returns>
        public String updateGuideLiquidation(String lhv_liquidation_no,
            String tgn_guide_id)
        {
            return Liquidation_Hdr.updateGuideLiquidation( lhv_liquidation_no, tgn_guide_id);
        }
    }
}
