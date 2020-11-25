using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLHTQT_Aplication.Startup))]
namespace QLHTQT_Aplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
