using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Variables;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
namespace Aquarella.bll
{
    public class Basico
    {
        
        public static DataTable leertrasnportador()
        {
            string sqlquery = "USP_Leer_Transportadora";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            return dt;
        }
        public static string guiasecuencia()
        {
            string sqlquery="USP_GuiaRemision_Secuencia";
            string nguia = "";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {

                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@numguia", SqlDbType.VarChar, 20);
                cmd.Parameters["@numguia"].Direction = ParameterDirection.Output;                   
                cmd.ExecuteNonQuery();                                
                nguia = Convert.ToString(cmd.Parameters["@numguia"].Value);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                nguia = "";
            }
            return nguia;
        }

      
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

        #region<REGION PARA LA CONFIGURACION DE EFACT>

        //variables globales
        private static string _efact = Path.GetDirectoryName(Application.ExecutablePath).ToString() + "/Efact";
        private static string _efact_error = _efact + "/error";
        private static string _efact_error_factura=_efact_error + "/factura/";
        private static string _efact_error_boleta=_efact_error + "/boleta/";
        private static string _efact_error_credito=_efact_error + "/credito/";
        private static string _efact_error_debito=_efact_error + "/debito/";
        private static string _salida = _efact.Replace("\\", "/") + "/salida";
        private static string _factura = _salida + "/factura/";
        private static string _credito = _salida + "/credito/";
        private static string _debito = _salida + "/debito/";
        private static string _boleta = _salida + "/boleta/";



        public static string _elec_efact_error
        {
            get { return _efact_error; }
        }
        public static string _elec_efact_error_factura
        {
            get { return _efact_error_factura; }
        }
        public static string _elec_efact_error_boleta
        {
            get { return _efact_error_boleta; }
        }
        public static string _elec_efact_error_credito
        {
            get { return _efact_error_credito; }
        }
        public static string _elec_efact_error_debito
        {
            get { return _efact_error_debito; }
        }

        public static string _elec_factura
        {
            get { return _factura ; }
        }
        public static string _elec_credito
        {
            get { return _credito; }
        }
        public static string _elec_debito
        {
            get { return _debito; }
        }
        public static string _elec_boleta
        {
            get { return _boleta; }
        }
       
    #endregion

        #region<CONFIGURACION PARA LA FACTURACION CARVAJAL>

         public static string _cerificado
        {
            get { return "C:\\carvajal\\certificado"; }
        }
         public static string _xml
         {
             get { return "C:\\carvajal\\xml"; }
         }
        public static string _mapas
        {
            get { return "C:\\carvajal\\Mapas"; }
        }
        public static string _esquemas
        {
            get {return "C:\\carvajal\\Esquemas";}
        }

        public static void _enviar_webservice_xml()
        {
            string _ruta_copy = _xml + "\\copy" ;
            try
            {
                if (!Directory.Exists(@_ruta_copy))
                {
                    Directory.CreateDirectory(@_ruta_copy);
                }
                string[] _archivos_xml = Directory.GetFiles(@_xml, "*.xml");

                if (!Directory.Exists(@_ruta_copy))
                {
                    Directory.CreateDirectory(@_ruta_copy);
                }

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






                    }
                }
            }
            catch
            {

            }
        }

        public static void copiar_archivo_config(ref string _error)
        {
            string _ruta_exe_local = Path.GetDirectoryName(Application.ExecutablePath).ToString();
            try
            {

                string[] _array_certificado =Directory.GetFiles(_ruta_exe_local + "\\Certificado","*.pfx");
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
            catch(Exception exc)
            {
                _error = exc.Message;
            }
        }

        #endregion

    }
}
