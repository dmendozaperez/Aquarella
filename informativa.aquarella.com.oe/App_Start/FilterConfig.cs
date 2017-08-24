using System.Web;
using System.Web.Mvc;

namespace informativa.aquarella.com.oe
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
