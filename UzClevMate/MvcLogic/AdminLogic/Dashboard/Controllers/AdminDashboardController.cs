using System.Web.Mvc;
using UzClevMate.MvcLogic._Common.Controllers;
using UzClevMate.MvcLogic.AdminLogic.Attributes;

namespace UzClevMate.MvcLogic.AdminLogic.Dashboard.Controllers
{
    [AdminAuthorize]
    public class AdminDashboardController : _BaseController
    {
        public ActionResult Index()
        {
            return View("~/MvcLogic/AdminLogic/Dashboard/Views/Index.cshtml");
        }
    }
}