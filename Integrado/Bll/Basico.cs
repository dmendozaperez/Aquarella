using CapaDato.Bll.Ecommerce;
using CapaEntidad.Bll.Ecommerce;
using CapaEntidad.Bll.Util;
using Integrado.comercioxpress;
using Integrado.Prestashop;
using Integrado.Urbano;
using Integrado.Chazki;
using Integrado.Savar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media.Imaging;

namespace Integrado.Bll
{
    public class Basico
    {
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        #region<CONFIGURACION PARA LA FACTURACION CARVAJAL>

        public static string _cerificado
        {
            get { return "D:\\carvajal\\certificado"; }
        }
        public static string _xml
        {
            get { return "D:\\carvajal\\xml"; }
        }
        public static string _mapas
        {
            get { return "D:\\carvajal\\Mapas"; }
        }
        public static string _esquemas
        {
            get { return "D:\\carvajal\\Esquemas"; }
        }
        public static void copiar_archivo_config(ref string _error)
        {
            string _ruta_exe_local = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); ;//Path.GetDirectoryName(Application.ExecutablePath).ToString();
            try
            {

                string[] _array_certificado = Directory.GetFiles(_ruta_exe_local + "\\Certificado", "*.pfx");
                string[] _array_mapas = Directory.GetFiles(_ruta_exe_local + "\\mapas", "*.xml");
                string[] _array_esquema = Directory.GetFiles(_ruta_exe_local + "\\esquemas", "*.xml");

                //si no existe la carpeta certificado
                if (!Directory.Exists(@_cerificado))
                {
                    Directory.CreateDirectory(@_cerificado);
                    for (Int32 i = 0; i < _array_certificado.Length; ++i)
                    {
                        string _origen = _array_certificado[i].ToString();
                        FileInfo infofile = new FileInfo(_origen);
                        string _archivo_copiar = infofile.Name;
                        string _ruta_copiar_error = _cerificado + "\\" + _archivo_copiar;
                        File.Copy(@_origen, @_ruta_copiar_error, true);
                    }
                }
                else
                {
                    for (Int32 i = 0; i < _array_certificado.Length; ++i)
                    {
                        string _origen = _array_certificado[i].ToString();
                        FileInfo infofile = new FileInfo(_origen);
                        string _archivo_copiar = infofile.Name;
                        string _ruta_copiar_error = _cerificado + "\\" + _archivo_copiar;

                        if (!File.Exists(@_ruta_copiar_error))
                        {
                            File.Copy(@_origen, @_ruta_copiar_error, true);
                        }
                    }
                }
                //si no existe el xml mapa
                if (!Directory.Exists(@_mapas))
                {
                    Directory.CreateDirectory(@_mapas);
                    for (Int32 i = 0; i < _array_mapas.Length; ++i)
                    {
                        string _origen = _array_mapas[i].ToString();
                        FileInfo infofile = new FileInfo(_origen);
                        string _archivo_copiar = infofile.Name;
                        string _ruta_copiar_error = _mapas + "\\" + _archivo_copiar;
                        File.Copy(@_origen, @_ruta_copiar_error, true);
                    }
                }
                else
                {
                    for (Int32 i = 0; i < _array_mapas.Length; ++i)
                    {
                        string _origen = _array_mapas[i].ToString();
                        FileInfo infofile = new FileInfo(_origen);
                        string _archivo_copiar = infofile.Name;
                        string _ruta_copiar_error = _mapas + "\\" + _archivo_copiar;

                        if (!File.Exists(@_ruta_copiar_error))
                        {
                            File.Copy(@_origen, @_ruta_copiar_error, true);
                        }
                    }
                }
                //si no existe el xml esquema
                if (!Directory.Exists(@_esquemas))
                {
                    Directory.CreateDirectory(@_esquemas);
                    for (Int32 i = 0; i < _array_esquema.Length; ++i)
                    {
                        string _origen = _array_esquema[i].ToString();
                        FileInfo infofile = new FileInfo(_origen);
                        string _archivo_copiar = infofile.Name;
                        string _ruta_copiar_error = _esquemas + "\\" + _archivo_copiar;
                        File.Copy(@_origen, @_ruta_copiar_error, true);
                    }
                }
                else
                {
                    for (Int32 i = 0; i < _array_esquema.Length; ++i)
                    {
                        string _origen = _array_esquema[i].ToString();
                        FileInfo infofile = new FileInfo(_origen);
                        string _archivo_copiar = infofile.Name;
                        string _ruta_copiar_error = _esquemas + "\\" + _archivo_copiar;

                        if (!File.Exists(@_ruta_copiar_error))
                        {
                            File.Copy(@_origen, @_ruta_copiar_error, true);
                        }
                    }
                }

                //si no existe la carpeta xml
                if (!Directory.Exists(@_xml))
                {
                    Directory.CreateDirectory(@_xml);
                }


            }
            catch (Exception exc)
            {
                _error = exc.Message;
            }
        }
        public static void _enviar_webservice_xml()
        {
            string _ruta_copy = _xml + "\\copy";
            try
            {
                //Boolean valida_envio = true;
                if (Ent_Global._canal_venta == "AQ") if (Ent_Conexion._Base_Datos != "BdAquarella") return; ;
                if (Ent_Global._canal_venta == "BA") if (Ent_Conexion._Base_Datos != "BD_ECOMMERCE") return;

                //return;
                if (!Directory.Exists(@_ruta_copy))
                {
                    Directory.CreateDirectory(@_ruta_copy);
                }
                string[] _archivos_xml = Directory.GetFiles(@_xml, "*.xml");

                if (!Directory.Exists(@_ruta_copy))
                {
                    Directory.CreateDirectory(@_ruta_copy);
                }

                #region<REGION PARA ENVIAR EL XML AL SERVIDOR>


                if (_archivos_xml.Length > 0)
                {
                    for (Int32 a = 0; a < _archivos_xml.Length; ++a)
                    {
                        string _archivo = _archivos_xml[a].ToString();
                        string _nombrearchivo_xml = System.IO.Path.GetFileNameWithoutExtension(@_archivo);

                        byte[] _archivo_bytes = File.ReadAllBytes(@_archivo);
                        ServiceBata.ws_bataSoapClient _valor = new ServiceBata.ws_bataSoapClient();
                        string _error_service = _valor.ws_envio_xml(_archivo_bytes, _nombrearchivo_xml);

                        if (_error_service == "1")
                        {
                            string _archivo_copy = _ruta_copy + "\\" + _nombrearchivo_xml + ".xml";
                            File.Copy(@_archivo, @_archivo_copy, true);
                            File.Delete(@_archivo);
                        }
                        else
                        {
                            break;
                        }

                    }
                }
                #endregion
            }
            catch
            {

            }
        }
        #endregion

        public static void cambio_img(System.Windows.Controls.Image imglogo)
        {
            if (Ent_Global._canal_venta == "AQ")
            {
                var uriSource = new Uri("/Integrado;component/Design/Images/aq_lineal.jpg", UriKind.Relative);
                imglogo.Source = new BitmapImage(uriSource);

            }
            else
            {
                var uriSource = new Uri("/Integrado;component/Design/Images/BataLogo.png", UriKind.Relative);
                imglogo.Source = new BitmapImage(uriSource);

                //#region<ENVIO DATA URBANO>
                //EnviaPedido envia = new EnviaPedido();
                //envia.sendUrbano();
                ////envia.send();
                //#endregion

            }
        }

        #region<PROCESOS DE E-CCOMERCE>
        public static void act_presta_urbano(string ven_id, ref string error, ref string cod_urbano)
        {
            //string codigo = ven_id.Substring(0,4)+"-"+ven_id.Substring(4,8);

            Dat_PrestaShop action_presta = null;
            Dat_Urbano data_urbano = null;

            Dat_Cexpress data_Cexpress = null;

            error = "";
            try
            {
                string guia_presta = ""; string guia_courier = ""; string name_carrier = "";
                action_presta = new Dat_PrestaShop();
                data_urbano = new Dat_Urbano();
                //action_presta.get_guia_presta_urba(ven_id, ref guia_presta, ref guia_urb, ref name_carrier);
                action_presta.get_carrier(ven_id, ref guia_presta, ref name_carrier);
                string track_chazki;

                if (guia_presta.Trim().Length > 0)
                {
                    UpdaEstado updateestado = new UpdaEstado();
                    //comentado
                    Boolean valida = (Ent_Global._err_con_mysql) ? true : updateestado.ActualizarReference(guia_presta);/*si la variable global es 1 quiere decir que el mysql esta sin conexion*/
                    //Boolean valida = true;

                    if (valida)
                    {
                        data_Cexpress = new Dat_Cexpress();
                        //action_presta.updestafac_prestashop(guia_presta);
                        EnviaPedidoCxpress envia2 = new EnviaPedidoCxpress();
                        string nroserv = "";

                        /*enviamos urbano la guia*/
                        EnviaPedido envia = new EnviaPedido();

                        if (name_carrier == "Comercio Xpress")
                        {
                            Ent_Cexpress ent_Cexpress = envia2.sendCexpress(ven_id, ref nroserv);
                        }

                        //intentando 3 veces
                        for (Int32 i = 0; i < 3; ++i)
                        {
                            /*Nuevo*/
                            if (name_carrier == "Comercio Xpress")
                            {
                                //Ent_Cexpress ent_Cexpress = envia2.sendCexpress(ven_id, ref nroserv);
                                action_presta.updestafac_prestashop(guia_presta);
                                data_Cexpress.update_guia(guia_presta, nroserv);
                                guia_courier = nroserv;
                                break;

                            }
                            //Chazki - Envíos Express
                            else if (name_carrier == "Chazki - Envíos Express")
                            {
                                EnviarChazki objChazki = new EnviarChazki();

                                string nrodelivery_chazki = objChazki.Envia_Courier_chazki(ven_id);
                                if (nrodelivery_chazki != "")
                                {
                                    action_presta.updestafac_prestashop(guia_presta);
                                    data_Cexpress.update_guia(guia_presta, ven_id);
                                    guia_courier = ven_id;
                                    break;
                                }
                            }
                            else if (name_carrier == "Envío a Domicilio - Savar")
                            {
                                EnviarSavar objSavar = new EnviarSavar();

                                string nrodelivery_savar = objSavar.Envia_Courier_Savar(ven_id);
                                if (nrodelivery_savar != "")
                                {
                                    action_presta.updestafac_prestashop(guia_presta);
                                    data_Cexpress.update_guia(guia_presta, nrodelivery_savar);
                                    guia_courier = nrodelivery_savar;
                                    break;
                                }

                            }
                            else
                            {
                                Ent_Urbano ent_urbano = envia.sendUrbano(ven_id);
                                if (ent_urbano.error == "1")
                                {
                                    if (ent_urbano.guia.Trim().Length > 0)
                                    {
                                        action_presta.updestafac_prestashop(guia_presta);
                                        data_urbano.update_guia(guia_presta, ent_urbano.guia);
                                        guia_courier = ent_urbano.guia;
                                        break;
                                    }
                                }
                            }

                        }
                        //guia_urb=
                        //action_presta.get_guia_presta_urba(ven_id, ref guia_presta, ref guia_urb);

                        ActTracking enviaguia_presta = new ActTracking();
                        string[] valida_prest;

                        //if (name_carrier == "Chazki - Envíos Express") //para chazki el codigo de seguimiento es el mismo nro de boleta
                        //{
                        //    track_chazki = ven_id.Substring(0, 4) + "-" + ven_id.Substring(4, 8);

                        //    valida_prest = enviaguia_presta.ActualizaTrackin(guia_presta, track_chazki);
                        //}
                        //else
                        //{
                            valida_prest = enviaguia_presta.ActualizaTrackin(guia_presta, guia_courier);
                        //}


                        /*el valor 1 quiere decir que actualizo prestashop*/
                        if (valida_prest[0] == "1" && guia_courier.ToString() != "")
                        {
                            data_urbano.updprestashopGuia(guia_presta, guia_courier);
                        }
                        cod_urbano = guia_courier;
                        /************************/
                    }
                }


            }
            catch (Exception exc)
            {
                cod_urbano = "";
                error = exc.Message;
            }
        }

    }
}
#endregion