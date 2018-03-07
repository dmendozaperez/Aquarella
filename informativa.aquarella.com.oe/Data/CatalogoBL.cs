using System;
using System.Collections.Generic;
using informativa.aquarella.com.oe.Models;
using System.Linq;
using System.Web;
using System.IO;

namespace informativa.aquarella.com.oe.Data
{
    public class CatalogoBL
    {

        private CatalogoDA catalogoDA = new CatalogoDA();
        private String gstrRuta = Conexion.Str_RutaCatalogo;

        public Ent_Catalogo GetCatalogo(string strId)
        {

            Ent_Catalogo catalogo = new Ent_Catalogo();
            try
            {

                catalogo = catalogoDA.GetCatalogo(strId);

            }
            catch (Exception ex)
            {
                catalogo = null;
            }

            return catalogo;
        }

        public List<Ent_Catalogo> get_listaCatalogo()
        {

            List<Ent_Catalogo> listCatalogo = new List<Ent_Catalogo>();
            try
            {

                listCatalogo = catalogoDA.get_listaCatalogo();

            }
            catch (Exception ex)
            {
                listCatalogo = null;
            }

            return listCatalogo;
        }

        public List<Ent_Catalogo> listarCatalogoMantenedor()
        {

            List<Ent_Catalogo> listCatalogo = new List<Ent_Catalogo>();
            try
            {

                listCatalogo = catalogoDA.listarCatalogoMantenedor();

            }
            catch (Exception ex)
            {
                listCatalogo = null;
            }

            return listCatalogo;
        }

        public int InsertarCatalogo(Ent_Catalogo catalogo)
        {

            int ICatalogo = 0;
            try
            {
                string strRuta = gstrRuta;
                catalogo.Catologo_strRuta = strRuta;
                ICatalogo = catalogoDA.InsertarCatalogo(catalogo);

            }
            catch (Exception ex)
            {
                ICatalogo = -1;
            }

            return ICatalogo;
        }

        public int ActualizarListCatalogo(string strListCatalogo)
        {

            int IdRespuesta = 0;
            try
            {


                IdRespuesta = catalogoDA.ActualizarListCatalogo(strListCatalogo);

            }
            catch (Exception ex)
            {
                IdRespuesta = -1;
            }

            return IdRespuesta;
        }


        public Boolean GuardarCatalogoArchivo(string nombreCarpeta,string nombreArchivo, HttpPostedFileBase archivo)
        {

            Boolean valido = true;

            try
            {
                if (archivo != null)
                {
                                        
                    MemoryStream target = new MemoryStream();
                    archivo.InputStream.CopyTo(target);
                    byte[] Archivo_Imagen = target.ToArray();
                    string strRuta = gstrRuta + nombreCarpeta+ "/pages/";
                    string cFolder = HttpContext.Current.Server.MapPath("~/" + strRuta);
                    File.WriteAllBytes(string.Format("{0}{1}", cFolder, nombreArchivo), Archivo_Imagen);

                }
            }
            catch (Exception ex)
            {
                valido = false;
            }


            return valido;
        }
    }
}