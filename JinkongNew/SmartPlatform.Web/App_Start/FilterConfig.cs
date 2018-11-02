using System.Web;
using System.Web.Mvc;

namespace SuperGPS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SuperGPS.App_Start.NoCacheAttribute());
            //filters.Add(new SuperGPS.App_Start.LogAttribute());  
        }
    }
}
