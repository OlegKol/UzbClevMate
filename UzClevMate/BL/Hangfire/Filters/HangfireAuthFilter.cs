using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Owin;
using System.Diagnostics;
using UzClevMate._Common.Extensions;

namespace UzClevMate.BL.Hangfire.Filters
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            if (Debugger.IsAttached)
            {
                return true;
            }

            var owinContext = new OwinContext(context.GetOwinEnvironment());

            var isAuthenticated = owinContext.Authentication.User?.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated)
            {
                return false;
            }

            var isAdmin = owinContext.Authentication.User.Identity.Name.IsAdmin();

            if (isAdmin || Debugger.IsAttached)
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