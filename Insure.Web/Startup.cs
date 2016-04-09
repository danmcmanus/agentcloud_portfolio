using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Insure.Web.Startup))]
namespace Insure.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
