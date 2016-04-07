using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tournamentz.Host.Startup))]
namespace Tournamentz.Host
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
