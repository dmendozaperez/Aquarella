using CapaEntidad.Bll.Util;
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
                return;
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
    }
}
