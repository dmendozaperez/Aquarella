using CapaDato.Bll.Venta;
using CapaEntidad.Bll.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epson_Ticket
{
    public class Imprimir_Doc
    {
        #region<FORMATO DE IMPRESION EPSON>
        public static string SetBold = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'b', (byte)'C' });
        private static string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);
            return retVal;
        }
        public static string Generar_Impresion(string _tipo, string _numero_doc)
        {

            try
            {
                #region<IMPRESION DE FACTURAS Y BOLETAS>
                if (_tipo == "F" || _tipo == "B")
                {
                    DataSet ds = Dat_Venta.leer_venta_tk(_numero_doc);
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                #region<VARIABLES DE EMPRESA TICKETS>
                                string _marca_emp = dt.Rows[0]["Alm_Descripcion"].ToString();
                                string _direccion_emp = dt.Rows[0]["Alm_Direccion"].ToString();
                                string _Telefono_emp = dt.Rows[0]["Alm_Telefono"].ToString();
                                string _razon_social_emp = dt.Rows[0]["Emp_Razon"].ToString();
                                string _ruc_emp = dt.Rows[0]["Emp_Ruc"].ToString();
                                String _nota = dt.Rows[0]["nota"].ToString();
                                string _impresora= dt.Rows[0]["impresora"].ToString();
                                #endregion
                                #region<VARIABLES DE IMPRESORA>
                                string _autoriacion_imp = "Autorizacion : " + dt.Rows[0]["Emp_Autorizacion"].ToString();
                                string _serie_imp = "Impresora : " + dt.Rows[0]["serie_impresora"].ToString();

                                Decimal _monto_efe =Convert.ToDecimal(dt.Rows[0]["montoefe"]);
                                Decimal _monto_tar = Convert.ToDecimal(dt.Rows[0]["montotar"]);
                                Decimal _monto_vue = Convert.ToDecimal(dt.Rows[0]["montovuel"]);

                                #endregion
                                #region<TIPO DE DOCUMENTO>
                                string _numero = dt.Rows[0]["nrodoc"].ToString();
                                string _tipo_numero = ((_numero.Substring(0, 1).ToString() == "F") ? "FACTURA: " : "BOLETA: ") + _numero;
                                DateTime _fecha_doc = Convert.ToDateTime(dt.Rows[0]["Ven_Fecha"]);
                                string _fecha_doc_text = "Fecha : " + _fecha_doc.ToString("dd/MM/yyyy") + " , " + _fecha_doc.ToString("hh:mm tt");
                                decimal _igv = Convert.ToDecimal(dt.Rows[0]["Ven_Igv_Porc"].ToString());
                                decimal _percepcionp = Convert.ToDecimal(dt.Rows[0]["Ven_PercepcionP"].ToString());
                                decimal _percepcionm = Convert.ToDecimal(dt.Rows[0]["Ven_PercepcionM"].ToString());
                                string _cod_hash = dt.Rows[0]["cod_hash"].ToString();
                                String _estadook = dt.Rows[0]["EstadoOk"].ToString();
                                Decimal _monto_nc = Convert.ToDecimal(dt.Rows[0]["monto_nc"].ToString());
                                string _referencia_nc = dt.Rows[0]["referencia_nc"].ToString();
                                #endregion
                                #region<VARIABLES DE CLIENTES>
                                string _cliente_nom = dt.Rows[0]["nombres"].ToString();
                                string _cliente_dni_ruc = dt.Rows[0]["Bas_Documento"].ToString();
                                #endregion
                                #region<FORMATO DE IMPRESION FACTURA O BOLETA>
                                CrearTicket tk = new CrearTicket();
                                tk.TextoCentro(_marca_emp);
                                tk.TextoCentro(_direccion_emp);
                                tk.TextoCentro("Telefono " + _Telefono_emp);
                                tk.TextoCentro(_razon_social_emp);
                                tk.TextoCentro(_ruc_emp);
                                tk.TextoIzquierda(_autoriacion_imp);
                                tk.TextoIzquierda(_serie_imp);
                                tk.TextoIzquierda(_tipo_numero);
                                tk.TextoIzquierda("");
                                tk.TextoCentro("DETALLE DE COMPRA");
                                tk.TextoIzquierda("");
                                tk.lineasGuio();
                                tk.TextoIzquierda("Cliente : ");
                                tk.TextoIzquierda(_cliente_nom);
                                tk.TextoIzquierda(((_numero.Substring(0, 1).ToString() == "F") ? "R.U.C : " : "DNI :  ") + _cliente_dni_ruc);
                                tk.TextoIzquierda(_fecha_doc_text);
                                tk.TextoIzquierda("");
                                tk.EncabezadoVenta("CODIGO  | DESCR.         | CNT | PRECIO ");
                                tk.lineasIgual();
                                //items del tickets
                                decimal dSubtotal = 0;
                                decimal descuento = 0;
                                for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                {
                                    string _iarticulo = dt.Rows[i]["Art_Id"].ToString();
                                    string _articulonombre = dt.Rows[i]["Art_Descripcion"].ToString();
                                    string _des_largo = TruncateAt(dt.Rows[i]["Art_Descripcion"].ToString().PadRight(40), 40);
                                    string _talla = dt.Rows[i]["Ven_Det_TalId"].ToString();
                                    decimal _cantidad = Convert.ToInt32(dt.Rows[i]["Ven_Det_Cantidad"]);
                                    Decimal _articulo_total = Convert.ToDecimal(dt.Rows[i]["articulo_total"].ToString());
                                    decimal _comision = Convert.ToDecimal(dt.Rows[i]["Ven_Det_ComisionM"].ToString());
                                    string _codigo_descripcion = TruncateAt(_iarticulo.PadRight(10), 10) +
                                    TruncateAt(_articulonombre.PadRight(9), 10) + TruncateAt(_talla.PadLeft(4), 4) + TruncateAt(_cantidad.ToString("#0").PadLeft(5), 5) + TruncateAt(_articulo_total.ToString("#0.00").PadLeft(9), 9);
                                    tk.AgregarItems(_codigo_descripcion);
                                    tk.AgregarItems(_des_largo);
                                    dSubtotal += _articulo_total;
                                    descuento += _comision;
                                }

                                double mtoigv = Math.Round((Double.Parse(dSubtotal.ToString()) - Convert.ToDouble(descuento)) * double.Parse(_igv.ToString()), 2, MidpointRounding.AwayFromZero);
                                Int32 porcigv = Convert.ToInt32((_igv * 100));
                                decimal totalpagar = ((dSubtotal - descuento) + Convert.ToDecimal(mtoigv)) + _percepcionm;

                                tk.lineasGuio();
                                tk.AgregarFooter("     " + TruncateAt("SUB-TOTAL".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(dSubtotal.ToString("#0.00").PadLeft(14), 14));
                                tk.AgregarFooter("     " + TruncateAt("DESCUENTO(-)".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(descuento.ToString("#0.00").PadLeft(14), 14));
                                tk.AgregarFooter("     " + TruncateAt("IGV " + porcigv + "%".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(mtoigv.ToString("#0.00").PadLeft(14), 14));
                                tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));

                                if (_estadook != "2")
                                {
                                    tk.AgregarFooter("     " + TruncateAt("TOTAL".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(((dSubtotal - descuento) + Convert.ToDecimal(mtoigv)).ToString("#0.00").PadLeft(14), 14));
                                }

                                if (_estadook == "1")
                                {
                                    tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                    tk.AgregarFooter("     " + TruncateAt("PERCEPCION " + _percepcionp + "%".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_percepcionm.ToString("#0.00").PadLeft(14), 14));
                                    tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                    tk.AgregarFooter("     " + TruncateAt("TOTAL A PAGAR".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(totalpagar.ToString("#0.00").PadLeft(14), 14));
                                }

                                if (_estadook == "2")
                                {
                                    Decimal _var_sb = Convert.ToDecimal(((dSubtotal - descuento) + Convert.ToDecimal(mtoigv)));

                                    if (_var_sb < _monto_nc)
                                    {
                                        _monto_nc = _var_sb;
                                    }
                                    decimal _total_nc = _var_sb - _monto_nc;

                                    decimal _total_pagar_nc = (_var_sb - _monto_nc) + Convert.ToDecimal(_percepcionm);

                                    tk.AgregarFooter("     " + TruncateAt("DESC NC  (-)".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_monto_nc.ToString("#0.00").PadLeft(14), 14));
                                    tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                    tk.AgregarFooter("     " + TruncateAt("TOTAL".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_total_nc.ToString("#0.00").PadLeft(14), 14));
                                    if (_total_nc != 0)
                                    {
                                        tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                        tk.AgregarFooter("     " + TruncateAt("PERCEPCION " + _percepcionp + "%".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_percepcionm.ToString("#0.00").PadLeft(14), 14));
                                        tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                        tk.AgregarFooter("     " + TruncateAt("TOTAL A PAGAR".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_total_pagar_nc.ToString("#0.00").PadLeft(14), 14));
                                    }
                                    tk.lineasGuio();
                                    tk.TextoIzquierda(_referencia_nc);
                                }

                                #region<REGION DE VENTAS DIRECTA CON PAGO>
                                if (Ent_Global._pvt_almaid!="11")
                                {
                                    tk.lineasGuio();
                                    tk.AgregarFooter(TruncateAt("EFECTIVO".ToString().PadRight(21), 21) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_monto_efe.ToString("#0.00").PadLeft(14), 14));
                                    /*VERIFICAR SI SE PAGA CON TARJETA*/
                                    if (ds.Tables.Count>1)
                                    {
                                        DataTable dttarjeta = ds.Tables[1];
                                        if (dttarjeta!=null)
                                        { 
                                            if (dttarjeta.Rows.Count>0)
                                            { 
                                                tk.AgregarFooter(TruncateAt("TARJETA".ToString().PadRight(16), 16));
                                                for(Int32 i=0;i<dttarjeta.Rows.Count;++i)
                                                {
                                                    string nom_tarjeta = dttarjeta.Rows[i]["bin_des"].ToString();
                                                    string num_tarjeta= dttarjeta.Rows[i]["num_tar"].ToString();
                                                    Decimal monto_tarjeta =Convert.ToDecimal(dttarjeta.Rows[i]["monto_tar"]);
                                                    tk.AgregarFooter(TruncateAt(nom_tarjeta.ToString().PadRight(22), 22) + " " +  TruncateAt(_monto_efe.ToString(num_tarjeta.ToString()).PadLeft(16), 16));
                                                    tk.AgregarFooter("                     " +  TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(monto_tarjeta.ToString("#0.00").PadLeft(14), 14));
                                                }
                                            }
                                        }
                                    }
                                    tk.AgregarFooter(TruncateAt("VUELTO".ToString().PadRight(21), 21) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_monto_vue.ToString("#0.00").PadLeft(14), 14));
                                }
                                #endregion

                                tk.lineasGuio();
                                tk.TextoCentro(_cod_hash);
                                tk.lineasGuio();
                                tk.TextoIzquierda(_nota);
                                tk.lineasGuio();
                                tk.TextoCentro("*** GRACIAS POR SU COMPRA ***");
                                tk.CortaTicket();
                                tk.ImprimirTicket(_impresora);      
                                if (!CrearTicket._esta_imp)
                                {
                                    return null;
                                }
                                #endregion
                            }
                        }

                    }
                }
                #endregion

                #region<IMPRESION DE NOTA DE CREDITO>
                if (_tipo == "N")
                {
                    DataSet ds = Dat_NotaCredito.leer_NC_tk(_numero_doc);
                    if (ds != null)
                    {
                        DataTable dt = ds.Tables[0];

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                #region<VARIABLES DE EMPRESA TICKETS>
                                string _marca_emp = dt.Rows[0]["Alm_Descripcion"].ToString();
                                string _direccion_emp = dt.Rows[0]["Alm_Direccion"].ToString();
                                string _Telefono_emp = dt.Rows[0]["Alm_Telefono"].ToString();
                                string _razon_social_emp = dt.Rows[0]["Emp_Razon"].ToString();
                                string _ruc_emp = dt.Rows[0]["Emp_Ruc"].ToString();
                                String _nota = dt.Rows[0]["nota"].ToString();
                                string _impresora = dt.Rows[0]["impresora"].ToString();
                                #endregion
                                #region<VARIABLES DE IMPRESORA>
                                string _autoriacion_imp = "Autorizacion : " + dt.Rows[0]["Emp_Autorizacion"].ToString();
                                string _serie_imp = "Impresora : " + dt.Rows[0]["serie_impresora"].ToString();
                                #endregion
                                #region<TIPO DE DOCUMENTO>
                                string _numero = dt.Rows[0]["nrodoc"].ToString();
                                string _tipo_numero = "NOTA DE CREDITO: " + _numero;
                                DateTime _fecha_doc = Convert.ToDateTime(dt.Rows[0]["Not_Fecha"]);
                                string _fecha_doc_text = "Fecha : " + _fecha_doc.ToString("dd/MM/yyyy") + " , " + _fecha_doc.ToString("hh:mm tt");
                                decimal _igv = Convert.ToDecimal(dt.Rows[0]["Ven_Igv_Porc"].ToString());
                                //decimal _percepcionp = Convert.ToDecimal(dt.Rows[0]["Ven_PercepcionP"].ToString());
                                //decimal _percepcionm = Convert.ToDecimal(dt.Rows[0]["Ven_PercepcionM"].ToString());
                                string _cod_hash = dt.Rows[0]["cod_hash"].ToString();
                                String _estadook = dt.Rows[0]["EstadoOk"].ToString();
                                //Decimal _monto_nc = Convert.ToDecimal(dt.Rows[0]["monto_nc"].ToString());
                                string _referencia_nc = dt.Rows[0]["referencia"].ToString();
                                #endregion
                                #region<VARIABLES DE CLIENTES>
                                string _cliente_nom = dt.Rows[0]["nombres"].ToString();
                                string _cliente_dni_ruc = dt.Rows[0]["Bas_Documento"].ToString();
                                #endregion
                                #region<FORMATO DE IMPRESION FACTURA O BOLETA>
                                CrearTicket tk = new CrearTicket();
                                tk.TextoCentro(_marca_emp);
                                tk.TextoCentro(_direccion_emp);
                                tk.TextoCentro("Telefono " + _Telefono_emp);
                                tk.TextoCentro(_razon_social_emp);
                                tk.TextoCentro(_ruc_emp);
                                tk.TextoIzquierda(_autoriacion_imp);
                                tk.TextoIzquierda(_serie_imp);
                                tk.TextoIzquierda(_tipo_numero);
                                tk.TextoIzquierda("");
                                tk.TextoCentro("DETALLE DE COMPRA");
                                tk.TextoIzquierda("");
                                tk.lineasGuio();
                                tk.TextoIzquierda("Cliente : ");
                                tk.TextoIzquierda(_cliente_nom);
                                tk.TextoIzquierda(((_numero.Substring(0, 1).ToString() == "F") ? "R.U.C : " : "DNI :  ") + _cliente_dni_ruc);
                                tk.TextoIzquierda(_fecha_doc_text);
                                tk.TextoIzquierda("");
                                tk.EncabezadoVenta("CODIGO  | DESCR.         | CNT | PRECIO ");
                                tk.lineasIgual();
                                //items del tickets
                                decimal dSubtotal = 0;
                                decimal descuento = 0;
                                for (Int32 i = 0; i < dt.Rows.Count; ++i)
                                {
                                    string _iarticulo = dt.Rows[i]["Art_Id"].ToString();
                                    string _articulonombre = dt.Rows[i]["Art_Descripcion"].ToString();
                                    string _des_largo = TruncateAt(dt.Rows[i]["Art_Descripcion"].ToString().PadRight(40), 40);
                                    string _talla = dt.Rows[i]["Not_Det_TalId"].ToString();
                                    decimal _cantidad = Convert.ToInt32(dt.Rows[i]["Not_Det_Cantidad"]);
                                    Decimal _articulo_total = Convert.ToDecimal(dt.Rows[i]["articulo_total"].ToString());
                                    decimal _comision = Convert.ToDecimal(dt.Rows[i]["Not_Det_ComisionM"].ToString());
                                    string _codigo_descripcion = TruncateAt(_iarticulo.PadRight(10), 10) +
                                    TruncateAt(_articulonombre.PadRight(9), 10) + TruncateAt(_talla.PadLeft(4), 4) + TruncateAt(_cantidad.ToString("#0").PadLeft(5), 5) + TruncateAt(_articulo_total.ToString("#0.00").PadLeft(9), 9);
                                    tk.AgregarItems(_codigo_descripcion);
                                    tk.AgregarItems(_des_largo);
                                    dSubtotal += _articulo_total;
                                    descuento += _comision;
                                }

                                double mtoigv = Math.Round((Double.Parse(dSubtotal.ToString()) - Convert.ToDouble(descuento)) * double.Parse(_igv.ToString()), 2, MidpointRounding.AwayFromZero);
                                Int32 porcigv = Convert.ToInt32((_igv * 100));
                                decimal totalpagar = ((dSubtotal - descuento) + Convert.ToDecimal(mtoigv));

                                tk.lineasGuio();
                                tk.AgregarFooter("     " + TruncateAt("SUB-TOTAL".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(dSubtotal.ToString("#0.00").PadLeft(14), 14));
                                tk.AgregarFooter("     " + TruncateAt("DESCUENTO(-)".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(descuento.ToString("#0.00").PadLeft(14), 14));
                                tk.AgregarFooter("     " + TruncateAt("IGV " + porcigv + "%".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(mtoigv.ToString("#0.00").PadLeft(14), 14));
                                tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));

                                if (_estadook != "2")
                                {
                                    tk.AgregarFooter("     " + TruncateAt("TOTAL".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(((dSubtotal - descuento) + Convert.ToDecimal(mtoigv)).ToString("#0.00").PadLeft(14), 14));
                                }

                                if (_estadook == "1")
                                {
                                    tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                    //tk.AgregarFooter("     " + TruncateAt("PERCEPCION " + _percepcionp + "%".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_percepcionm.ToString("#0.00").PadLeft(14), 14));
                                    tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                    tk.AgregarFooter("     " + TruncateAt("TOTAL A PAGAR".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(totalpagar.ToString("#0.00").PadLeft(14), 14));
                                }

                                if (_estadook == "2")
                                {
                                    Decimal _var_sb = Convert.ToDecimal(((dSubtotal - descuento) + Convert.ToDecimal(mtoigv)));

                                    //if (_var_sb < _monto_nc)
                                    //{
                                    //    _monto_nc = _var_sb;
                                    //}
                                    //decimal _total_nc = _var_sb - _monto_nc;

                                    //decimal _total_pagar_nc = (_var_sb - _monto_nc) ;

                                    //tk.AgregarFooter("     " + TruncateAt("DESC NC  (-)".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_monto_nc.ToString("#0.00").PadLeft(14), 14));
                                    //tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                    //tk.AgregarFooter("     " + TruncateAt("TOTAL".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_total_nc.ToString("#0.00").PadLeft(14), 14));
                                    //if (_total_nc != 0)
                                    //{
                                    //    tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                    //    //tk.AgregarFooter("     " + TruncateAt("PERCEPCION " + _percepcionp + "%".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_percepcionm.ToString("#0.00").PadLeft(14), 14));
                                    //    tk.AgregarFooter("     " + TruncateAt("------------".ToString().PadRight(14), 14));
                                    //    tk.AgregarFooter("     " + TruncateAt("TOTAL A PAGAR".ToString().PadRight(16), 16) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(_total_pagar_nc.ToString("#0.00").PadLeft(14), 14));
                                    //}
                                    //tk.lineasGuio();
                                    //tk.TextoIzquierda(_referencia_nc);
                                }

                                tk.lineasGuio();
                                tk.TextoIzquierda("FIRMA:");
                                tk.AgregarFooter(TruncateAt("EFECTIVO:".ToString().PadRight(21), 21) + TruncateAt("S/".ToString().PadRight(3), 3) + TruncateAt(((dSubtotal - descuento) + Convert.ToDecimal(mtoigv)).ToString("#0.00").PadLeft(14), 14));
                                tk.lineasGuio();
                                tk.TextoIzquierda("REFERENCIA DE N/C: " + _referencia_nc);
                                tk.lineasGuio();
                                tk.TextoCentro(_cod_hash);
                                tk.lineasGuio();
                                tk.TextoIzquierda(_nota);
                                tk.lineasGuio();
                                tk.TextoCentro("*** GRACIAS POR SU COMPRA ***");
                                tk.CortaTicket();
                                tk.ImprimirTicket(_impresora);
                                if (!CrearTicket._esta_imp)
                                {
                                    return null;
                                }
                                #endregion
                            }
                        }

                    }
                }
                #endregion
                return "ok";
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
