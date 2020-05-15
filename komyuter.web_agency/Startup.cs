using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(komyuter.web_agency.Startup))]
namespace komyuter.web_agency
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
