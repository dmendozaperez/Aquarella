using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Admonred;
namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class ConsultaRucDniReniec : System.Web.UI.Page
    {
         Persona myInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //CargarImagen();
                txtDoc.Focus();
            }
        }

        protected void btValidateDoc_Click(object sender, EventArgs e)
        {

            buscar_documento(txtDoc.Text, "");
        }

        private void buscar_documento(string doc, string _codigocaptcha)
        {
            try
            {
                lbldniruc.Text = "";
                lblnombreruc.Text = "";
                lbldireccionruc.Text = "";
                msnMessage.Visible = false;
                string ocultar_datasunat = "$('#fsSunat').hide();";
                System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "HideDivs", ocultar_datasunat, true);

                if (doc.Length != 8 && doc.Length != 11)
                {
                    msnMessage.LoadMessage("El Numero de Documento es incorrecto. por favor verifique", UserControl.ucMessage.MessageType.Error);
                    txtDoc.Focus();
                    return;
                }

                //if (_codigocaptcha.Length == 0)
                //{
                //    msnMessage.LoadMessage("Ingrese el codigo Captcha", UserControl.ucMessage.MessageType.Error);
                //    txtimagen.Focus();
                //    return;
                //}

                //Consultar_Documento myRucDni = new Consultar_Documento((doc.Length == 8) ? Microsoft.VisualBasic.Strings.Trim("10" + doc + Consultar_Documento.getDigito("10" + doc).ToString()) : doc);

                //if (string.IsNullOrEmpty(myRucDni.Error))
                //{
                //    string showDivs = "$('#fsSunat').show();";

                //    lblnombreruc.Text = Consultar_Documento.Convert_MayusMin(myRucDni.GetInfo.RazonSocial);
                //    lbldireccionruc.Text = Consultar_Documento.Convert_MayusMin(myRucDni.GetInfo.Direccion);
                //    System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "ShowDivs", showDivs, true);
                //}

                //else
                //{
                //Persona info_captcha = Session["Captcha"];
                    Persona myInfo = new Persona();//  (Persona)HttpContext.Current.Session["Captcha"];                
                if (doc.Length==8)
                { 
                    myInfo.GetInfo(txtDoc.Text, "");
                }
                else
                {
                    myInfo.GetInfo(txtDoc.Text);
                }
                HttpContext.Current.Session["Captcha"] = myInfo;
                    CaptionResul();
                    //CargarImagen();
                //}
            }
            catch
            {
            }
        }
        private  void CaptionResul()
        {
            Persona myInfo = (Persona)HttpContext.Current.Session["Captcha"];                
            try
            {
                switch (myInfo.GetResul)
                {
                    case www.aquarella.com.pe.bll.Admonred.Persona.Resul.Ok:
                        string showDivs = "$('#fsSunat').show();";
                        this.lbldniruc.Text = txtDoc.Text.ToString();
                        this.lblnombreruc.Text = Consultar_Documento.Convert_MayusMin(myInfo.ApePaterno) + ' ' + Consultar_Documento.Convert_MayusMin(myInfo.ApeMaterno) + ' ' +Consultar_Documento.Convert_MayusMin(myInfo.Nombres);
                        this.lbldireccionruc.Text = Consultar_Documento.Convert_MayusMin(myInfo.direccion);
                        System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "ShowDivs", showDivs, true);
                        break;
                    case www.aquarella.com.pe.bll.Admonred.Persona.Resul.NoResul:
                        msnMessage.LoadMessage("No existe DNI o Ruc", UserControl.ucMessage.MessageType.Error);                        
                        break;
                    case www.aquarella.com.pe.bll.Admonred.Persona.Resul.ErrorCapcha:
                        CargarImagen();                        
                        msnMessage.LoadMessage("Intente de nuevo por favor...", UserControl.ucMessage.MessageType.Error);
                        break;
                    case www.aquarella.com.pe.bll.Admonred.Persona.Resul.Error:
                        msnMessage.LoadMessage("Error Desconocido", UserControl.ucMessage.MessageType.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CargarImagen();                
                //this.txtimagen.Focus();
            }
            catch (Exception ex)
            {
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);                
            }
        }
        private void CargarImagen()
        {
            try
            {             
                HttpContext.Current.Session["Captcha"]=new Persona();
                Persona myInfo =(Persona) HttpContext.Current.Session["Captcha"];                
                Session["ImageBytes"] = myInfo.GetByteCaptcha;
                //pictureCapcha.ImageUrl = "ImageHandler.ashx";
                Session["Captcha"] = myInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}