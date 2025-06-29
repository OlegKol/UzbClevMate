using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Web.Routing;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.LogManagement.Managers;
using UzClevMate.BL.LogManagement.Models;
using UzClevMate.MvcLogic._Common.Controllers;

namespace UzClevMate.BL.LogManagement.Filters
{
    public class LoggingFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            var controller = filterContext.Controller as _BaseController;
            if (controller.LogRecord == null)
            {
                controller.LogRecord = new LogRecord();
            }

            AddContextData(filterContext.RequestContext, controller);

            controller.LogRecord.AddExceptionToLog(filterContext.Exception.ToString());
            controller.LogRecord.AddLogToDb();

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        message = "Error occurred"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/MvcLogic/ErrorHandling/Views/Error.cshtml"
                };
            }

            filterContext.ExceptionHandled = true;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //record to log
            var controller = filterContext.Controller as _BaseController;

            if (controller.LogRecord != null) //log is not empty
            {
                if (controller.LogRecord.LogStrings.HasElements())
                {
                    AddContextData(filterContext.RequestContext, controller);
                }

                controller.LogRecord.AddLogToDb();
            }

            base.OnActionExecuted(filterContext);
        }

        private static void AddContextData(RequestContext requestContext, _BaseController controller)
        {
            bool isLoggedIn = System.Web.HttpContext.Current.User != null &&
                System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (isLoggedIn)
            {
                var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                controller.LogRecord.AddDataToLog($"userId: {userId}");
            }

            controller.LogRecord.AddDataToLog($"" +
                $"{requestContext.RouteData.Values["controller"].ToString()}/" +
                $"{requestContext.RouteData.Values["action"].ToString()}");
        }
    }
}