using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogoVirtual
{
    public class Basico
    {

        public static string conexion
        {
            get { return "Server=www.aquarellaperu.com.pe;Database=BdAquarella;User ID=sa;Password=Bata2013;Trusted_Connection=False;"; }
        }
        public static string _ruta_catalogo(string _rc_id)
        {
            string _ruta = "";
            string sqlquery = "USP_LeerRutaCatalogoVirtual";          
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Rc_Id", _rc_id);
                        cmd.Parameters.Add("@ruta_catalogo", SqlDbType.VarChar, 800);
                        cmd.Parameters["@ruta_catalogo"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        _ruta = cmd.Parameters["@ruta_catalogo"].Value.ToString();
                    }
                }
            }
            catch
            {

            }
            return _ruta;
        }


        public static void ejecuta_pdf(string file_html, string file_destination)
        {            
            try
            {                                                 
                        string readContentsprincipal;
                        using (StreamReader streamReader = new StreamReader(@file_html, Encoding.UTF8))
                        {
                            readContentsprincipal = streamReader.ReadToEnd();
                        }                                                                           
                        string _readcontentsecundario = readContentsprincipal;                                                                        

                        GeneraPDF(_readcontentsecundario, file_destination);
                        
            }
            catch
            {

            }
        }
        private static bool GeneraPDF(string readContents, string _file_pdf_destino)
        {
            Boolean _valida = false;
            try
            {
                //string readContents;
                //using (StreamReader streamReader = new StreamReader(@_file_html, Encoding.UTF8))
                //{
                //    readContents = streamReader.ReadToEnd();
                //}
                //readContents = readContents.Replace("XXXXXXXX", "AAAAAAA");
                var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
                htmlToPdf.PageHeight = 180;
                htmlToPdf.PageWidth = 140;
                var margins = new NReco.PdfGenerator.PageMargins();
                margins.Bottom = 2;
                margins.Top = 1;
                margins.Left = 3;
                margins.Right = 5;
                htmlToPdf.Margins = margins;
                htmlToPdf.Orientation = NReco.PdfGenerator.PageOrientation.Portrait;
                htmlToPdf.Zoom = 1F;
                htmlToPdf.Size = NReco.PdfGenerator.PageSize.A4;
                var pdfBytes = htmlToPdf.GeneratePdf(readContents);
                File.WriteAllBytes(@_file_pdf_destino, pdfBytes);
                _valida = true;
            }
            catch
            {
                _valida = false;
            }
            return _valida;
        }
    }
}
