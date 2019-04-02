using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PucMinas.SistemaControleLogistica.WebApi.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(PucMinas.SistemaControleLogistica.WebApi.App_Start.Startup))]

namespace PucMinas.SistemaControleLogistica.WebApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // configuracao WebApi
            var config = new HttpConfiguration();

            // configurando rotas
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                  name: "DefaultApi",
                  routeTemplate: "api/{controller}/{id}",
                  defaults: new { id = RouteParameter.Optional }
             );

            // ativando cors
            app.UseCors(CorsOptions.AllowAll);

            // ativando a geração do token
            AtivarGeracaoTokenAcesso(app);

            // ativando configuração WebApi
            app.UseWebApi(config);
        }

        private void AtivarGeracaoTokenAcesso(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(4),
                Provider = new ProviderAccessToken()
            };
            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}