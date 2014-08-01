using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Magenic.SignalR.Startup))]
namespace Magenic.SignalR
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
