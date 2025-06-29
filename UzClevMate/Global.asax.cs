using Hangfire;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GlobalConfigurationHangfire = Hangfire.GlobalConfiguration;

namespace UzClevMate
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private BackgroundJobServer _backgroundJobServer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfigurationHangfire.Configuration.UseSqlServerStorage(ConfigurationManager.ConnectionStrings["HangFireConnectionString"].ConnectionString);

            GlobalConfigurationHangfire.Configuration.UseSerializerSettings(
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            _backgroundJobServer = new BackgroundJobServer();
            //RecurringJob.AddOrUpdate(() => LogWrtingManager.WriteLogs(), Cron.Minutely);
        }
    }
}
