using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using informativa.aquarella.com.oe.Models;
using informativa.aquarella.com.oe.Data;
using informativa.aquarella.com.oe.Models.Util;
using System.IO;
using System.Text;
namespace informativa.aquarella.com.oe.Controllers
{
    public class CatalogoController : Controller
    {
        // GET: Catalogo
        private CatalogoBL catalogoBL = new CatalogoBL();

        public ActionResult Index()
        {
            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                return RedirectToAction("Index", "Admin", "");

            }
            else
            {
                return View();
            }
        }

        public PartialViewResult List()
        {

            return PartialView(listar());
        }

        public ActionResult Nuevo()
        {
            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                return RedirectToAction("Index", "Admin", "");

            }
            else
            {
               
                return View();
            }
        }

        public List<Ent_Catalogo> listar()
        {
            List<Ent_Catalogo> listCatalogo = new List<Ent_Catalogo>();
            listCatalogo = catalogoBL.listarCatalogoMantenedor();

            return listCatalogo;
        }

        [HttpGet]
        public ActionResult Editar(string strId)
        {
            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                return RedirectToAction("Index", "Admin", "");

            }
            else
            {
                Ent_Catalogo catalogo = new Ent_Catalogo();
                catalogo = catalogoBL.GetCatalogo(strId);
                return View("Editar", catalogo);
            }
        }
      
        private void crear_pdf(string NombreCarpeta)
        {
            try
            {

                string _folder_principal_path = System.Web.HttpContext.Current.Server.MapPath("~" +Conexion.Str_RutaPlantilla);
                string ruta_foto = _folder_principal_path + "\\" + NombreCarpeta + "\\pages";

                string[] _foto = null;
                _foto = Directory.GetFiles(@ruta_foto, "*.jpg");
                
                if (_foto.Length > 0)
                {
                    string path_destino_pdf = _folder_principal_path + "\\" + NombreCarpeta + "\\pdf";
                    string file_detino_html_pdf = path_destino_pdf + "\\catalogo.html";
                    string file_destino_pdf = path_destino_pdf + "\\catalogo.pdf";
                    string readContentsprincipal;
                    using (StreamReader streamReader = new StreamReader(file_detino_html_pdf, Encoding.UTF8))
                    {
                        readContentsprincipal = streamReader.ReadToEnd();
                    }


                    string _img_html = "";

                    for (Int32 i = 0; i < _foto.Length; ++i)
                    {
                        string fileorigen = _foto[i].ToString();
                        FileInfo fi_name = new FileInfo(fileorigen);
                        string num = (i + 1).ToString() + ".jpg";
                        string filedestino = ruta_foto + "\\" + num;//fi_name.Name;
                        _img_html += "<img src='file:///" + filedestino + "' width='573' height='780' border='2' />";

                        //break;
                    }
                    readContentsprincipal = readContentsprincipal.Replace("xxxx", _img_html);

                    using (var fileStream = System.IO.File.Create(@file_detino_html_pdf))
                    {
                        var texto = new UTF8Encoding(true).GetBytes(readContentsprincipal);
                        fileStream.Write(texto, 0, texto.Length);
                        fileStream.Flush();
                    }
                   ejecuta_pdf(@file_detino_html_pdf, @file_destino_pdf);
                }
            }
            catch
            {
                throw;
            }
        }

        private static void ejecuta_pdf(string file_html, string file_destination)
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

        [OutputCache(CacheProfile = "OneMinuteValidate")]
        private static bool GeneraPDF(string readContents, string _file_pdf_destino)
        {
            Boolean _valida = false;
            try
            {
               
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
                System.IO.File.WriteAllBytes(@_file_pdf_destino, pdfBytes);
                _valida = true;
            }
            catch(Exception ex)
            {
                _valida = false;
            }
            return _valida;
        }
               
        private void CopiarFolderPlantilla(string NombreCarpeta )
        {
            try
            {

                string _folder_principal_path = System.Web.HttpContext.Current.Server.MapPath("~" + Conexion.Str_RutaPlantilla);
                string _folder_defecto = Conexion.Str_FolderPlantilla;


                string _folder_path_defecto = _folder_principal_path + "\\" + _folder_defecto;
                string _folder_path_destino = _folder_principal_path + "\\" + NombreCarpeta;


                if (System.IO.File.Exists(_folder_path_destino))
                {
                    System.IO.File.Delete(_folder_path_destino);
                }

                DirectoryInfo sourceDir = new DirectoryInfo(@_folder_path_defecto);
                DirectoryInfo destinationDir = new DirectoryInfo(@_folder_path_destino);


                if (Directory.Exists(@_folder_path_defecto))
                {
                    CopyDirectory(sourceDir, destinationDir);
                }

                      
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region<METODOS>
        #endregion
        private void editar_html(string NombreCarpeta,string nroPaginas,string titulo)
        {
            try
            {
                string _folder_principal_path = System.Web.HttpContext.Current.Server.MapPath("~" + Conexion.Str_RutaPlantilla);
                string _folder_path_destino = _folder_principal_path + "\\" + NombreCarpeta;

                string _ruta_index = _folder_path_destino + "\\index.html";

                string _titulo_default = "{AQUARELLA - PERU} - " + titulo;
                string _pagina = "pages: " + nroPaginas;
                string readContentsprincipal;
                using (StreamReader streamReader = new StreamReader(_ruta_index, Encoding.UTF8))
                {
                    readContentsprincipal = streamReader.ReadToEnd();
                }
                readContentsprincipal = readContentsprincipal.Replace("xxxx", _titulo_default);
                readContentsprincipal = readContentsprincipal.Replace("pages: 0", _pagina);

                using (var fileStream = System.IO.File.Create(@_ruta_index))
                {
                    var texto = new UTF8Encoding(true).GetBytes(readContentsprincipal);
                    fileStream.Write(texto, 0, texto.Length);
                    fileStream.Flush();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            if (!destination.Exists)
            {
                destination.Create();
            }

            // Copy all files.
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                file.CopyTo(System.IO.Path.Combine(destination.FullName,
                    file.Name));
            }

            // Process subdirectories.
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                // Get destination directory.
                string destinationDir = System.IO.Path.Combine(destination.FullName, dir.Name);

                // Call CopyDirectory() recursively.
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }
                
        [OutputCache(CacheProfile = "OneMinuteValidate")]
        public ActionResult GuardarCatalogo()
        {
            var oJRespuesta = new JsonResponse();
            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                oJRespuesta.Data = -1;
                oJRespuesta.Message = "Debe Iniciar sessión.";


            }
            else
            {
                string NombreCarpeta = Post("Catologo_Carpeta");


                Ent_Catalogo catalogo = new Ent_Catalogo();
                catalogo.Catalogo_Id = Convert.ToInt32(Post("Catalogo_Id"));
                catalogo.Catalogo_Titulo = Post("Catalogo_Titulo");
                catalogo.Catalogo_Descripcion = Post("Catalogo_Descripcion");
                catalogo.Catalogo_Estado = Post("Catalogo_Estado");
                catalogo.Catologo_Orden = Post("Catologo_Orden");
                catalogo.Catalogo_strNroPag = Post("Catalogo_strNroPag");
                catalogo.Catalogo_UpdArchivo = Post("Catalogo_UpdArchivo");
                catalogo.Catologo_Carpeta = NombreCarpeta;
                catalogo.UsuCrea = _usuario.usu_login;
                catalogo.Ip = _usuario.usu_ip;
                int IdCatalogo = catalogoBL.InsertarCatalogo(catalogo);
                oJRespuesta.Data = IdCatalogo;

                if (IdCatalogo > 0)
                {
                    if (catalogo.Catalogo_UpdArchivo == "S")
                    {
                        CopiarFolderPlantilla(catalogo.Catologo_Carpeta);

                       
                        foreach (string fileName in Request.Files)
                        {

                            HttpPostedFileBase file = Request.Files[fileName];
                            string nombrelbl = fileName.Remove(0, 16);
                            string nombre = Post("Catalogo_Nombre" + nombrelbl);

                            Boolean valido = true;
                            valido = catalogoBL.GuardarCatalogoArchivo(NombreCarpeta, nombre, file);

                        }

                        
                        editar_html(catalogo.Catologo_Carpeta, catalogo.Catalogo_strNroPag, catalogo.Catalogo_Titulo);
                        //cargar_fotos(catalogo.Catologo_Carpeta);
                        crear_pdf(catalogo.Catologo_Carpeta);
                    }

                }
            }

            return Json(oJRespuesta, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ActualizarListCatalogo(string strListCatalogo)
        {

            var oJRespuesta = new JsonResponse();
            oJRespuesta.Success = true;
            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
             
                oJRespuesta.Data = -1;
                oJRespuesta.Message = "Debe Iniciar sessión.";

            }
            else
            {
                oJRespuesta.Message = "Error en la Actualización.";
                int respuesta = catalogoBL.ActualizarListCatalogo(strListCatalogo);
                oJRespuesta.Data = respuesta;
                

                if (respuesta > 0)
                    oJRespuesta.Message = "Los registros fueron actualizados.";
            }

            return Json(oJRespuesta, JsonRequestBehavior.AllowGet);
        }
        public static string Post(string campo)
        {
            
            bool existeParametro = System.Web.HttpContext.Current.Request.Form[campo] != null;
            string parametro = existeParametro ? System.Web.HttpContext.Current.Request.Form[campo].ToString().Trim() : string.Empty;
            return parametro;
        }
    }
}