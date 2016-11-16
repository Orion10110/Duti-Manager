using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiplomWeb.Startup))]
namespace DiplomWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
