using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Net;
using System.IO;
using System.Data;
namespace www.aquarella.com.pe.bll.Admonred
{
    public class Persona
    {
        public enum Resul
        {
            /// <summary>
            /// Se encontro la persona
            /// </summary>
            Ok = 0,
            /// <summary>
            /// No se encontro la persona
            /// </summary>
            NoResul = 1,
            /// <summary>
            /// la imagen capcha no es valida
            /// </summary>
            ErrorCapcha = 2,
            /// <summary>
            /// Error no especificado
            /// </summary>
            Error = 3,
        }

        private Resul state;
        private string _Nombres;
        private string _ApePaterno;
        private string _ApeMaterno;
        private string _primer_nombre;
        private string _segundo_nombre;
        private CookieContainer myCookie;

        private string _direccion;
        private string _telefono;

        public Int32 estado_reniec { set; get; }

        #region Propiedades

        public string direccion { get { return _direccion; } }
        public string telefono { get { return _telefono; } }
        /// <summary>
        /// Devuelve la imagen para el reto capcha
        /// </summary>
        public Image GetCapcha { get { return ReadCapcha(); } }

        public byte[] GetByteCaptcha { get { return ReadCapchaBytes(); } }

        /// <summary>
        /// Si no Hubo error en la lectura de datos devuelve los nombres 
        /// de la persona caso contrario devuelve ""
        /// </summary>
        public string Nombres { get { return _Nombres; } }

        public string primer_nombre { get { return _primer_nombre; } }
        public string segundo_nombre { get { return _segundo_nombre; } }
        /// <summary>
        /// Si no Hubo error en la lectura de datos devuelve el Apellido Paterno
        /// de la persona caso contrario devuelve ""
        public string ApePaterno { get { return _ApePaterno; } }

        /// <summary>
        /// Si no Hubo error en la lectura de datos devuelve el Apellido Materno
        /// de la persona caso contrario devuelve ""
        public string ApeMaterno { get { return _ApeMaterno; } }

        /// <summary>
        /// Devuelve el resultado de la busqueda de DNI
        /// </summary>
        public Resul GetResul { get { return state; } }

        #endregion

        #region Constructor

        public Persona()
        {
            try
            {
                myCookie = null;
                myCookie = new CookieContainer();

                //Permitir SSL
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                //ReadCapcha();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        /// <summary>
        /// Carga la imagen Capcha
        /// </summary>
        /// 

        private byte[] ReadCapchaBytes()
        {
            return imageToByteArray(ReadCapcha());
        }

        private byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
         MemoryStream ms = new MemoryStream();
         imageIn.Save(ms,System.Drawing.Imaging.ImageFormat.Gif);
         return  ms.ToArray();
        }

        private Image ReadCapcha()
        {
            try
            {                

                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("https://cel.reniec.gob.pe/valreg/codigo.do");

                myWebRequest.CookieContainer = myCookie;

                myWebRequest.Proxy = null;
                

                myWebRequest.Credentials = CredentialCache.DefaultCredentials;

                myWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();

                Stream myImgStream = myWebResponse.GetResponseStream();

                //myWebResponse.Close();

                return Image.FromStream(myImgStream);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inicia la carga de los datos de la persona 
        /// </summary>
        /// <param name="numDni"></param>
        /// <param name="ImgCapcha"></param>
        /// 
        public static DataTable _consultaReniec(string _dni)
        {
            DataTable dt = null;
            try
            {

                ws_clientedniruc.Cons_ClienteSoapClient ws_cliente = new ws_clientedniruc.Cons_ClienteSoapClient();
                dt = ws_cliente.ws_persona_reniec(_dni);

            }
            catch (Exception exc)
            {
                dt = null;
            }
            return dt;
        }
        public void GetInfo(string numDni, string ImgCapcha)
        {
            this._direccion = "";
            this._telefono = "";
            try
            {
                this.estado_reniec = 0;
                DataTable dt_data = _consultaReniec(numDni);

                if (dt_data == null)
                {
                    state = Resul.NoResul;
                    return;
                }
                else
                { 
                    if (dt_data.Rows.Count==0)
                    {
                        state = Resul.NoResul;
                        return;
                    }
                    else
                    {
                        this.estado_reniec = Convert.ToInt32(dt_data.Rows[0]["estado"]);
                //string myUrl = String.Format("https://cel.reniec.gob.pe/valreg/valreg.do?accion=buscar&nuDni={0}&imagen={1}",
                //                        numDni, ImgCapcha);

                        //HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
                        //myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";//esto creo que lo puse por gusto :/
                        //myWebRequest.CookieContainer = myCookie;
                        //myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                        //myWebRequest.Proxy = null;

                        //myWebRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => { return true; };

                        //HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();

                        //Stream myStream = myHttpWebResponse.GetResponseStream();

                        //StreamReader myStreamReader = new StreamReader(myStream);

                        //string _WebSource = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());

                        //string[] _split = _WebSource.Split(new char[] { '<', '>', '\n', '\r' });

                        //List<string> _resul = new List<string>();

                        ////quitamos todos los caracteres nulos
                        //for (int i = 0; i < _split.Length; i++)
                        //{
                        //    if (!string.IsNullOrEmpty(_split[i].Trim()))
                        //        _resul.Add(_split[i].Trim());
                        //}



                        // Anlizando la el arreglo "_resul" llegamos a la siguiente conclusion
                        // 
                        // _resul.Count == 217 cuando nos equivocamos en el captcha
                        // _resul.Count == 232 cuando todo salio ok
                        // _resul.Count == 222 cuando no existe el DNI
                        //

                        switch (this.estado_reniec)
                {
                    case 217:
                        state = Resul.ErrorCapcha;
                        break;
                     case 231:
                        state = Resul.Ok;
                        break;
                    case 232:
                        state = Resul.Ok;
                        break;
                    case 222:
                        state = Resul.NoResul;
                        break;
                    default:
                        state = Resul.Error;
                        break;
                }

                if (state == Resul.Ok)
                {
                    this._Nombres = dt_data.Rows[0]["nombres"].ToString();// _resul[185].Trim();
                    string[] nombres = splitString(_Nombres, ' ');                    
                    string _seg_nom_comp = ""; bool _segn_nom = false;
                   if (_Nombres.Length>0)
                   {
                       for (Int32 i = 0; i < nombres.Length; ++i)
                       {
                           if (i==0)
                           {
                               this._primer_nombre = nombres[i].ToString();
                           }
                           else
                           {
                               if (i >= 2) _seg_nom_comp = _seg_nom_comp + " ";
                               
                               _seg_nom_comp += nombres[i].ToString();
                           }
                       }
                       this._segundo_nombre = _seg_nom_comp;
                   }
                   else
                   {
                       this._primer_nombre = nombres[0].ToString();
                       this._segundo_nombre = "";
                   }
                    
                    this._ApePaterno = dt_data.Rows[0]["apepat"].ToString();// _resul[186];
                    this._ApeMaterno = dt_data.Rows[0]["apemat"].ToString();// _resul[187];
                }
                
                }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string[] splitString(string _textString, char _character)
        {
            string[] split = null;
            if (!string.IsNullOrEmpty(_textString))
            {
                split = _textString.Split(new Char[] { _character });
            }
            return split;
        }

        public void GetInfo(string numDni)
        {
            DataTable dt = null;
            this._direccion = "";
            this._telefono = "";
            this._primer_nombre = "";
            this._segundo_nombre = "";
            this._ApePaterno = "";
            this._ApeMaterno = "";
            try
            {
                 dt = GetSunat(numDni);                 

            }
            catch (Exception exc)
            {
                dt = null;
            }
            if (dt == null)
            {
                state = Resul.NoResul;
            }
            else
            {
                if (dt.Rows.Count == 0)
                {
                    state = Resul.NoResul;
                }
                else
                {
                    string _error = dt.Rows[0]["nombres"].ToString();
                    if (_error == "Error!")
                    {
                        state = Resul.NoResul;
                    }
                    else
                    {
                        state= Resul.Ok;
                        this._Nombres= dt.Rows[0]["nombres"].ToString();
                        this._primer_nombre = dt.Rows[0]["nombres"].ToString();
                        this._direccion = dt.Rows[0]["direccion"].ToString();
                        this._telefono= dt.Rows[0]["telefono"].ToString();
                    }
                }
            }


        }

        private static DataTable GetSunat(string _ruc)
        {
            DataTable dt = null;
            try
            {

                ws_clientedniruc.Cons_ClienteSoapClient ws_cliente = new ws_clientedniruc.Cons_ClienteSoapClient();
                dt = ws_cliente.ws_persona_sunat(_ruc);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }
    }
}