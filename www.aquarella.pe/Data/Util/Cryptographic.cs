using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace www.aquarella.pe.Data.Util
{
    public class Cryptographic
    {
        #region < Metodos staticos >
        private static string _key = "_MANISOL";
        //vector de inicialización
        private static string _depl = "_BATA_SA";
        /// <summary>
        /// Encripta una cadena.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static string encrypt(string cadena)
        {
            // Create a new DES key.
            DESCryptoServiceProvider key = new DESCryptoServiceProvider();
            key.Key = Encoding.UTF8.GetBytes(_key);
            key.IV = Encoding.UTF8.GetBytes(_depl);

            // Encrypt a string to a byte array.
            byte[] buffer = encrypt(cadena, key);

            string cad;
            //CONVIERTE EN STRING EL ARREGLO DE BYTES
            cad = Convert.ToBase64String(buffer);

            return cad;
        }

        /// <summary>
        /// Encriptar una cadena
        /// </summary>
        /// <param name="PlainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static byte[] encrypt(string PlainText, SymmetricAlgorithm key)
        {
            // Create a memory stream.
            MemoryStream ms = new MemoryStream();

            // Create a CryptoStream using the memory stream and the 
            // CSP DES key.  
            CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);

            // Create a StreamWriter to write a string
            // to the stream.
            StreamWriter sw = new StreamWriter(encStream);

            // Write the plaintext to the stream.
            sw.WriteLine(PlainText);

            // Close the StreamWriter and CryptoStream.
            sw.Close();
            encStream.Close();

            // Get an array of bytes that represents
            // the memory stream.
            byte[] buffer = ms.ToArray();

            // Close the memory stream.
            ms.Close();

            // Return the encrypted byte array.
            return buffer;
        }

        /// <summary>
        /// Desencripta una cadena encriptada.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public static string decrypt(string cadena)
        {
            try
            {
                // Create a new DES key.
                DESCryptoServiceProvider key = new DESCryptoServiceProvider();
                key.Key = Encoding.UTF8.GetBytes(_key);
                key.IV = Encoding.UTF8.GetBytes(_depl);
                //Convierte la cadena entrante en arreglo de Bytes           
                byte[] bytes = Convert.FromBase64String(cadena);

                // Decrypt the byte array back to a string.
                string plaintext = decrypt(bytes, key);
                return plaintext;
            }
            catch (Exception exc)
            {
                return null;
            }

        }

        /// <summary>
        /// Decrypt the byte array.
        /// </summary>
        /// <param name="CypherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string decrypt(byte[] CypherText, SymmetricAlgorithm key)
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

        public static string getMd5Hash(string value)
        {
            // Crea una nueva instancia del objeto MD5.
            MD5 md5Hasher = MD5.Create();
            // Convierte la cadena ingresada a un array de bytes y genera el hash.      
            Byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            // Create a new Stringbuilder to collect the bytes and create a string.      
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data and format each one as a hexadecimal string. 
            for (int i = 0; i <= data.Length - 1; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        #endregion
    }
}