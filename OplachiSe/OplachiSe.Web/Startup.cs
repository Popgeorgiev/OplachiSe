using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OplachiSe.Web.Startup))]
namespace OplachiSe.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
