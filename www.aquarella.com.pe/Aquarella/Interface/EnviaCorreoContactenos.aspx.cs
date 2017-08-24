using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll.Interfaces;
//using Bata.Aquarella.Pe.BLL.Interfaces;

namespace www.aquarella.com.pe.Aquarella.Interface
{
    public partial class EnviaCorreoContactenos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
                 string VNombre, VApellido, VTelefono, VEmail, VComentario;

                 VNombre = Request.QueryString["nombre"].ToString();
                 VApellido =Request.QueryString["apellido"].ToString();
                 VTelefono = Request.QueryString["telefono"].ToString();
                 VEmail =  Request.QueryString["email"].ToString();
                 VComentario = Request.QueryString["comentario"].ToString();


                 Contactenos.enviar_correo_contactenos(VNombre, VApellido, VTelefono, VEmail, VComentario);
        //         string path = MapPath("../../Design/templateMail.htm");
        //         string vdetalle = "<b>Nombres: </b>" + VNombre + "<br /><b>Apellidos: </b>" + VApellido + "<br /><b>Telefono: </b>" + VTelefono + "<br /><b>Email: </b>" + VEmail + "<br /><b>Comentario: </b>" + VComentario;   

        //         DataTable dt = EnvioCorreos.Get_Correo_Envio_admin().Tables[0];


        //         if (dt.Rows.Count > 0)
        //         {
        //             string destinatario = "";
        //             for (Int32 i = 0; i < dt.Rows.Count; i++ )
        //             {
        //                 if (i == 0)
        //                 {
        //                     destinatario = dt.Rows[i]["email"].ToString();
        //                 }
        //                 else
        //                 {
        //                     destinatario += "," + dt.Rows[i]["email"].ToString();
        //                 }
                       
        //             }
        //             Utilities.sendInstitutionalMessage(destinatario, "Contactenos [AQUARELLA]",
        //              "Estimado usuario, este es un correo de contactenos de la pagina www.aquarellaperu.com.pe; a continuación se detalla la información:",
        //              vdetalle, path);
                        

        //         }









        }


    }
}