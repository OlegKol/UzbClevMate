using System.Web.Mvc;
using UzClevMate._Common.Extensions;
using UzClevMate.MvcLogic._Common.Controllers;
using UzClevMate.MvcLogic._Common.Extensions;

namespace UzClevMate.MvcLogic.Apps.WebApp.Landing.Controllers
{
    public class HomeController : _BaseController
    {
        public ActionResult Index()
        {
            bool isLoggedIn = User != null && User.Identity.IsAuthenticated;

            if (isLoggedIn)
            {
                if (User.IsInRole(_Definitions.TeacherRole))
                {
                    this.SetCookieValue(CookieDefinitions.RoleCockieName, _Definitions.TeacherRole);
                    return RedirectToAction("Index", "TeacherDashboard");
                }
                else
                {
                    this.SetCookieValue(CookieDefinitions.RoleCockieName, _Definitions.StudentRole);
                    return RedirectToAction("Index", "StudentDashboard");
                }
            }

            return View("~/MvcLogic/Apps/WebApp/Landing/Views/Index.cshtml");
        }
    }
}