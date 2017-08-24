using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using www.aquarella.com.pe.bll.Admonred;
using System.Globalization;
namespace www.aquarella.com.pe.bll.Admonred
{
    public class Consultar_Documento
    {
        #region Private

        private const string urlinforuc = "http://www.sunat.gob.pe/w/wapS01Alias?ruc=";

        private HttpWebRequest _WebRequest;
        private HttpWebResponse _WebResponse;
        private string _WebSource;
        private Infodniruc _Info;
        private bool _ok;
        private string _error;

        private bool LoadWebSource(string url)
        {
            _WebRequest = (HttpWebRequest)WebRequest.Create(url);
            _WebRequest.Proxy = null;

            try
            {
                _WebResponse = (HttpWebResponse)_WebRequest.GetResponse();
            }
            catch
            {
                _ok = false;
                _error = "Error al consultar con Sunat";
                return false;
            }

            Stream _Stream = _WebResponse.GetResponseStream();

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");


            StreamReader _StreamReader = new StreamReader(_Stream, encode);

            _WebSource = HttpUtility.HtmlDecode(_StreamReader.ReadToEnd());

            return true;
        }

        private bool ParseInfoContribuyente(List<string> _resul, string ruc)
        {
            try
            {
                _Info = new Infodniruc();
                _Info.Ruc = ruc;

                for (int i = 0; i < _resul.Count; i++)
                {
                    switch (_resul[i])
                    {
                        case "Número Ruc.":
                            _Info.RazonSocial = _resul[i + 2].Substring(14);
                            break;
                        case "Antiguo Ruc.":
                            _Info.AntiguoRuc = _resul[i + 5];
                            break;
                        case "Estado.":
                            _Info.Estado = _resul[i + 2];
                            break;
                        case "Agente Retención IGV.":
                            _Info.EsAgenteRetencion = _resul[i + 3];
                            break;
                        case "Nombre Comercial.":
                            _Info.NombreComercial = _resul[i + 3];
                            break;
                        case "Dirección.":
                            _Info.Direccion = _resul[i + 3];
                            break;
                        case "Teléfono(s).":
                            _Info.Telefono = _resul[i + 3];
                            break;
                        case "Dependencia.":
                            _Info.Dependencia = _resul[i + 3];
                            break;
                        case "Tipo.":
                            _Info.Tipo = _resul[i + 3];
                            break;
                        case "Fecha Nacimiento.":
                            _Info.Fecha_Nac = _resul[i + 2];
                            break;

                    }
                }

                return true;
            }
            catch
            {
                _ok = false;
                _error = "Error al procesar informacion de sunat(Funcion ParseInfo)";
            }
            return false;
        }

        private void LoadInfoContribuyente(string dniruc)
        {
            try
            {
                if (LoadWebSource(String.Format("{0}{1}", urlinforuc, dniruc)))
                {

                    string[] _split = _WebSource.Split(new char[] { '<', '>' });
                    List<string> _resul = new List<string>();

                    //quitamos todos los caracteres nulos y convertirmos todo  utf8
                    for (int i = 0; i < _split.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(_split[i].Trim()))
                            _resul.Add(_split[i].Trim());
                    }

                    string[] _car = null;

                    if (_resul.Count == 34) //no es valido o algo salio mal
                    {
                        _ok = false;
                        _error = _resul[15];
                    }
                    else
                    {
                        _car = _resul[14].Split(new char[] { '"' });

                        if (_car[1] == "Resultado")
                        {
                            _ok = true;
                        }
                        else
                        {
                            _ok = false;
                            _error = "El servidor de Sunat no devolvio una respuesta conocida(Funcion LoadInfo)";
                        }
                    }

                    ParseInfoContribuyente(_resul, dniruc);
                }
            }
            catch
            {

            }
        }

        #endregion
        #region Constructor

        public Consultar_Documento(string dniruc)
        {
            LoadInfoContribuyente(dniruc);
        }        
        #endregion
        #region Public

        public string Error
        {
            get
            {
                if (_ok)
                    return string.Empty;
                else
                    return _error;
            }
        }

        public Infodniruc GetInfo
        {
            get
            {
                if (_ok)
                    return _Info;
                else
                    return null;
            }

        }

        #endregion

        //leerdigito
        #region <LEER DIGITO DOCUMENTO>
        public static int getDigito(string nro)
        {
            int[] numArray = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int num5 = 0;
            int num7 = nro.Length - 1;
            for (int i = 0; i <= num7; i++)
            {
                num5 += int.Parse(nro.Substring(i, 1)) * numArray[i];
            }
            int num3 = num5 % 11;
            int num4 = 11 - num3;
            switch (num4)
            {
                case 10:
                    return 0;

                case 11:
                    return 1;
            }
            return num4;
        }

        public static void divide_nombres(string nombres_appellidos,ref string primer_nombre, ref string segundo_nombre, ref string primer_apellido, ref string segundo_apellido)
        {
            string[] nombres = splitString(nombres_appellidos, ' ');

            string _seg_ape_comp = "";
            string _seg_nom_comp = ""; bool _segn_nom = false;
            for (Int32 i = 0; i < nombres.Length; ++i)
            {
                switch (i)
                {
                    case 0:
                        primer_apellido = nombres[i].ToString();
                        break;
                    case 1:
                        if (!(nombres.Length == 3))
                        {
                            _seg_ape_comp = nombres[i].ToString();
                        }
                        break;
                    case 2:
                        if (nombres[i].Contains("DE"))
                        {
                            _seg_ape_comp = _seg_ape_comp + ' ' + nombres[i].ToString();
                        }
                        break;
                    case 3:
                        if (_seg_ape_comp.Contains("DE"))
                        {
                            _seg_ape_comp = _seg_ape_comp + ' ' + nombres[i].ToString();
                        }

                        for (Int32 a = i; a < nombres.Length; ++a)
                        {
                            if ((nombres[a].Contains("DE")))
                            {
                                _segn_nom = true;
                                break;
                            }

                        }

                        break;
                    case 4:
                        if (!(_segn_nom))
                            _seg_ape_comp = _seg_ape_comp + ' ' + nombres[i].ToString();
                        break;
                }

                if (nombres.Length - 2 == i)
                {
                    if (nombres[i].Contains("DE"))
                    {
                        primer_nombre = nombres[i - 1].ToString();
                        _seg_nom_comp = nombres[i].ToString();
                    }
                    else
                    {
                        primer_nombre = nombres[i].ToString();
                    }
                }

                if (nombres.Length - 1 == i)
                {
                    segundo_nombre = ((_seg_nom_comp.Length > 0) ? _seg_nom_comp + " " : "") + nombres[i].ToString();
                }


            }

            segundo_apellido = _seg_ape_comp;
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
        #endregion

        public static string Convert_MayusMin(string _texto)
        {           
            string cadena = _texto.ToLower();
            cadena = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(cadena);
            return cadena;
        }

    }
}