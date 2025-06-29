using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using UzClevMate.BL.UzClevMateUsers.Students.Managers;

namespace UzClevMate.MvcLogic.Apps.WebApp.StudentApp._Common.Attributes
{
    public class StudentAuthorizeAttribute : AuthorizeAttribute
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
            else
            {
                if (!httpContext.Request.IsAjaxRequest())
                {
                    var userId = httpContext.User.Identity.GetUserId();
                    StudentEditManager.SetLastLogin(userId);
                }
                return true;
            }
        }
    }
}