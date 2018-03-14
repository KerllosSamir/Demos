using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestAnglr.Startup))]
namespace TestAnglr
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
