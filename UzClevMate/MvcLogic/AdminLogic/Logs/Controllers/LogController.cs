using System;
using System.Web.Mvc;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.LogManagement.Models;
using UzClevMate.MvcLogic._Common.Controllers;
using UzClevMate.MvcLogic.AdminLogic.Attributes;
using UzClevMate.MvcLogic.AdminLogic.Logs.Managers;

namespace UzClevMate.MvcLogic.AdminLogic.Logs.Controllers
{
    [AdminAuthorize]
    public class LogController : _BaseController
    {
        public ActionResult Index(string userId)
        {
            if (userId.HasValue())
            {
                ViewBag.UserId = userId;
            }
            ViewBag.Date = DateTime.Today.ToString("yyyy-MM-dd");
            return View("~/MvcLogic/AdminLogic/Logs/Views/Index.cshtml");
        }

        [HttpPost]
        public ActionResult Get(
            string userId, string dateStartStr, string dateEndStr, bool onlyErrors,
            int dateStartHour, int dateEndHour, int methodName)
        {
            var dateStart = DateTime.ParseExact(dateStartStr,
                "yyyy-MM-dd", System.Globalization.CultureInfo.DefaultThreadCurrentCulture);

            var dateEnd = dateEndStr.HasValue()
                ? DateTime.ParseExact(dateEndStr,
                  "yyyy-MM-dd", System.Globalization.CultureInfo.DefaultThreadCurrentCulture)
                : DateTime.Now;

            var logs = LogGettingManager.GetLogs(userId, dateStart, dateEnd, onlyErrors, (LogMethodNameEnum)methodName);

            return Json(new { logs });
        }
    }
}