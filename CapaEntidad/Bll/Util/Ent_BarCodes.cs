using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad.Bll.Util
{
    public class Ent_BarCodes
    {
        public static String[] getInfoFromTheBarCode(String codeBar)
        {
            String[] infoArray = new String[3];
            ///

            /// Determinar el numero de digitos enviados en la cadena
            /// 
            int numDigitsInCodeBar = codeBar.Length;

            /// Determinado el numero de digitos en el codigo de barras enviarlo a la
            /// funcion que lo podra evaluar
            /// 
            if (numDigitsInCodeBar == 14)
            {
                ///
                String refArticle = codeBar.ToString().Substring(0, 8);

                ///
                String size = codeBar.Substring(8, 2);

                /// Ref Articulo
                /// 
                infoArray[0] = refArticle;

                /// Talla
                /// 
                infoArray[1] = size;

                ///
                return infoArray;
            }
            else if (numDigitsInCodeBar == 10)
            {
                ///
                ///
                String refArticle = codeBar.ToString().Substring(0, 7);

                ///
                String size = codeBar.Substring(7, 2);

                string calidad = codeBar.ToString().Substring(codeBar.Length - 1, 1);
                /// Ref Articulo
                /// 
                infoArray[0] = refArticle;

                /// Talla
                /// 
                infoArray[1] = size;

                ///
                //    //calidad

                infoArray[2] = calidad;

                return infoArray;
            }
            /// Ean13
            /// Digit 1-> Company
            /// Digit 2-9-> Article ref
            /// Digit 10-> Cte
            /// Digit 11-12-> Position in plane
            /// Digit 13-> Static of verification
            /// BARRA EA13
            else if (numDigitsInCodeBar == 13)
            {
                ///
                String refArticle = codeBar.ToString().Substring(1, 7);

                ///
                String posPlaneColumn = Convert.ToDecimal((codeBar.Substring(10, 2)).ToString()).ToString();

                String calidad = (codeBar.Substring(8, 1)).ToString();

                /// Ref Articulo
                /// 
                infoArray[0] = refArticle;

                /// Columna en el plano
                /// 
                infoArray[1] = posPlaneColumn;

                infoArray[2] = calidad;
                ///
                return infoArray;
            }
            /// Ean18
            /// Digit 1 - 3-> Año (1 digito), semana (2 digitos)
            /// Digit 3 - 10-> Article ref
            /// Digit 11-12-> Position in plane
            /// Digit 13 - 18 -> Others
            else if (numDigitsInCodeBar == 18)
            {
                ///
                String refArticle = codeBar.ToString().Substring(3, 7);

                ///
                String posPlaneColumn = (Convert.ToDecimal(codeBar.Substring(10, 2))).ToString();

                /// Ref Articulo
                /// 
                infoArray[0] = refArticle;

                /// Columna en el plano
                /// 
                infoArray[1] = posPlaneColumn;

                ///
                return infoArray;
            }
            else if(numDigitsInCodeBar==9)
            {
                ///
                ///
                String refArticle = codeBar.ToString().Substring(0, 7);

                ///
                String size = codeBar.Substring(7, 1);

                string calidad = codeBar.ToString().Substring(codeBar.Length - 1, 1);
                /// Ref Articulo
                /// 
                infoArray[0] = refArticle;

                /// Talla
                /// 
                infoArray[1] = size;

                ///
                //    //calidad

                infoArray[2] = calidad;

                return infoArray;
            }

            /// para cuando sea un accesorio talla de un caracter
            //else if (numDigitsInCodeBar == 9)
            //{
            //    ///
            //    ///
            //    String refArticle = codeBar.ToString().Substring(0, 7);

            //    ///
            //    String size = codeBar.Substring(7, 2);

            //    string calidad = codeBar.ToString().Substring(codeBar.Length -1 , 1); 

            //    /// Ref Articulo
            //    /// 
            //    infoArray[0] = refArticle;

            //    /// Talla
            //    /// 
            //    infoArray[1] = size;

            //    //calidad

            //    infoArray[2] = calidad;
            //    ///
            //    return infoArray;
            //}

            ///
            return null;
        }
    }
}
