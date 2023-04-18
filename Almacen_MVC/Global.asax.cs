using Almacen_MVC.App_Start;
using Almacen_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;



namespace Almacen_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Establecer el tipo de Claim que es utilizado
            //para identificar al usuario de forma única
            System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier =
                System.Security.Claims.ClaimTypes.NameIdentifier;
            
        }


        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string[] ArrayRole = new string[5];
            string id;
            DataTable Dt = new DataTable();
            RolesUser rol = new RolesUser();

            if(HttpContext.Current.User!= null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var userStore = new UserStore<IdentityUser>();
                    var userManager = new UserManager<IdentityUser>(userStore);
                    id = userManager.FindByName(User.Identity.Name).Id;
                    Dt = rol.ObtenerRoles(id);
                    for(int i = 0; i < Dt.Rows.Count; ++i)
                    {
                        ArrayRole[i] = Dt.Rows[i]["ClaimValue"].ToString();

                    }

                    GenericIdentity myIdentity = new GenericIdentity(HttpContext.Current.User.Identity.Name);
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(myIdentity, ArrayRole);
                }
            }
        }
    }
}
