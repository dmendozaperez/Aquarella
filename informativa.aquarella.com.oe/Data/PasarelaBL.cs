using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using informativa.aquarella.com.oe.Models;
using System.IO;

namespace informativa.aquarella.com.oe.Data
{
    public class PasarelaBL
    {

        private PasarelaDA pasarelaDA = new PasarelaDA();
        private String gstrRuta = Conexion.Str_RutaImg;

        public int InsertarPasarela(Ent_Pasarela pasarela)
        {

            int IdPasarela = 0;
            try
            {
                string strRuta = gstrRuta;
                pasarela.Pasarela_strRuta = strRuta;
                IdPasarela = pasarelaDA.InsertarPasarela(pasarela);

            }
            catch (Exception ex)
            {
                IdPasarela = -1;
            }

            return IdPasarela;
        }

        public int EliminarPasarelaDetalle(Ent_PasarelaDetalle pasarelaDetalle)
        {

            int IdPasarelaDetalle = 0;
            
            IdPasarelaDetalle = pasarelaDA.EliminarPasarelaDetalle(pasarelaDetalle);


            return IdPasarelaDetalle;
        }

        public Ent_Pasarela GetPasarela(string strId)
        {

            Ent_Pasarela pasarela = new Ent_Pasarela();
            try
            {

                pasarela = pasarelaDA.GetPasarela(strId);

            }
            catch (Exception ex)
            {
                pasarela = null;
            }

            return pasarela;
        }

        public Boolean GuardarPasarelaArchivo(String nombre, HttpPostedFileBase archivo)
        {

            Boolean valido = true;

            try
            {
                if (archivo != null)
                    {
               
                        MemoryStream target = new MemoryStream();
                        archivo.InputStream.CopyTo(target);
                        byte[] PasarelaDet_Imagen = target.ToArray();
                        string strRuta = gstrRuta;
                        string cFolder = HttpContext.Current.Server.MapPath("~/" + strRuta);
                        File.WriteAllBytes(string.Format("{0}{1}", cFolder, nombre), PasarelaDet_Imagen);
            

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