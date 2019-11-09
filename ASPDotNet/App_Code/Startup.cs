using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPDotNet.Startup))]
namespace ASPDotNet
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
