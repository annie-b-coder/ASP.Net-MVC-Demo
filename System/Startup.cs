using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(System.Startup))]
namespace System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
