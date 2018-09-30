using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DraftPS.WebUI.Startup))]
namespace DraftPS.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
