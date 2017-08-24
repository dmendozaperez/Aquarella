using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Aquarella.bll
{
    class Utilities
    {
        #region < VARIABLES >
        //llave
        private static string LLAVE = "_MANISOL";
        //vector de inicialización
        private static string DESPL = "_BATA_SA";
        #endregion

        /// <summary>
        /// Verificar si una cadena es numerica o pesee algun caracter
        /// </summary>
        /// <param name="chain">Cadena</param>
        /// <returns>True or False</returns>
        public static Boolean isNumeric(String chain)
        {
            ///
            for (int i = 0; i < chain.Length; i++)
            {
                ///
                if (!char.IsNumber(chain, i))
                    return false;
            }
            ///
            return true;
        }

        /// <summary>
        /// Desencripta una cadena encriptada.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static string DECRYPT(string cadena)
        {
            // Create a new DES key.
            DESCryptoServiceProvider key = new DESCryptoServiceProvider();
            key.Key = Encoding.UTF8.GetBytes(LLAVE);
            key.IV = Encoding.UTF8.GetBytes(DESPL);
            //Convierte la cadena entrante en arreglo de Bytes           
            byte[] bytes = Convert.FromBase64String(cadena);

            // Decrypt the byte array back to a string.
            string plaintext = Decrypt(bytes, key);

            return plaintext;
        }

        /// <summary>
        /// Decrypt the byte array.
        /// </summary>
        /// <param name="CypherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string Decrypt(byte[] CypherText, SymmetricAlgorithm key)
        {
            // Create a memory stream to the passed buffer.
            MemoryStream ms = new MemoryStream(CypherText);

            // Create a CryptoStream using the memory stream and the 
            // CSP DES key. 
            CryptoStream encStream = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);

            // Create a StreamReader for reading the stream.
            StreamReader sr = new StreamReader(encStream);

            // Read the stream as a string.
            string val = sr.ReadLine();

            // Close the streams.
            sr.Close();

            encStream.Close();

            ms.Close();

            return val;
        }

    }
}
