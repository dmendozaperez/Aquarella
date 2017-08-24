using CapaDato.Bll.Venta;
using CapaEntidad.Bll.Util;
using CapaEntidad.Bll.Venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epson_Ticket
{
    public class ImprimirCierre
    {
        #region<REGION DE CIERRE DE VENTA>

        public static string Generar_Impresion_Cierre(DateTime fecha_c)
        {
            try
            {
                Dat_Cierre_Venta dat_cierre = new Dat_Cierre_Venta();
                Ent_Cierre_Venta get_cierre=dat_cierre.leer_data_cierre(fecha_c);
                if (get_cierre!=null)
                {
                    string _almacen = "ALMACEN           :" + Ent_Global._pvt_nombre.ToString().PadLeft(21);
                    string _maquina = "SERIE DE IMPRESORA:" + Ent_Global._serie_imp.PadLeft(21);
                    string _fecha = "FECHA             :" + get_cierre.fecha_venta.ToString("dd-MM-yyyy").PadLeft(21);
                    string _total_venta="TOTAL DE VENTA    :" + string.Format("{0:C2}", get_cierre.total_venta).PadLeft(21);
                    string _fectivoletr = "EFECTIVO           ";
                    string _efectivo="EFECTIVO          :" + string.Format("{0:C2}", get_cierre.efectivo).PadLeft(21);
                    string _vuelto="VUELTO            :" + string.Format("{0:C2}", get_cierre.vuelto).PadLeft(21);
                    string _neto=  "TOTAL EFECTIVO    :" + string.Format("{0:C2}", get_cierre.total_efectivo).PadLeft(21);
                    string _total_tarjeta="TOTAL TARJETA     :" + string.Format("{0:C2}", get_cierre.total_tarjeta).PadLeft(21);
                    string _netoefe= "         NETO     :" + string.Format("{0:C2}", get_cierre.total_efectivo).PadLeft(21);
                    string _fondocaja= "Fondo de Caja  (+):" + string.Format("{0:C2}", get_cierre.inicio_caja).PadLeft(21);
                         string _tcaja="TOTAL DE CAJA     :" + string.Format("{0:C2}", get_cierre.total_caja).PadLeft(21);
                    CrearTicket tk = new CrearTicket();
                    tk.TextoCentro("CIERRE TOTAL DEL DIA");
                    tk.lineasIgual();
                    tk.TextoIzquierda(_almacen);
                    tk.TextoIzquierda(_maquina);
                    tk.TextoIzquierda(_fecha);
                    tk.lineasGuio();
                    tk.TextoCentro("RESUMEN DE VENTAS");
                    tk.TextoIzquierda(_total_venta);
                    tk.lineasGuio();
                    tk.TextoCentro("VENTAS POR TIPO DE PAGO");
                    //tk.TextoIzquierda(_fectivoletr);
                    tk.TextoIzquierda(_efectivo);
                    tk.TextoIzquierda(_vuelto);
                    tk.TextoDerecha("========");
                    tk.TextoIzquierda(_neto);
                    tk.TextoIzquierda("");
                    tk.TextoIzquierda(_total_tarjeta);
                    tk.lineasGuio();
                    tk.TextoCentro("ARQUEO DE CAJA");
                    tk.TextoIzquierda(_fectivoletr);
                    tk.TextoIzquierda(_netoefe);
                    tk.TextoIzquierda(_fondocaja);
                    tk.TextoDerecha("========");
                    tk.TextoIzquierda(_tcaja);
                    tk.lineasGuio();

                    if (get_cierre.banco_des.Length>0)
                    {
                        //string _fondocaja = "Fondo de Caja  (+):" + string.Format("{0:C2}", get_cierre.inicio_caja).PadLeft(21);
                        string _banco = "BANCO             :" + get_cierre.banco_des.PadLeft(21);
                        string _operacion = "NRO. OPERACION    :" + get_cierre.nro_operacion.PadLeft(21);
                        string _monto_opera = "MONTO OPERACION   :" + string.Format("{0:C2}", get_cierre.monto_opera).PadLeft(21);
                        tk.TextoCentro("BANCO DE DEPOSITO");
                        tk.TextoIzquierda(_banco);
                        tk.TextoIzquierda(_operacion);
                        tk.TextoIzquierda(_monto_opera);
                        tk.lineasGuio();
                    }

                    tk.CortaTicket();
                    tk.ImprimirTicket(Ent_Global._impresora);
                   

                    if (!CrearTicket._esta_imp)
                    {
                        return null;
                    }
                }
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
