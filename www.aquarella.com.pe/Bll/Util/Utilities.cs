using System;
using System.Data;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.Common;
namespace www.aquarella.com.pe.bll.Util
{
    public class Utilities
    {
        

        public static void logout(HttpSessionState session, System.Web.HttpResponse response)
        {
            string url = FormsAuthentication.LoginUrl;
            session.Clear();
            session.Abandon();
            FormsAuthentication.SignOut();
            response.Redirect(url, true);
        }
        /// <summary>
        /// Obtener un datatable en una posicion de un dataset
        /// </summary>
        /// <param name="dtObj"></param>
        /// <param name="posTable"></param>
        /// <returns></returns>
        public static DataTable getTableFromDataset(object dtObj, int posTable)
        {
            try
            {
                DataSet ds = (DataSet)dtObj;
                return ds.Tables[posTable];
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Calcula el margen en porcentaje entre precio y costo.
        /// Para obtener el porcentaje multiplicar por 100 (*100).
        /// </summary>
        /// <param name="price"></param>
        /// <param name="cost"></param>
        /// <returns>numero decimal menor o igual que uno que representa el porcentaje.</returns>
        public static decimal margenCalc(decimal price, decimal cost)
        {
            if (price < cost)
            {
                //No multiplicado por 100.
                return Decimal.Round(((cost - price) / cost) * (-1), 4);
            }
            else if (price > cost)
            {
                return Decimal.Round(((price - cost) / price), 4);
            }
            else
            {
                return Decimal.Round(0, 2);
            }
        }

        /// <summary>
        /// Realizar un filtro linq sobre un objeto dataset
        /// </summary>
        /// <param name="dtObj">Objeto dataset</param>
        /// <param name="posTable">indice de tabla sobre la cual realizar el linq</param>
        /// <param name="f1">campo 1 de comparacion</param>
        /// <param name="f2"></param>
        /// <param name="f3"></param>
        /// <param name="fieldValue1">valor 1 de comparacion frente al campo 1</param>
        /// <param name="fieldValue2"></param>
        /// <param name="fieldValue3"></param>
        /// <param name="fieldOrder">campo de ordenacion</param>
        /// <returns></returns>
        public static DataTable getFilterObject(object dtObj, int posTable, string f1, string f2, string f3,
            string fieldValue1, string fieldValue2, string fieldValue3, string fieldOrder)
        {
            try
            {
                DataTable ds = (DataTable)dtObj;
                var result = (from x in ds.AsEnumerable()
                              where x.Field<string>(f1).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue1) ? fieldValue1.ToUpper() : string.Empty)) ||
                                  x.Field<string>(f2).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue2) ? fieldValue2.ToUpper() : string.Empty)) ||
                                  (!string.IsNullOrEmpty(Convert.ToString(x[f3])) ? Convert.ToString(x[f3]) : string.Empty).Contains(fieldValue3)
                              //orderby x.Field<string>(fieldOrder)
                              select x).CopyToDataTable();
                return result;
            }
            catch
            {
                return new DataTable();
            }
        }

        public static DataTable getFilterObject(object dtObj,string f1,string f2,string f3,
           Decimal fieldValue1, string fieldValue2, string fieldValue3)
        {
            try
            {
                DataTable ds = (DataTable)dtObj;
                var result = (from x in ds.AsEnumerable()
                              where x.Field<decimal>(f1)!=fieldValue1 &
                              (x.Field<string>(f2).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue2) ? fieldValue2.ToUpper() : string.Empty))
                              ||
                              x.Field<string>(f3).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue3) ? fieldValue3.ToUpper() : string.Empty))
                              )
                              //where x.Field<string>(f1).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue1) ? fieldValue1.ToUpper() : string.Empty)) 
                              //orderby x.Field<string>(fieldOrder)
                              select x).CopyToDataTable();
                return result;
            }
            catch
            {
                return new DataTable();
            }
        }

        public static DataTable getFilterObject(object dtObj, int posTable, string f1, string f2, string f3, string f4,string f5,
          string fieldValue1, string fieldValue2, string fieldValue3, string fieldValue4, string fieldValue5)
        {
            try
            {
                DataTable ds = (DataTable)dtObj;                
                if (fieldValue1 != "xxx")
                {
                    var result = (from x in ds.AsEnumerable()
                                  where x.Field<string>(f1).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue1) ? fieldValue1.ToUpper() : string.Empty)) &
                                      (x.Field<string>(f2).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue2) ? fieldValue2.ToUpper() : string.Empty)) ||
                                      (!string.IsNullOrEmpty(Convert.ToString(x[f3])) ? Convert.ToString(x[f3]) : string.Empty).Contains(fieldValue3) ||
                                       (!string.IsNullOrEmpty(Convert.ToString(x[f4])) ? Convert.ToString(x[f4]) : string.Empty).Contains(fieldValue4) ||
                                        (!string.IsNullOrEmpty(Convert.ToString(x[f5])) ? Convert.ToString(x[f5]) : string.Empty).Contains(fieldValue5))
                                  //x.Field<string>(f4).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue4) ? fieldValue4.ToUpper() : string.Empty))
                                  //orderby x.Field<string>(fieldOrder)
                                  select x).CopyToDataTable();
                    return result;
                }
                else
                {
                    var result = (from x in ds.AsEnumerable()
                                  where x.Field<string>(f1).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue1) ? fieldValue1.ToUpper() : string.Empty)) ||
                                      x.Field<string>(f2).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue2) ? fieldValue2.ToUpper() : string.Empty)) ||
                                      (!string.IsNullOrEmpty(Convert.ToString(x[f3])) ? Convert.ToString(x[f3]) : string.Empty).Contains(fieldValue3) ||
                                       (!string.IsNullOrEmpty(Convert.ToString(x[f4])) ? Convert.ToString(x[f4]) : string.Empty).Contains(fieldValue4) ||
                                        (!string.IsNullOrEmpty(Convert.ToString(x[f5])) ? Convert.ToString(x[f5]) : string.Empty).Contains(fieldValue5)
                                  //x.Field<string>(f4).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue4) ? fieldValue4.ToUpper() : string.Empty))
                                  //orderby x.Field<string>(fieldOrder)
                                  select x).CopyToDataTable();
                    return result;
                }
                
            }
            catch
            {
                return new DataTable();
            }
        }

        public static DataTable getFilterObject(object dtObj, int posTable, string f1, string f2, string f3,string f4,
           string fieldValue1, string fieldValue2, string fieldValue3, string fieldValue4, string fieldOrder)
        {
            try
            {
                DataTable ds = (DataTable)dtObj;
                var result = (from x in ds.AsEnumerable()
                              where x.Field<string>(f1).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue1) ? fieldValue1.ToUpper() : string.Empty)) ||
                                  x.Field<string>(f2).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue2) ? fieldValue2.ToUpper() : string.Empty)) ||
                                  (!string.IsNullOrEmpty(Convert.ToString(x[f3])) ? Convert.ToString(x[f3]) : string.Empty).Contains(fieldValue3) ||
                                   x.Field<string>(f4).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue4) ? fieldValue4.ToUpper() : string.Empty)) 
                              //orderby x.Field<string>(fieldOrder)
                              select x).CopyToDataTable();
                return result;
            }
            catch
            {
                return new DataTable();
            }
        }

        public static DataTable getFilterObject(object dtObj, int posTable, string f1, string fieldValue1, string fieldOrder)
        {
            try
            {
                DataTable ds = (DataTable)dtObj;
                var result = (from x in ds.AsEnumerable()
                              where x.Field<string>(f1).ToUpper().Contains((!string.IsNullOrEmpty(fieldValue1) ? fieldValue1.ToUpper() : string.Empty))
                              //orderby x.Field<string>(fieldOrder.ToString())
                              select x).CopyToDataTable();
                return result;
            }
            catch
            {
                return new DataTable();
            }
        }


        #region < Funciones de ayudas para paginación y ordenacion de grillas >

        /// <summary>
        /// Paginación y ordenación de la fuente de datos
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="sortField"></param>
        /// <param name="dt"></param>
        /// <param name="iPage"></param>
        public static void pageAndSort(GridView gv, ref SortDirection GridViewSortDirection, string sortField, DataTable dt, int iPage)
        {
            if (dt != null)
            {
                if (!string.IsNullOrEmpty(sortField))
                {
                    int actualPage = gv.PageIndex;

                    // Si es paginacion, permanecer con la misma direccion de ordenación
                    if (actualPage != iPage)
                    {
                        // Esta asc ordenarlo desc
                        if (GridViewSortDirection.Equals(SortDirection.Ascending))
                            gv.DataSource = dt.AsEnumerable().OrderBy(x => x.Field<object>(sortField)).CopyToDataTable();
                        else
                            gv.DataSource = dt.AsEnumerable().OrderByDescending(x => x.Field<object>(sortField)).CopyToDataTable();
                    }
                    else
                    {
                        // Esta asc ordenarlo desc
                        if (GridViewSortDirection.Equals(SortDirection.Ascending))
                        {
                            gv.DataSource = dt.AsEnumerable().OrderByDescending(x => x.Field<object>(sortField)).CopyToDataTable();
                            GridViewSortDirection = SortDirection.Descending;
                        }
                        else
                        {
                            gv.DataSource = dt.AsEnumerable().OrderBy(x => x.Field<object>(sortField)).CopyToDataTable();
                            GridViewSortDirection = SortDirection.Ascending;
                        }
                    }
                }
                else
                    gv.DataSource = dt;

                gv.PageIndex = iPage;
                gv.DataBind();
            }
        }
        #endregion

        #region < Envio de correos >

        public static void sendMessage(string destinatario, string asunto, string cuerpo)
        {
            MailMessage message = new MailMessage();
            //message.From = new MailAddress("mail@manisol.com.co");
            message.From = new MailAddress("aqsoporte@bataperu.com.pe");
            message.To.Add(destinatario);
            message.Subject = asunto;
            message.Body = cuerpo;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                smtpClient.Host = "bataperu.com.pe";
                //smtpClient.Host = "mail.manisol.com.co";
                smtpClient.EnableSsl = false;
                smtpClient.Send(message);
            }
            catch
            {
            }
        }

        public static void sendInstitutionalMessage(string destinatario, string asunto, string header, string body, string pathTemplate)
        {
            //valores de la configuracion de correo del admin
            string vemail="";
            string vpassword="";
            string vhost="";
            Int32 vpuerto=0;

            fgetconfigadmin(ref vemail, ref vpassword, ref vhost, ref vpuerto);


            //
            string HTMLTemplateMail = TemplateHTML(pathTemplate);

            HTMLTemplateMail = BindTemplateEmail(HTMLTemplateMail, header, body);

            MailMessage message = new MailMessage();
            //message.From = new MailAddress("mail@manisol.com.co");
            //message.From = new MailAddress("aqsoporte@bataperu.com.pe");
            message.From = new MailAddress(vemail);

            message.To.Add(destinatario);
            message.Subject = asunto;
            message.Body = HTMLTemplateMail;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                //smtpClient.Host = "mail.manisol.com.co";
                //smtpClient.Host = "bataperu.com.pe";
                smtpClient.Host = vhost;
                smtpClient.Port = vpuerto;
                //smtpClient.EnableSsl = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(vemail, vpassword);
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);
            }
            catch
            {
            }
        }
        //valores de la basew de datoa de la configuracion del correo del admin
        private static void fgetconfigadmin(ref string vemail, ref string vpassword,ref string vhost,ref Int32 vpuerto)
        {
            //DataTable dt;
            //try
            //{
            //    Object Results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlcommand = "CONTROL.USP_GET_CONFIGCORREOADMIN";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlcommand, Results);
            //    dt=new DataTable();
            //    dt = db.ExecuteDataSet(dbCommandWrapper).Tables[0];

            //    if (dt.Rows.Count > 0)
            //    {
            //        vemail = dt.Rows[0]["email"].ToString();
            //        vpassword = dt.Rows[0]["password"].ToString();
            //        vhost=dt.Rows[0]["host"].ToString();
            //        vpuerto = Convert.ToInt32(dt.Rows[0]["puerto"].ToString());
            //    }

            //}
            //catch(Exception exc)
            //{
            //}
        }

        public static void sendenviarcorreoliquidacion(string destinatario, string asunto, string header, string body, string pathTemplate, string vruta)
        {
            string HTMLTemplateMail = TemplateHTML(pathTemplate);

            HTMLTemplateMail = BindTemplateEmail(HTMLTemplateMail, header, body);

            //Attachment data = new Attachment(vruta);

            MailMessage message = new MailMessage();
            //message.From = new MailAddress("mail@manisol.com.co");
            message.From = new MailAddress("aqsoporte@bataperu.com.pe");
            message.To.Add(destinatario);
            message.Subject = asunto;
            message.Body = HTMLTemplateMail;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            // message.Attachments.Add(data);
            SmtpClient smtpClient = new SmtpClient();
            try
            {
                //smtpClient.Host = "mail.manisol.com.co";

                //smtpClient.Timeout = 100000;
                smtpClient.Host = "bataperu.com.pe";
                smtpClient.EnableSsl = false;
                smtpClient.Send(message);
            }
            catch
            {
            }
        }

        /// <summary>Encarga de cargar el templatehtml en un string
        /// </summary>
        /// <returns>String con el codigo html del mensaje</returns>
        public static string TemplateHTML(string path)
        {
            string HTML = null;
            try
            {
                if (System.IO.File.Exists(path))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
                    {
                        while (reader.Peek() >= 0)
                        {
                            HTML += reader.ReadLine();
                        }
                    }
                }

            }
            catch
            {
                //msnMessage.LoadMessage(error.Message, UserControl.ucMessage.MessageType.Error);
            }

            return HTML;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HTMLTemplate"></param>
        /// <param name="header"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string BindTemplateEmail(string HTMLTemplate, string header, string body)
        {
            try
            {
                HTMLTemplate = HTMLTemplate.Replace(@"#_DATE", DateTime.Now.ToString("dddd d, MMMM yyyy, HH:mm:ss"));
                HTMLTemplate = HTMLTemplate.Replace(@"#_HEADER_MAIL", header);
                HTMLTemplate = HTMLTemplate.Replace(@"#_BODY_MAIL", body);
            }
            catch (Exception)
            {
                return HTMLTemplate;
            }
            return HTMLTemplate;
        }

        #endregion

    }
    public class msgdisponible
    {
        public Int32 tpedido { get; set; }
        public Int32 tdisponible { get; set; }


    }
}