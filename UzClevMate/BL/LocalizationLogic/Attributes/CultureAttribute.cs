using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using UzClevMate._Common.Extensions;
using UzClevMate.MvcLogic._Common.Extensions;

namespace UzClevMate.BL.LocalizationLogic.Attributes
{
    public class CultureAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var cultureName = _Definitions.DefaultCulture;

            var cultureCookie = request.Cookies[CookieDefinitions.CultureCookieName];
            if (cultureCookie != null)
            {
                cultureName = cultureCookie.Value;
            }

            var cultureInfo = new CultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            base.OnActionExecuting(filterContext);
        }
    }
}