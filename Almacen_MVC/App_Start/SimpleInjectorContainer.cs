using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using Almacen_MVC.AlmacenMVC.DAL;
using Almacen_MVC.Contratos;
using Almacen_MVC.Controllers;
using Almacen_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;



namespace Almacen_MVC.App_Start
{
    public class SimpleInjectorContainer
    {
        public static Container RegisterService(IAppBuilder app)
        {
            var container = GetInitializeContainer(app);

            container.Register<IProducto, ProductoDAL>(Lifestyle.Scoped);
            container.Register<ConsultasController>(Lifestyle.Scoped);
            container.Register<HomeController>(Lifestyle.Scoped);
            container.Register<IVentas, VentasDAL>(Lifestyle.Scoped);
            container.Register<VentasController>(Lifestyle.Scoped);
            container.Register<IAgregar, AgregarDAL>(Lifestyle.Scoped);
            container.Register<IAdmin, AdminDAL>(Lifestyle.Scoped);
            container.Register<AdminController>(Lifestyle.Scoped);
                
            container.RegisterMvcIntegratedFilterProvider();
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }
        public static Container GetInitializeContainer(IAppBuilder app)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            //container.Register<IAppBuilder>();
            //container.RegisterPerWebRequest<ApplicationUserManager>();

            container.Register<ApplicationDbContext>(
                ()=> new ApplicationDbContext("DefaultConnection"),
                Lifestyle.Scoped);

            container.Register<IUserStore<ApplicationUser>>(
               () => new UserStore<ApplicationUser>(
                   container.GetInstance<ApplicationDbContext>()),
               Lifestyle.Scoped);

            container.Register<ApplicationUserManager>(Lifestyle.Scoped);

            container.RegisterInitializer<ApplicationUserManager>(
                manager => InitializeUserManager(manager, app));

            container.Register<AccountController>(Lifestyle.Scoped);

            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            container.Register<IAuthenticationManager>(() => container.IsVerifying
            ? new OwinContext(new Dictionary<string, object>()).Authentication
            : HttpContext.Current.GetOwinContext().Authentication,
            Lifestyle.Scoped);

            return container;
        }
        
        private static void InitializeUserManager(
            ApplicationUserManager manager, IAppBuilder app)
        {
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true

            };

            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            var dataProtectionProvider = app.GetDataProtectionProvider();

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(
                        dataProtectionProvider.Create("ASP.NET Identity"));
            }

        }

    }
}