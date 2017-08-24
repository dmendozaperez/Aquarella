using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Interfaces;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Interfaces;


namespace www.aquarella.com.pe.Aquarella.Interface
{
    public partial class expClientes : System.Web.UI.Page
    {
        Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
               // Session[_nameSessionData] = null;

                if ((_user._usu_tip_id == "02"))
                {
                    Utilities.logout(Page.Session, Page.Response);                    
                }
                else
                {
                    this.formForEmployee();
                }
                //if (_user._usv_employee)
                //    this.formForEmployee();
                //else
                //    Utilities.logout(Page.Session, Page.Response);
            }
        }

        protected void formForEmployee()
        {
            if (!string.IsNullOrEmpty(_user._usv_warehouse))
            {
                if (!string.IsNullOrEmpty(_user._usv_area))
                {
                    //WareAreaForm.wareAreaForm(_user._usv_co, _user._usv_region);
                    //WareAreaForm.setFormByUser(_user);
                    //WareAreaForm.unableArea();
                }
              
            }
           

            // Enlazar datoS
            //refreshGridView();
        }


        protected void btConsult_Click(object sender, EventArgs e)
        {
          
        }

        protected void ibExportDoc_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //DataSet _interf =  Bata.Aquarella.Pe.BLL.Interfaces.ExpClientesBL.Get_ClientesRel_Lider(_user._usv_co, DateTime.Parse(txtDateStart.Text), DateTime.Parse(txtDateEnd.Text));
                DataSet _ds = www.aquarella.com.pe.bll.Interfaces.ExpClientesBL.Get_ClientesRel_Lider();

                System.Text.StringBuilder str = new System.Text.StringBuilder();

                for (int i = 0; i <= _ds.Tables[0].Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= _ds.Tables[0].Columns.Count - 1; j++)
                    {
                        str.Append(_ds.Tables[0].Rows[i][j].ToString() + "\t");
                    }

                    str.Append("\r\n");

                }


                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment;filename=ClientesAquarella.txt");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.Default;

                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                Response.Write(str.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                string msgError;
                msgError = ex.Message;
            }
        }
    }
}