using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GanGao.MVC.Web.Startup))]
namespace GanGao.MVC.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
