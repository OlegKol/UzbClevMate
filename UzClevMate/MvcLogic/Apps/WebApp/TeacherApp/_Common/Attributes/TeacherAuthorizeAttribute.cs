using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using UzClevMate._Common.Extensions;
using UzClevMate.BL.UzClevMateUsers.Teachers.Managers;

namespace UzClevMate.MvcLogic.Apps.WebApp.TeacherApp._Common.Attributes
{
    public class TeacherAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }

            var isAuthenticated = httpContext.User != null &&
                httpContext.User.Identity.IsAuthenticated;

            if (!isAuthenticated)
            {
                return false;
            }

            if (httpContext.User.IsInRole(_Definitions.TeacherRole))
            {
                if (!httpContext.Request.IsAjaxRequest())
                {
                    var userId = httpContext.User.Identity.GetUserId();
                    TeacherEditManager.SetLastLogin(userId);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}