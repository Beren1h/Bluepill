using FluentValidation.Mvc;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Bluepill.Web.Framework
{
    public class BluePillWebApplication : HttpApplication
    {
        private static HttpContextBase _httpContextCurrent;

        public BluePillWebApplication()
        {
            PostAuthenticateRequest += OnAuthenticateRequest;
        }


        protected void Application_Start()
        {
            DependencyResolver.SetResolver(new IocDependencyResolver(IocInitializer.CreateContainer()));

            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new ValidatorFactory()));

            RegisterGlobalFilters();
        }

        protected void OnAuthenticateRequest(object sender, EventArgs e)
        {
            if (IsPageRequest)
            {
                var principalCreator = DependencyResolver.Current.GetService<IPrincipalService>();

                if (principalCreator != null)
                {
                    var user = principalCreator.GetPrincipal();

                    if (user != null)
                        HttpContextCurrent.User = user;
                }
            }
        }
        private void RegisterGlobalFilters()
        {
            GlobalFilters.Filters.Add(new AuthorizeAttribute());
            //GlobalFilters.Filters.Add(new RequireHttpsAttribute());
            //GlobalFilters.Filters.Add(new AllowAnonymousAttribute());
        }

        public IUnityContainer Container
        {
            get { return DependencyResolver.Current.GetService<IUnityContainer>(); }
        }

        private static bool IsPageRequest
        {
            // ignore requests for static files - css, images, js
            get { return RouteTable.Routes.GetRouteData(HttpContextCurrent) != null; }
        }

        public static HttpContextBase HttpContextCurrent
        {
            get { return _httpContextCurrent ?? new HttpContextWrapper(HttpContext.Current); }
            set { _httpContextCurrent = value; }
        }
    }
}