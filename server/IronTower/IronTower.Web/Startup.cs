using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IronTower.Web.Startup))]
namespace IronTower.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
