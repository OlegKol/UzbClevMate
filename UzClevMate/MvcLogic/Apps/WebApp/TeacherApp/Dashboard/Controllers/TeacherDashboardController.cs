using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UzClevMate._Common.Extensions;
using UzClevMate.MvcLogic._Common.Controllers;
using UzClevMate.MvcLogic._Common.Extensions;
using UzClevMate.MvcLogic.Apps.WebApp.TeacherApp._Common.Attributes;

namespace UzClevMate.MvcLogic.Apps.WebApp.TeacherApp.Dashboard.Controllers
{
    [TeacherAuthorize]
    public class TeacherDashboardController : _BaseController
    {
        public ActionResult Index()
        {
            this.SetCookieValue(CookieDefinitions.RoleCockieName,_Definitions.TeacherRole);

            var teacherId = User.Identity.GetUserId();
            
            //var teacher = TeacherGetManager.GetByUserIdForDashboard(teacherId);
            //
            //if (teacher == null)
            //{
            //    var appUser = UserBaseManager.GetUserById(teacherId);
            //    teacher = new Teacher()
            //    {
            //        UserId = appUser.Id,
            //        Email = appUser.Email,
            //        Name = appUser.UserRealName,
            //        TariffDescriptionId = 1
            //    };
            //    TeacherEditManager.CreateTeacher(teacher);
            //    teacher = TeacherGetManager.GetByUserIdForDashboard(teacherId);
            //}
            //
            //if (!teacher.WasOnboardingShown)
            //{
            //    TeacherEditManager.SetOnboardingAsShown(teacher.Id);
            //    ViewBag.IsFirstTime = true;
            //}
            //
            //ViewBag.ViewModel = JsonConvert.SerializeObject(DashoboardViewModelManager.ScaffordViewModel(teacher), Formatting.Indented, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            return View("~/MvcLogic/Apps/WebApp/TeacherApp/Dashboard/Views/Index.cshtml");
        }
    }
}