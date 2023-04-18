using Almacen_MVC.App_Start;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Almacen_MVC.Startup))]
namespace Almacen_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = SimpleInjectorContainer.RegisterService(app);
            ConfigureAuth(app, container);
        }
    }
}
