using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Coter1.Startup))]
namespace Coter1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
