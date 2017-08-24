using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.aquarella.com.pe.Bll.Util
{
    public class SingleTon
    {
        public static void ClearCache()
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetExpires(DateTime.Now);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();
        }
    }
}