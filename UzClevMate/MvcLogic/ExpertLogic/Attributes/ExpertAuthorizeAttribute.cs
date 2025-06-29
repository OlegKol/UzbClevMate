using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using UzClevMate._Common.Extensions;

namespace UzClevMate.MvcLogic.ExpertLogic.Attributes
{
    public class ExpertAuthorizeAttribute : AuthorizeAttribute
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

            if (httpContext.User.Identity.Name.IsExpert() || Debugger.IsAttached)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}