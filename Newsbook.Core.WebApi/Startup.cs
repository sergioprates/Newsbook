using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newsbook.Core.WebApi.Security;
using Newsbook.Dependencias;
using Owin;
using SimpleInjector.Integration.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SimpleInjector;
using Newsbook.Core.WebApi.Controllers;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.WebApi;

namespace Newsbook.Core.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            var container = RegistradorDependencias.GetContainer(new WebRequestLifestyle());
            container.RegisterWebApiControllers(config);

            Registration registration = container.GetRegistration(typeof(FeedUrlController)).Registration;

            registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Teste");

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            WebApiConfig.Register(config);
            
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}