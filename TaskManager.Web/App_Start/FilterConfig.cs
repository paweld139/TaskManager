using PDWebCore.Filters.MVC;
using System.Web.Mvc;

namespace TaskManager.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogExceptionFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
