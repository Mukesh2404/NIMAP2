using NIMAP2.ValidationFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NIMAP2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }


        protected void Application_AuthenticateRequest()
        {
            var token = Request.Cookies["Bearer"]?.Value;

            if (token != null)
            {
                var Principal = TokenCreate.ValidateJwtToken(token);

                HttpContext.Current.User = Principal;

                Thread.CurrentPrincipal = Principal;

            }
        }


    }
}
