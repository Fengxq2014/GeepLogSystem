using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeepLogSystem.Startup))]
namespace GeepLogSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
