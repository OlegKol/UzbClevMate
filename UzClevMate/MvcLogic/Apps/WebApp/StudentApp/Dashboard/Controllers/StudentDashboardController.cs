using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UzClevMate._Common.Extensions;
using UzClevMate.MvcLogic._Common.Controllers;
using UzClevMate.MvcLogic._Common.Extensions;

namespace UzClevMate.MvcLogic.Apps.WebApp.StudentApp.Dashboard.Controllers
{
    public class StudentDashboardController : _BaseController
    {
        public ActionResult Index()
        {
            this.SetCookieValue(CookieDefinitions.RoleCockieName, _Definitions.StudentRole);

            //var studentId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            //var viewModel = StudentDashboardViewModelManager.ScaffoldViewModel(studentId);
            //
            //ViewBag.ViewModel = JsonConvert.SerializeObject(viewModel, Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            return View("~/MvcLogic/Apps/WebApp/StudentApp/Dashboard/Views/Index.cshtml");
        }
    }
}