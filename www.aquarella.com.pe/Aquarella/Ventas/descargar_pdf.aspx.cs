using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
using System.Net;
using www.aquarella.com.pe.UserControl;
namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class descargar_pdf : System.Web.UI.Page
    {
        string _nodoc;
        string _rutapdf;
        Users _user;
        string _nombreSession = "rutapdf";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
                else
                    _user = (Users)Session[Constants.NameSessionUser];
                if (!IsPostBack)
                {
                    Session[_nombreSession] = null;
                }

                if (Request.Params["numero"] != null && Request.Params["rutapdf"] != null)
                {
                    _nodoc = Request.Params["numero"].ToString();
                    _rutapdf = Request.Params["rutapdf"].ToString();
                    NetworkShare.ConnectToShare(@"\\10.10.10.30\FileRepositorySS", "Administrator", "adminBata.");
                    //System.IO.FileInfo file = new System.IO.FileInfo(@_rutapdf);
                    //if ((file.Exists))
                    //{
                    Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + _nodoc + ".pdf");
                    ////Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = "Application/pdf";
                    ////Response.TransmitFile(Server.MapPath(_rutapdf));
                    //Response.WriteFile(_rutapdf);

                    string pdfPath = _rutapdf;
                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(@pdfPath);
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + _nodoc + ".pdf");
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", buffer.Length.ToString());
                    Response.BinaryWrite(buffer);
                    Response.End();
                    Response.Close();
                        //file = null;
                    //}

                    //Set the appropriate ContentType.
                    //Response.ContentType = "Application/pdf";
                    ////Get the physical path to the file.
                    //string FilePath = MapPath(_rutapdf + ".pdf");
                    ////Write the file directly to the HTTP content output stream.
                    //Response.WriteFile(FilePath);
                    //Response.End();

                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.ContentType = "application/pdf";
                    ////Response.Clear();

                    ////Response.ContentType = "application/pdf";
                    //HttpContext.Current.Response.AddHeader(
                    // "content-disposition", string.Format("attachment; filename={0}", _nodoc + ".pdf"));
                    //HttpContext.Current.Response.End();
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + _nodoc + ".pdf");
                    //Response.TransmitFile(_rutapdf);
                    //Response.End();
                }

            }
            catch (Exception ex)
            {
                this.msnMessage.LoadMessage(ex.Message, ucMessage.MessageType.Error);
                //throw new Exception(msgErr.Message, msgErr.InnerException);
                //Response.Redirect("panelReturns.aspx");
            }
        }
    }
}