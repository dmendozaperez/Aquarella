using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using System.Data;
using System.Data.SqlClient;
using Carvajal.FEPE.PreSC.Core;
using System.Configuration;
using System.Web.UI;
using System.IO;
namespace www.aquarella.com.pe.bll
{
    public class F_Electronico
    {


        #region<METODO PARA ENVIAR WEB SERVICE A CARVAJAL>

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
            get { return "C:\\carvajal\\Esquemas"; }
        }

        public static void _enviar_webservice_xml()
        {
            string _ruta_copy = _xml + "\\copy";
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
                        string _error_service = _valor.ws_envio_xml_desarrollo(_archivo_bytes, _nombrearchivo_xml);

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
        #endregion

        #region<METODO PARA GENERAR HASH>
        public static void ejecutar_factura_electronica(string _tipo_doc, string _num_doc, ref string cod_hash, ref string _error)
        {
            string _formato_doc = "";

            try
            {
                
                //modificar web.config              

                _formato_doc = _leer_formato_electronico(_tipo_doc, _num_doc, ref _error);
                GeneratorCdp generatorCdp = new GeneratorCdp();

                if (_tipo_doc == "B" || _tipo_doc == "F")
                {
                    cod_hash = generatorCdp.GetHashForInvoiceCdp(_formato_doc);
                }
                else
                {
                    cod_hash = generatorCdp.GetHashForNoteCdp(_formato_doc);
                }
                //enviar_xml_webservice bata===>>>




            }
            catch (Exception exc)
            {
                _error = exc.Message + " ===Metodo generacion de hash";
            }
        }
        #endregion


        #region<PROPIEDADES ESTATICOS>

        public static string _leer_formato_electronico(string _tipo_doc, string _num_doc, ref string _error)
        {
            string _formato_doc = "";
            string sqlquery = "[USP_Leer_Formato_Electronico]";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tip", _tipo_doc);
                cmd.Parameters.AddWithValue("@doc_id", _num_doc);
                cmd.Parameters.Add("@formato_txt", SqlDbType.NVarChar, -1);
                cmd.Parameters["@formato_txt"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _formato_doc = cmd.Parameters["@formato_txt"].Value.ToString();
            }
            catch (Exception exc)
            {
                _formato_doc = "";
                _error = exc.Message;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return _formato_doc;
        }
        public static DataTable _leer_FE(DateTime _fechaini,DateTime _fechafin,string _bas_id,string _tipo,string _numdocdniruc,string _cliente)
        {
            string sqlquery = "[USP_Leer_Facturas_Electronicas]";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fecha_ini", _fechaini);
                cmd.Parameters.AddWithValue("@fecha_fin", _fechafin);
                cmd.Parameters.AddWithValue("@bas_id", _bas_id);
                cmd.Parameters.AddWithValue("@tipo", _tipo);
                cmd.Parameters.AddWithValue("@numdoc_dniruc",_numdocdniruc);
                cmd.Parameters.AddWithValue("@cliente", _cliente);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }
        public static DataSet _leer_opciones()
        {
            string sqlquery = "USP_Leer_FEOpciones";
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
            }
            catch
            {
                ds = null;
            }
            return ds;
        }
        #endregion
    }
}