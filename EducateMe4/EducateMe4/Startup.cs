using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EducateMe4.Startup))]
namespace EducateMe4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
