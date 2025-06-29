using System.Web;
using System.Web.Mvc;
using UzClevMate.BL.LocalizationLogic.Attributes;
using UzClevMate.BL.LogManagement.Filters;

namespace UzClevMate
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CultureAttribute());
            filters.Add(new LoggingFilter());
        }
    }
}
