using Newsbook.Core.WebApi.AutoMapper;
using Newsbook.Core.WebApi.Controllers;
using Newsbook.Dependencias;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Newsbook.Core.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = RegistradorDependencias.GetContainer(new WebRequestLifestyle());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            Registration registration = container.GetRegistration(typeof(FeedUrlController)).Registration;

            registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Teste");

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutoMapperConfig.RegisterMappings();
        }
    }
}
