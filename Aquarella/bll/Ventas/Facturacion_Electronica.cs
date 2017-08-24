using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using Variables;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Zip;
using System.Xml;
using Carvajal.FEPE.PreSC.Core;
namespace Aquarella.bll
{
    public class Facturacion_Electronica
    {

        #region <CODIGO DE FACTURACION EFACT>

        private static DataTable _leer_venta_anular(string _numero_doc,string _anul="")
        {
            String sqlquery = "USP_Leer_Venta_Anular_Xml";
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
                cmd.Parameters.AddWithValue("@ven_id", _numero_doc);
                cmd.Parameters.AddWithValue("@tipo", _anul);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch
            {
                dt=null;
            }
            return dt;
        }
        private static DataTable _leer_venta(string _numero_doc)
        {
            string sqlquery = "USP_Leer_Venta_Imprimir_Electronico";
            SqlConnection cn=null;
            SqlCommand cmd=null;
            SqlDataAdapter da=null;
            DataTable dt=null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", _numero_doc);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        private const string ConSignos = "áàäéèëíìïóòöúùuñÁÀÄÉÈËÍÌÏÓÒÖÚÙÜçÇ";
        private const string SinSignos = "aaaeeeiiiooouuunAAAEEEIIIOOOUUUcC";

        public static string RemoverSignosAcentos(string texto)
        {
            var textoSinAcentos = string.Empty;

            foreach (var caracter in texto)
            {
                var indexConAcento = ConSignos.IndexOf(caracter);
                if (indexConAcento > -1)
                    textoSinAcentos = textoSinAcentos + (SinSignos.Substring(indexConAcento, 1));
                else
                    textoSinAcentos = textoSinAcentos + (caracter);
            }            
            return textoSinAcentos.ToString().Replace(",", " "); ;
        }

        

        //Leer xml 
        #region <LEER XML GENERADO POR LA DLL DE EFACT>
        public static string leer_datos_xml(string _ruta_carpeta, byte[] _archivo, ref string _documento_return, ref string _valida_return, ref string _mensaje_return)
        {
            string resulado = "";
            //string _ruta_carpeta = "d:/sunat.xml";
            try
            {

                string xml = "";

                //if (System.IO.File.Exists(_ruta_carpeta))
                //{
                //    System.IO.File.Delete(_ruta_carpeta);
                //}


                //if (!(System.IO.File.Exists(_ruta_carpeta)))
                //{

                    //StreamWriter objlog = new StreamWriter(_ruta_carpeta);
                    //objlog.Flush();
                    //objlog.Close();
                    //objlog.Dispose();


                byte[] b = convertir_archivo_a_byte(_ruta_carpeta);
                xml = System.Text.ASCIIEncoding.ASCII.GetString(b);
                //FileStream streamWriter  ;
                //streamWriter = File.OpenWrite(_ruta_carpeta);

                //int size = 8192;
                //byte[] data = new byte[8192];
                //while (true)
                //{
                //    size = s.Read(data, 0, data.Length);
                //    if (size > 0)
                //    {
                //        streamWriter.Write(data, 0, size);
                //    }
                //    else
                //    {
                //        break;

                //    }
                //}
                //xml = System.Text.ASCIIEncoding.ASCII.GetString(data);
                //streamWriter.Close();

                    //byte[] file = File.ReadAllBytes(_ruta_carpeta);
                    //xml = System.Text.ASCIIEncoding.ASCII.GetString(file);
                    //xml = UnzipFile(_ruta_carpeta, _archivo);                    
                //}
              

                XmlDocument doc = new XmlDocument();
                //doc.Load(_ruta_carpeta);
                doc.LoadXml(xml.ToString());

                XmlNodeList documentos = doc.GetElementsByTagName("CreditNote");
                XmlNodeList lista = ((XmlElement)documentos[0]).GetElementsByTagName("cbc:ID");

                string _documento = "";
                string _valida = "";
                string _mensaje = "";

                foreach (XmlElement nodo in lista)
                {
                    XmlNodeList xdocumento = nodo.GetElementsByTagName("cbc:ReferenceID");
                    XmlNodeList xvalida = nodo.GetElementsByTagName("cbc:ResponseCode");
                    XmlNodeList xmensaje = nodo.GetElementsByTagName("cbc:Description");

                    _documento = xdocumento[0].InnerText.Trim();
                    _valida = xvalida[0].InnerText.Trim();
                    _mensaje = xmensaje[0].InnerText.Trim();
                }

                //resulado = Actualiza_Sunat.insertar_actualiza_sunat(_documento, _valida, _mensaje);
                _documento_return = _documento;
                _valida_return = _valida;
                _mensaje_return = _mensaje;

                // string endXml = xml.ToString();                            
                resulado = "";
            }
            catch (Exception exc)
            {               
                resulado = exc.Message;
            }
            return resulado;
        }

        public static byte[] convertir_archivo_a_byte(string ruta)
        {

            FileStream fs = new FileStream(ruta, FileMode.Open, FileAccess.Read);
            /*Create a byte array of file stream length*/
            byte[] b = new byte[fs.Length];
            /*Read block of bytes from stream into the byte array*/
            fs.Read(b, 0, System.Convert.ToInt32(fs.Length));
            /*Close the File Stream*/
            fs.Close();

            return b;
        }

        #endregion

        public static void anular_facturacion_electronica(string _numero_doc,ref string _error,string anul="")
        {
            DataTable dt = null;
            try
            {
                dt = _leer_venta_anular(_numero_doc,anul);
                if (dt!=null)
                {
                    if (dt.Rows.Count>0)
                    {
                        string fecha_doc = dt.Rows[0]["fecha_doc"].ToString();
                        string fecha_act = dt.Rows[0]["fecha_act"].ToString(); ;
                        string codigo_transac = dt.Rows[0]["codigo_transac"].ToString();
                        string Emp_Razon_Alterno = dt.Rows[0]["Emp_Razon_Alterno"].ToString();
                        string Emp_Comercial = dt.Rows[0]["Emp_Comercial"].ToString();
                        string Emp_Ruc_Alterno = dt.Rows[0]["Emp_Ruc_Alterno"].ToString();
                        string emp_ubigeo = dt.Rows[0]["emp_ubigeo"].ToString();
                        string emp_direccion = dt.Rows[0]["emp_direccion"].ToString();
                        string emp_dep = dt.Rows[0]["emp_dep"].ToString();
                        string emp_prv = dt.Rows[0]["emp_prv"].ToString();
                        string emp_dis = dt.Rows[0]["emp_dis"].ToString();
                        string pais = dt.Rows[0]["pais"].ToString();
                        string usuario = dt.Rows[0]["usuario"].ToString();
                        string contraseña = dt.Rows[0]["contraseña"].ToString();
                        string linea = dt.Rows[0]["linea"].ToString();
                        string tipodoc = dt.Rows[0]["tipodoc"].ToString();
                        string serie = dt.Rows[0]["serie"].ToString();
                        string numero = dt.Rows[0]["numero"].ToString();
                        string descripcion = dt.Rows[0]["descripcion"].ToString();
                        string serie_formato = dt.Rows[0]["serie_formato"].ToString();

                        String sFilename = _numero_doc.ToString() + ".csv";

                        string _ruta_carpea = Path.GetDirectoryName(Application.ExecutablePath).ToString() + "\\" + "CSV\\ANULAR\\";

                        if (!(Directory.Exists(_ruta_carpea)))
                        {
                            System.IO.Directory.CreateDirectory(@_ruta_carpea);
                        }

                        sFilename = _ruta_carpea + "\\" + sFilename;
                        FileInfo t = new FileInfo(sFilename);
                        StreamWriter outFile = t.CreateText();
                        System.Text.StringBuilder str = new System.Text.StringBuilder();

                        str.Append(fecha_doc + ",");
                        str.Append(fecha_act + ",");
                        str.Append(codigo_transac + ",");
                        str.Append("\r\n");
                        str.Append(Emp_Razon_Alterno + ",");
                        str.Append(Emp_Comercial + ",");
                        str.Append(Emp_Ruc_Alterno + ",");
                        str.Append(emp_ubigeo + ",");
                        str.Append(emp_direccion + ",");
                        str.Append(emp_dep + ",");
                        str.Append(emp_dis + ",");
                        str.Append(pais + ",");
                        str.Append(usuario + ",");
                        str.Append(contraseña + ",");
                        str.Append("\r\n");
                        str.Append(linea + ",");
                        str.Append(tipodoc + ",");
                        str.Append(serie + ",");
                        str.Append(numero + ",");
                        str.Append(descripcion + "");
                        str.Append("\r\n");
                        str.Append(serie_formato);
                        outFile.WriteLine(str.ToString());

                        outFile.Close();

                        Console.WriteLine(File.ReadAllText(sFilename));


                        //EfactService.BataTransactionServiceClient _client = new EfactService.BataTransactionServiceClient();
                        //EfactService.transactionResponse[] _valida = null; 

                        Byte[] _anular_doc=convertir_archivo_a_byte(sFilename);

                        //_valida = _client.sendVoidedDocument(_anular_doc);
                         

                        //EfactService.sendVoidedDocumentRequest fr = new EfactService.sendVoidedDocumentRequest();
                        
                    }
                }
                
            }
            catch(Exception exc)
            {
                _error = exc.Message.ToString();
                MessageBox.Show(exc.Message.ToString(), "Aviso del sistema...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public static void ejecutar_factura_electronica(string _numero_doc, ref string _codigo_hash)
        //{
        //    try
        //    {

        //        DataTable dt_venta = _leer_venta(_numero_doc);
        //        if (dt_venta != null)
        //        {
        //            if (dt_venta.Rows.Count > 0)
        //            {

        //                Decimal _monto_nc =Convert.ToDecimal(dt_venta.Rows[0]["monto_nc"].ToString());
        //                string _referencia_nc = dt_venta.Rows[0]["referencia_nc"].ToString();
        //                Decimal _monto_nc_sin_igv=0;
        //                var sub_total = dt_venta.AsEnumerable().Select(r => Convert.ToDecimal(r.Field<Decimal>("articulo_total"))).Sum();
        //                var comision = dt_venta.AsEnumerable().Select(r => Convert.ToDecimal(r.Field<Decimal>("ven_det_comisionm"))).Sum();
        //                Decimal igv = Convert.ToDecimal(dt_venta.Rows[0]["ven_igv"].ToString());
        //                decimal _igv_porc_cab=Convert.ToDecimal(dt_venta.Rows[0]["Ven_Igv_Porc"].ToString());
        //                Decimal total = ((sub_total - comision) + igv) - _monto_nc;
        //                Decimal percepcion = 0;
        //                if (total < 0)
        //                { 
        //                    total = 0;
        //                    _monto_nc = ((sub_total - comision) + igv);                            
        //                    percepcion = 0;
        //                }
        //                else
        //                {
        //                    percepcion = Convert.ToDecimal(dt_venta.Rows[0]["ven_percepcionm"].ToString());
        //                }

        //                _monto_nc_sin_igv = Math.Round((_monto_nc) / (_igv_porc_cab + 1), 2, MidpointRounding.AwayFromZero);

        //                string total_letra = Conv_NumLetra.numeroAletras(total);
                        

        //                Decimal total_g = total + percepcion;

        //                String sFilename = _numero_doc.ToString() + ".csv";

        //                string _ruta_carpea = Path.GetDirectoryName(Application.ExecutablePath).ToString() + "\\" + "CSV";

        //                if (!(Directory.Exists(_ruta_carpea)))
        //                {
        //                    System.IO.Directory.CreateDirectory(@_ruta_carpea);
        //                }

        //                sFilename = _ruta_carpea + "\\" + sFilename;
        //                FileInfo t = new FileInfo(sFilename);
        //                StreamWriter outFile = t.CreateText();
        //                System.Text.StringBuilder str = new System.Text.StringBuilder();

        //                string _tipo_doc = (dt_venta.Rows[0]["Bas_Documento"].ToString().Length == 8) ? "BOL" : "FAC";

        //                decimal _comision = 0;
        //                decimal _monto = 0;
        //                decimal _total_sin_igv = 0;
                        
        //                    for (Int32 a = 0; a < dt_venta.Rows.Count; ++a)
        //                    {
        //                        _comision = _comision + Convert.ToDecimal(dt_venta.Rows[a]["ven_det_comisionm"].ToString());
        //                        _monto = _monto + Convert.ToDecimal(dt_venta.Rows[a]["articulo_total"].ToString());
        //                    }
        //                    _total_sin_igv = (_monto - _comision) - _monto_nc_sin_igv;
                        

        //                //insertar cabezera de archivo
        //                str.Append(Convert.ToDateTime(dt_venta.Rows[0]["ven_fecha"].ToString()).ToString("dd/M/yyyy") + ",");
        //                //string doc_n = ((_tipo_doc == "BOL") ? "B" : "F").ToString() + dt_venta.Rows[0]["nrodoc"].ToString();
        //                string doc_n = dt_venta.Rows[0]["nrodoc"].ToString();
        //                str.Append(doc_n + ",");



        //                str.Append(((_tipo_doc=="BOL") ?"3":"1") + ",");
        //                str.Append("PEN" + ",");
        //                str.Append(_total_sin_igv.ToString("#####0.00") + ",");                        
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append(igv.ToString() + ",");
        //                str.Append(igv.ToString() + ",");
        //                str.Append("PEN" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                //str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append(comision.ToString() + ",");                        
        //                str.Append("2005" + ",");
        //                str.Append(total.ToString() + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append(((_tipo_doc != "BOL") ? "2001" : "") + "," );
        //                str.Append(((_tipo_doc != "BOL") ? total.ToString() : "") + "," );
        //                str.Append(((_tipo_doc != "BOL") ? percepcion.ToString() : "2001") + ",");
        //                str.Append(((_tipo_doc != "BOL") ? total_g.ToString() : total.ToString()) + ",");
        //                str.Append(((_tipo_doc != "BOL") ? "" : percepcion.ToString()) + ",");
        //                str.Append(((_tipo_doc != "BOL") ? "" : total_g.ToString()) + ",");
        //                str.Append("" + ",");                        
        //                str.Append("" + ",");

        //                //en este caso vamos a validar y aumentar columnas 

        //                if (_referencia_nc.Length > 0)
        //                {
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append(((_tipo_doc == "BOL") ? _monto_nc.ToString() : "") + ",");
        //                    str.Append(((_tipo_doc == "BOL") ? _referencia_nc : "") + ",");                            
        //                    str.Append(((_tipo_doc != "BOL") ? _monto_nc.ToString() : "") + ",");
        //                    str.Append(((_tipo_doc != "BOL") ? _referencia_nc : "") + ",");
                          


        //                }

        //                //****saltar linea
        //                str.Append("\r\n");
        //                //*********************
        //                str.Append(dt_venta.Rows[0]["Emp_Razon_Alterno"].ToString() + ",");
        //                str.Append(dt_venta.Rows[0]["Emp_Comercial"].ToString() + ",");
        //                str.Append(dt_venta.Rows[0]["Emp_Ruc_Alterno"].ToString() + ",");
        //                str.Append(dt_venta.Rows[0]["Emp_Ubigeo"].ToString() + ",");
        //                str.Append(dt_venta.Rows[0]["Emp_Direccion"].ToString() + ",");
        //                str.Append("" + ",");
        //                str.Append(dt_venta.Rows[0]["emp_dep"].ToString() + ",");
        //                str.Append(dt_venta.Rows[0]["emp_prv"].ToString() + ",");
        //                str.Append(dt_venta.Rows[0]["emp_dis"].ToString() + ",");
        //                str.Append("PE" + ",");
        //                str.Append("TEST" + ",");
        //                str.Append("TEST" + ",");
        //                //****saltar linea
        //                str.Append("\r\n");
        //                //*********************
        //                str.Append(dt_venta.Rows[0]["Bas_Documento"].ToString() + ",");
        //                //1 es dni y 6 ruc
        //                str.Append(((_tipo_doc=="BOL")? "1":"6") + ",");
        //                str.Append(RemoverSignosAcentos(dt_venta.Rows[0]["nombres"].ToString()) + ",");
        //                str.Append(((_tipo_doc == "BOL") ? RemoverSignosAcentos(dt_venta.Rows[0]["Bas_Direccion"].ToString()) : "") + ",");
        //                str.Append(((_tipo_doc == "BOL") ? dt_venta.Rows[0]["pais"].ToString() : dt_venta.Rows[0]["ubigeo_cliente"].ToString()) + ",");
        //                str.Append(((_tipo_doc == "BOL") ? dt_venta.Rows[0]["Bas_Correo"].ToString() : dt_venta.Rows[0]["Bas_Direccion"].ToString()) + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append("" + ",");
        //                str.Append(((_tipo_doc != "BOL") ? "PE" : "") +",");

        //                if (_tipo_doc != "BOL")
        //                {
        //                    str.Append(dt_venta.Rows[0]["Bas_Correo"].ToString() + ",");                            
        //                }

        //                //****saltar linea
        //                str.Append("\r\n");
        //                //*********************
        //                str.Append(total_letra + ",");
        //                str.Append("" + ",");
        //                str.Append("comprobante percepcion" + ",");
        //                //****saltar linea
        //                str.Append("\r\n");
        //                str.Append("\r\n");
        //                //*********************
        //                //**ahora insertamos detalle                        
        //                for (Int32 i = 0; i < dt_venta.Rows.Count; ++i)
        //                {
        //                    string _des_art = dt_venta.Rows[i]["Art_Descripcion"].ToString();
        //                    _des_art = RemoverSignosAcentos(_des_art);
        //                    str.Append((i + 1).ToString() + ",");
        //                    str.Append("PAR" + ",");
        //                    str.Append(dt_venta.Rows[i]["ven_det_cantidad"].ToString() + ",");
        //                    str.Append(_des_art + ",");

        //                    string _precio = Convert.ToDecimal(dt_venta.Rows[i]["precio_inc_igv"].ToString()).ToString("###,##0.00").ToString();
        //                    str.Append(_precio + ",");
        //                    str.Append("01".ToString() + ",");
        //                    string _precio_igv = Convert.ToDecimal(dt_venta.Rows[i]["precio_igv_m"].ToString()).ToString("###,##0.00").ToString();
        //                    string _precio_sin_igv = Convert.ToDecimal(dt_venta.Rows[i]["Ven_Det_Precio"].ToString()).ToString("###,##0.00").ToString();
        //                    string _precio_unsigv = Convert.ToDecimal(dt_venta.Rows[i]["precio_unsigv"].ToString()).ToString("###,##0.00").ToString();
        //                    string _igv_porc = (Convert.ToDecimal(dt_venta.Rows[i]["ven_igv_porc"].ToString()) * 100).ToString();
        //                    string _total_item = Convert.ToDecimal(dt_venta.Rows[i]["total_item"].ToString()).ToString("###,##0.00").ToString();

        //                    str.Append(_precio_igv + ",");
        //                    str.Append(_precio_igv + ",");
        //                    str.Append("10" + ",");
        //                    str.Append("1000" + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append(dt_venta.Rows[i]["art_id"].ToString() + ",");
        //                    str.Append(_precio_sin_igv + ",");
        //                    str.Append(_precio_unsigv + ",");
        //                    str.Append("" + ",");
        //                    str.Append("" + ",");
        //                    str.Append(_igv_porc + ",");
        //                    str.Append(Convert.ToDecimal(dt_venta.Rows[i]["ven_det_comisionm"]).ToString("###,##0.00") + ",");
        //                    str.Append("" + ",");
        //                    str.Append(_total_item + ",");
        //                    //****saltar linea                            
        //                    str.Append("\r\n");
        //                }

        //                str.Append("FF00FF");
        //                //Decimal =
        //                //*******************************************************
                        
        //                outFile.WriteLine(str.ToString());
                        
        //                outFile.Close();

        //                Console.WriteLine(File.ReadAllText(sFilename));

        //                return;
        //                //sFilename

        //                //ahora invocamos la dll de efact
        //                //sFilename="D:/F5130010.csv";
        //                //efact.client.csvdata.ResponseDocument[] res;
        //                //if (_tipo_doc == "BOL")
        //                //{
        //                //    res = efact.client.csvdata.ServiceDocument.boleta(sFilename);
        //                //}
        //                //else
        //                //{
        //                //    res = efact.client.csvdata.ServiceDocument.invoce(sFilename);
        //                //}



        //                //if (res[0].getErrorCode().ToString() != "0")
        //                //{
        //                //    _codigo_hash = "error";
        //                //    return;
        //                //}

        //                //_codigo_hash = res[0].getDigest().ToString();

                       
        //                //envio zip al web service efact
        //                string[] _ar_xml_prin = archivos_xml((_tipo_doc == "BOL") ? "B" : "F");
        //                for (Int32 i = 0; i < _ar_xml_prin.Length; ++i)
        //                {


        //                    string xml1 = _ar_xml_prin[i].ToString();
        //                    string xml2 = _ar_xml_prin[i].ToString().Replace(".xml", "_inter.xml");

        //                    //VALIDAR ARCHIVOS SI EXISTE
        //                    if (!(File.Exists(xml1)))
        //                    {
        //                        MessageBox.Show("El archivo no existe en la ruta specificada : " + xml1, "Aviso del sistema...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        return;
        //                    }
        //                    if (!(File.Exists(xml2)))
        //                    {
        //                        MessageBox.Show("El archivo no existe en la ruta specificada : " + xml2, "Aviso del sistema...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        return;
        //                    }

        //                    //**********************************************************

        //                    Boolean _valor = enviar_facturacion_electronica(xml1, xml2, (_tipo_doc == "BOL") ? "B" : "F");
        //                    if (_valor)
        //                    {
        //                        String[] filenames = { xml1, xml2 };
        //                        for (Int32 a = 0; a < filenames.Length; ++a)
        //                        {
        //                            string xml_borrar = filenames[a].ToString();
        //                            if (File.Exists(xml_borrar))
        //                            {
        //                                File.Delete(xml_borrar);
        //                            }
        //                        }
        //                       // MessageBox.Show("El archivo se envio satisfactoriamente a la WSL efact", "Aviso del sistema...", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                    }
        //                    else
        //                    {
        //                        String[] filenames = { xml1, xml2 };
        //                        string _ruta_error = (_tipo_doc == "BOL") ? Basico._elec_efact_error_boleta : Basico._elec_efact_error_factura;
        //                        string _id="";
        //                        //_id_documento_xml(xml1, ref _id);
        //                        for (Int32 a = 0; a < filenames.Length; ++a)
        //                        {
        //                            string xml_borrar = filenames[a].ToString();
        //                            FileInfo infofile = new FileInfo(xml_borrar);
        //                            string _archivo_copiar = infofile.Name;                                    
        //                            string _ruta_copiar_error = _ruta_error + "\\" + _archivo_copiar;
        //                            File.Copy(xml_borrar, _ruta_copiar_error,true);
        //                            if (File.Exists(xml_borrar))
        //                            {
        //                                File.Delete(xml_borrar);
        //                            }
        //                        }
        //                       // MessageBox.Show("Hubo un error en el envio del archivo zip", "Aviso del sistema...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        //return;
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    catch (Exception exc)
        //    {
                
        //        _codigo_hash = exc.Message;
        //        MessageBox.Show(_codigo_hash, "Aviso del sistema...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private static void _id_documento_xml(string _ruta_xml,ref string _id_doc)
        {
            string _doc = "";
            try
            {

                //StreamWriter objlog = new StreamWriter(_ruta_xml);
                //System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(_ruta_xml);
                //string contents = "";
                //while (reader.Read())
                //{
                //    reader.MoveToContent();
                //    if (reader.NodeType == System.Xml.XmlNodeType.Element)
                //        contents += "<" + reader.Name + ">\n";
                //    if (reader.NodeType == System.Xml.XmlNodeType.Text)
                //        contents += reader.Value + "\n";
                //}
                string xmlText = File.ReadAllText(_ruta_xml);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlText);

                XmlNodeList documentos = doc.GetElementsByTagName("cac:DocumentResponse");
                XmlNodeList lista = ((XmlElement)documentos[0]).GetElementsByTagName("cac:Response");

                string _documento = "";
                string _valida = "";
                string _mensaje = "";

                foreach (XmlElement nodo in lista)
                {
                    XmlNodeList xdocumento = nodo.GetElementsByTagName("cbc:ReferenceID");
                    XmlNodeList xvalida = nodo.GetElementsByTagName("cbc:ResponseCode");
                    XmlNodeList xmensaje = nodo.GetElementsByTagName("cbc:Description");

                    _documento = xdocumento[0].InnerText.Trim();
                    _valida = xvalida[0].InnerText.Trim();
                    _mensaje = xmensaje[0].InnerText.Trim();
                }

            }
            catch
            {
                _doc = "";
            }
            _id_doc = _doc;
        }


        private static Boolean enviar_facturacion_electronica(string xml1, string xml2,string _doc)
        {
            Boolean valor = false;
            try
            {

                string ruta_zip = Path.GetDirectoryName(Application.ExecutablePath).ToString() + "/archivo.zip";

                if (File.Exists(ruta_zip))
                {
                    File.Delete(ruta_zip);
                }

                //crear archivo zip
                ZipOutputStream zipOut = new ZipOutputStream(File.Create(@ruta_zip));

                //*********************
                String[] filenames = { xml1, xml2 };

                for (Int32 i = 0; i < filenames.Length; ++i)
                {
                    string _archivo_xml = filenames[i].ToString();
                    FileInfo fi = new FileInfo(_archivo_xml);
                    ICSharpCode.SharpZipLib.Zip.ZipEntry entry = new ICSharpCode.SharpZipLib.Zip.ZipEntry(fi.Name);
                    FileStream sReader = File.OpenRead(_archivo_xml);
                    byte[] buff = new byte[Convert.ToInt32(sReader.Length)];
                    sReader.Read(buff, 0, (int)sReader.Length);
                    entry.DateTime = fi.LastWriteTime;
                    entry.Size = sReader.Length;
                    sReader.Close();
                    zipOut.PutNextEntry(entry);
                    zipOut.Write(buff, 0, buff.Length);
                }

                zipOut.Finish();
                zipOut.Close();

                byte[] file = File.ReadAllBytes(ruta_zip);


                valor = true;


                //si el archivo existe entonces lo eliminamos porque ya se transformo en bytes
                if (File.Exists(ruta_zip))
                {
                    File.Delete(ruta_zip);
                }

                //EfactService.BataTransactionServiceClient _client = new EfactService.BataTransactionServiceClient();
                //EfactService.transactionResponse[] _valida = null; 

                //switch (_doc)
                //{
                //    case "B":
                //        _valida = _client.sendBoleta(file);
                //        break;
                //    case "F":
                //        _valida = _client.sendInvoice(file);
                //        break;
                //    case "NC":
                //        _valida = _client.sendCreditNote(file);
                //        break;
                //}
                
                //if (_doc == "B")
                //{
                //    _valida = _client.sendBoleta(file);
                //}
                //else
                //{
                //    _valida = _client.sendInvoice(file);
                //}
                //valor del web serice 0 llego correctamente efact


                //string _codigo_valida = _valida[0].responseCode.ToString();

                //if (_codigo_valida == "0")
                //{
                //    valor = true;
                //}
                //else
                //{
                //    valor = false;
                //}
                return valor;
            }
            catch (Exception exc)
            {
                valor = false;
            }
            return valor;
        }
        private static string[] archivos_xml(string _tipodoc)
        {

            //string[] daysArray = { "Mon", "Tue", "Wed", "Thur", "Fri", "Sat", "Sun" };
            ////var days = from day in daysArray select day;
            //var days = from day in daysArray where day == "Thurs" select day;

            //Int32 con=days.Count();
            string[] _xml=null;
            switch (_tipodoc)
            {
                case "B":
                    _xml = System.IO.Directory.GetFiles(@Basico._elec_boleta, "*.xml");
                    break;
                case "F":
                    _xml = System.IO.Directory.GetFiles(@Basico._elec_factura, "*.xml");
                    break;
                case "NC":
                    _xml = System.IO.Directory.GetFiles(@Basico._elec_credito, "*.xml");
                    break;
            }

            //if (_tipodoc == "B")
            //{                
            //    _xml = System.IO.Directory.GetFiles(@Basico._elec_boleta, "*.xml");
            //}
            //else
            //{
            //    _xml = System.IO.Directory.GetFiles(@Basico._elec_factura, "*.xml");
            //}

            var xml_p = from xmlp in _xml where Basico.Right(xmlp, 10) != "_inter.xml" select xmlp;

            //Int32 con =xml_p.Count();
            //var result = Enumerable.Range(0, _xml.Length).Where(i => xml[i]).ToArray();
            return xml_p.ToArray();

        }

        public static void insertar_codigo_hash(string _ven_id, string _hash,string _estado)
        {
            string sqlquery = "USP_Insertar_Codigo_Hash";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", _ven_id);
                cmd.Parameters.AddWithValue("@codigo_hash", _hash);
                cmd.Parameters.AddWithValue("@Estado", _estado);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            if (cn.State == ConnectionState.Open) cn.Close();
        }
        #endregion

        #region<CODIGO PARA FACTURACION CARVAJAL>

        public static string _leer_formato_electronico(string _tipo_doc, string _num_doc,ref string _error)
        {
            string _formato_doc = "";
            string sqlquery = "[USP_Leer_Formato_Electronico]";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State==0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tip", _tipo_doc);
                cmd.Parameters.AddWithValue("@doc_id", _num_doc);
                cmd.Parameters.Add("@formato_txt", SqlDbType.NVarChar,-1);
                cmd.Parameters["@formato_txt"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _formato_doc = cmd.Parameters["@formato_txt"].Value.ToString();
            }
            catch(Exception exc)
            {
                _formato_doc = "";
                _error = exc.Message;
            }
            if (cn.State==ConnectionState.Open) cn.Close();
            return _formato_doc;
        }

        public static void ejecutar_factura_electronica(string _tipo_doc,string _num_doc,ref string cod_hash,ref string _error)
        {
            string _formato_doc = "";

            try
            {

                _formato_doc = _leer_formato_electronico(_tipo_doc, _num_doc, ref _error);
                GeneratorCdp generatorCdp = new GeneratorCdp();
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load("C:\\carvajal\\xml\\20101951872_07_F030_22.xml");

                //byte[] _valor=generatorCdp.GetImageBarCodeForNoteCdp(_formato_doc);

                if (_tipo_doc=="B" || _tipo_doc=="F")
                { 
                    cod_hash = generatorCdp.GetHashForInvoiceCdp(_formato_doc);
                }
                else
                {
                    cod_hash = generatorCdp.GetHashForNoteCdp(_formato_doc);
                }
                //enviar_xml_webservice bata===>>>



                                                       
            }
            catch(Exception exc)
            {
                _error = exc.Message;
            }
        }

        #endregion
    }
}
