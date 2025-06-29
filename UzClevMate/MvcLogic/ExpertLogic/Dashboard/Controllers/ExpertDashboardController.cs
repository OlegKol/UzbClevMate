using System.Web.Mvc;
using UzClevMate.MvcLogic._Common.Controllers;
using UzClevMate.MvcLogic.ExpertLogic.Attributes;

namespace UzClevMate.MvcLogic.ExpertLogic.Dashboard.Controllers
{
    [ExpertAuthorize]
    public class ExpertDashboardController : _BaseController
    {
        public ActionResult Index()
        {
            return View("~/MvcLogic/ExpertLogic/Dashboard/Views/Index.cshtml");
        }
    }
}