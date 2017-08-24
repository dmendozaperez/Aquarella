using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Variables;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.IO;
namespace Sistema_Aquarella
{
    public class Basico
    {

        public static DataSet getStatusByModule(string _module)
        {
            string sqlquery = "USP_Leer_EstadoModulo";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {

                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@est_mod_id", _module);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        
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

        private static Form form_principal;
        public static Form _form_principal
        {
            set { form_principal = value; }
            get { return form_principal; }
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
       



        public static void crear_archivo_properties()
        {
            string _archivo_pro = "config.properties";
            string _EFACT_2013 = "20101951872.pfx";
            string _ruta_properties = Path.GetDirectoryName(Application.ExecutablePath).ToString() + "/" + _archivo_pro;
            string _ruta_certificado= Path.GetDirectoryName(Application.ExecutablePath).ToString() + "/" + _EFACT_2013;
            string _log = _efact + "/log/";


            string _certificado = Path.GetDirectoryName(Application.ExecutablePath).ToString() + "/20101951872.pfx";
            try
            {
                //si no existe creamos la carpeta efact
                if (!(System.IO.Directory.Exists(_efact)))
                {
                    System.IO.Directory.CreateDirectory(_efact);
                }
                //si no existe creamos la carpeta log
                if (!(System.IO.Directory.Exists(_log)))
                {
                    System.IO.Directory.CreateDirectory(_log);
                }
                //si no existe creamos la carpeta salida
                if (!(System.IO.Directory.Exists(_salida)))
                {
                    System.IO.Directory.CreateDirectory(_salida);
                }

                //si no existe creamos la carpeta factura
                if (!(System.IO.Directory.Exists(_factura)))
                {
                    System.IO.Directory.CreateDirectory(_factura);
                }
                //si no existe creamos la carpeta credito
                if (!(System.IO.Directory.Exists(_credito)))
                {
                    System.IO.Directory.CreateDirectory(_credito);
                }
                //si no existe creamos la carpeta debito
                if (!(System.IO.Directory.Exists(_debito)))
                {
                    System.IO.Directory.CreateDirectory(_debito);
                }
                //si no existe creamos la carpeta boleta
                if (!(System.IO.Directory.Exists(_boleta)))
                {
                    System.IO.Directory.CreateDirectory(_boleta);
                }

                //si no esixte la carpeta de errores

                if (!(System.IO.Directory.Exists(_elec_efact_error)))
                {
                    System.IO.Directory.CreateDirectory(_elec_efact_error);
                }

                if (!(System.IO.Directory.Exists(_elec_efact_error_factura)))
                {
                    System.IO.Directory.CreateDirectory(_elec_efact_error_factura);
                }
                if (!(System.IO.Directory.Exists(_elec_efact_error_boleta)))
                {
                    System.IO.Directory.CreateDirectory(_elec_efact_error_boleta);
                }
                if (!(System.IO.Directory.Exists(_elec_efact_error_credito)))
                {
                    System.IO.Directory.CreateDirectory(_elec_efact_error_credito);
                }
                if (!(System.IO.Directory.Exists(_elec_efact_error_debito)))
                {
                    System.IO.Directory.CreateDirectory(_elec_efact_error_debito);
                }
                //***********************************************

                //crear archivo de certificado

                if (!(System.IO.File.Exists(_ruta_certificado)))
                {
                    NetworkShare.ConnectToShare(@"\\148.102.50.45\wwwroot\Aquarella\Certificado\20101951872.pfx", "Administrador", "Bata2013");
                    string origen = @"\\148.102.50.45\wwwroot\Aquarella\Certificado\20101951872.pfx";
                    string destino = @_ruta_certificado;                   
                    System.IO.File.Copy(origen, destino);
                    MessageBox.Show("Se copio el certificado de la facturacion electronica en el cliente", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (!(System.IO.File.Exists(_ruta_properties)))
                {                   

                    StreamWriter obj_properties = new StreamWriter(_ruta_properties);
                    obj_properties.Flush();
                    obj_properties.Close();
                    obj_properties.Dispose();

                    StreamWriter sw = new StreamWriter(_ruta_properties);
                    sw.WriteLine("#path to output xml files signed");
                    sw.WriteLine("outdir_log=" + _log.Replace("\\","/"));
                    sw.WriteLine("outdir_invoice=" + _factura.Replace("\\", "/"));
                    sw.WriteLine("outdir_creditnote=" + _credito.Replace("\\", "/"));
                    sw.WriteLine("outdir_debitnote=" + _debito.Replace("\\", "/"));
                    sw.WriteLine("outdir_boleta=" + _boleta.Replace("\\", "/"));
                    sw.WriteLine();
                    sw.WriteLine("#certificate");
                    sw.WriteLine("certificate_file=" + _certificado.Replace("\\", "/"));
                    sw.WriteLine("certificate_alias=5c21c030-2720-11e4-8c21-0800200c9a66");
                    sw.WriteLine("certificate_password=BATAPERU1108");
                    sw.WriteLine();
                    sw.WriteLine("write_xml_intern=true");
                    sw.WriteLine("write_xml_sunat=true");
                    sw.WriteLine();
                    sw.WriteLine("log_file=logger.log");

                    sw.Close();
                    //obj_properties.WriteLine(str);

                    //formato del archivo config.propertys
                    

                }

            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
