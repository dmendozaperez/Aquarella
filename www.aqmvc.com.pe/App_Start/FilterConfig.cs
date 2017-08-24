using System.Web;
using System.Web.Mvc;

namespace www.aqmvc.com.pe
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
