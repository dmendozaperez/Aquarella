using System;
using informativa.aquarella.com.oe.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Data
{
    public class LoginBL
    {

        private LoginDA loginDa = new LoginDA();

        

        public Ent_Usuario get_login(string strLogin)
        {

            Ent_Usuario usuario  = new Ent_Usuario();
            try
            {

                usuario = loginDa.get_login(strLogin);

            }
            catch (Exception ex)
            {
                usuario = null;
            }

            return usuario;
        }
    }
}