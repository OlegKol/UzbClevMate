using System.Web.Mvc;
using UzClevMate.MvcLogic._Common.Controllers;
using UzClevMate.MvcLogic._Common.Extensions;

namespace UzClevMate.MvcLogic.Apps.WebApp.LanguageLogic.Controllers
{
    public class LanguageController : _BaseController
    {
        public ActionResult Set(string culture, string returnUrl = "/")
        {
            this.SetCookieValue(culture, CookieDefinitions.CultureCookieName);
            return Redirect(returnUrl);
        }
    }
}