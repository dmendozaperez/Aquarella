using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace Sistema_Aquarella
{
    public class Conv_NumLetra
    {
        private  const string moneda = "NUEVOS SOLES";

        public static string numeroAletras(decimal numero)
        {
            StringBuilder res = new StringBuilder();
            try
            {
                string parteEntera = numero.ToString("#0.00").Split('.')[0];
                string parteDecimal = numero.ToString("#0.00").Split('.')[1];

                StringBuilder numeroFormateado = new StringBuilder();
                int cuenta3 = 0;
                for (int p = parteEntera.Length - 1; p >= 0; p--)
                {
                    if (cuenta3 == 3)
                    {
                        numeroFormateado.Insert(0, "|");
                        cuenta3 = 0;
                    }
                    numeroFormateado.Insert(0, parteEntera.Substring(p, 1));
                    cuenta3++;
                }
                string[] numeros = numeroFormateado.ToString().Split('|');

                for (int p = 0; p < numeros.Length; p++)
                {
                    string n = num2texto(numeros[p]);
                    res.Append(n);
                    if (!n.Trim().Equals(""))
                    {
                        res.Append(grupos(numeros.Length - p - 1));
                    }
                }

                res = res.Replace("DIECIUNO", "ONCE");
                res = res.Replace("DIECIDOS", "DOCE");
                res = res.Replace("DIECITRES", "TRECE");
                res = res.Replace("DIECICUATRO", "CATORCE");
                res = res.Replace("DIECICINCO", "QUINCE");
                res = res.Replace("  ", " ");
                res = res.Replace("  ", " ");
                if (res.ToString().StartsWith("UNO MILLONES"))
                {
                    res = res.Replace("UNO MILLONES", "UN MILLON");
                }
                res = res.Replace("UNO MIL", "UN MIL");


                return res.ToString() + "Y " + parteDecimal + "/100 " + moneda;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private static string grupos(int p)
        {
            string res = string.Empty;
            switch (p)
            {
                case 1:
                    res = " MIL ";
                    break;
                case 2:
                    res = " MILLONES ";
                    break;
                case 3:
                    res = " MIL ";
                    break;
            }
            return res;
        }

        private static string num2texto(string numero)
        {
            string res = string.Empty;

            switch (numero.Length)
            {
                case 1:
                    res += unidad2texto(int.Parse(numero.Substring(0, 1)));
                    break;

                case 2:
                    res += decena2texto(int.Parse(numero.Substring(0, 1)));

                    if (int.Parse(numero.Substring(0, 1)) > 0
                        && int.Parse(numero.Substring(1, 1)) == 0)
                    {
                        res = res.Replace("DIECI", "DIEZ ");
                        res = res.Replace("VEINTI", "VEINTE ");
                    }


                    if (int.Parse(numero.Substring(0, 1)) >= 3
                        && int.Parse(numero.Substring(1, 1)) > 0)
                    {
                        res += " Y ";
                    }
                    if (int.Parse(numero.Substring(1, 1)) > 0)
                    {
                        res += unidad2texto(int.Parse(numero.Substring(1, 1)));
                    }
                    break;

                case 3:
                    res += centena2texto(int.Parse(numero.Substring(0, 1)));

                    if (int.Parse(numero.Substring(0, 1)) > 0
                        && int.Parse(numero.Substring(1, 2)) == 0)
                    {
                        res = res.Replace("CIENTO", "CIEN ");
                    }

                    if (int.Parse(numero.Substring(1, 2)) > 0)
                    {
                        if (int.Parse(numero.Substring(1, 1)) > 0)
                        {
                            res += decena2texto(int.Parse(numero.Substring(1, 1)));

                            if (int.Parse(numero.Substring(1, 1)) > 0
                                && int.Parse(numero.Substring(2, 1)) == 0)
                            {
                                res = res.Replace("DIECI", "DIEZ ");
                                res = res.Replace("VEINTI", "VEINTE ");
                            }

                            if (int.Parse(numero.Substring(1, 1)) >= 3
                                && int.Parse(numero.Substring(1, 1)) > 0)
                            {
                                res += " Y ";
                            }
                        }

                        if (int.Parse(numero.Substring(2, 1)) > 0)
                        {
                            res += unidad2texto(int.Parse(numero.Substring(2, 1)));
                        }
                    }
                    break;
            }
            return res;
        }
        private static string centena2texto(int numero)
        {
            string res = string.Empty;
            switch (numero)
            {
                case 1:
                    res = "CIENTO ";
                    break;
                case 2:
                    res = "DOSCIENTOS ";
                    break;
                case 3:
                    res = "TRESCIENTOS ";
                    break;
                case 4:
                    res = "CUATROCIENTOS ";
                    break;
                case 5:
                    res = "QUINIENTOS ";
                    break;
                case 6:
                    res = "SEISCIENTOS ";
                    break;
                case 7:
                    res = "SETECIENTOS ";
                    break;
                case 8:
                    res = "OCHOCIENTOS ";
                    break;
                case 9:
                    res = "NOVECIENTOS ";
                    break;
            }
            return res;
        }
        private static string decena2texto(int numero)
        {
            string res = string.Empty;
            switch (numero)
            {
                case 1:
                    res = "DIECI";
                    break;
                case 2:
                    res = "VEINTI";
                    break;
                case 3:
                    res = "TREINTA ";
                    break;
                case 4:
                    res = "CUARENTA ";
                    break;
                case 5:
                    res = "CINCUENTA ";
                    break;
                case 6:
                    res = "SESENTA ";
                    break;
                case 7:
                    res = "SETENTA ";
                    break;
                case 8:
                    res = "OCHENTA ";
                    break;
                case 9:
                    res = "NOVENTA ";
                    break;
            }
            return res;
        }
        private static string unidad2texto(int numero)
        {
            string res = string.Empty;
            switch (numero)
            { 
                case 0:
                    res = "CERO ";
                    break;
                case 1:
                    res = "UNO ";
                    break;
                case 2:
                    res = "DOS ";
                    break;
                case 3:
                    res = "TRES ";
                    break;
                case 4:
                    res = "CUATRO ";
                    break;
                case 5:
                    res = "CINCO ";
                    break;
                case 6:
                    res = "SEIS ";
                    break;
                case 7:
                    res = "SIETE ";
                    break;
                case 8:
                    res = "OCHO";
                    break;
                case 9:
                    res = "NUEVE ";
                    break;
            }
            return res;
        }

    
    }
}
