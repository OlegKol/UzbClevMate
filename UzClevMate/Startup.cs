using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UzClevMate.Startup))]
namespace UzClevMate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
