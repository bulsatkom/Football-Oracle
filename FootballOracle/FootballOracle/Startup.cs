using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FootballOracle.Startup))]
namespace FootballOracle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
