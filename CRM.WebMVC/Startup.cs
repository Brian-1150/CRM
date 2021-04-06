using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRM.WebMVC.Startup))]
namespace CRM.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
