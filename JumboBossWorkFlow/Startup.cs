using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JumboBossWorkFlow.Startup))]
namespace JumboBossWorkFlow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
