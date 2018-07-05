﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.Services;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
using System.Linq;
using System.Web.UI.WebControls;

namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class recoveryPassword : System.Web.UI.Page
    {
        public static string _nSlogTx = "_nSlogTx";
        Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
                TreeView treeMenu = new TreeView();
            treeMenu = (TreeView)this.Master.FindControl("MenuPrin");
            treeMenu.Visible = false;
            }
        }
        
        private  string GetUserIPAddress()
        {
            //var context = System.Web.HttpContext.Current;
            string ip = String.Empty;
            try
            {

                ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                //    ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                //else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                //    ip = context.Request.UserHostAddress;

                //if (ip == "::1")
                //    ip = "127.0.0.1";
            }
            catch
            {

            }
            return ip;
        }

        private  void GetIpAddress(ref string _host,ref string _ip)
        {
            try
            {

                //The X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a 
                //client connecting to a web server through an HTTP proxy or load balancer
                _ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(_ip))
                {
                    _ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                //_host = System.Net.Dns.GetHostName();
                //_ip = System.Net.Dns.GetHostAddresses(_host).GetValue(1).ToString();
            }
            catch
            {

            }
        }
        
        protected void btEnviar_Click(object sender, EventArgs e)
        {
            string nombre = UserName.Text;
           
            enviarCorreo(nombre);
        }

        private void enviarCorreo(string userName)
        {
            DataTable dtEnvio = Users.enviaCorreo_Password(userName,"");
            DataRow dr = dtEnvio.Rows[0];
            string Descripcion = "";
            string CodError = "";

            if (dtEnvio != null || dtEnvio.Rows.Count >= 0)
            {
                CodError = dr["COD_ERROR"].ToString();
                Descripcion = dr["DESCRIPCION_ERROR"].ToString();
            }

            if (CodError != "0")
                FailureText.Text = Descripcion;
            else {
                FailureText.Text = "";
                sussesText.Text = Descripcion;
            }
        }     

    }
}