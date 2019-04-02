using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(PucMinas.SistemaControleLogistica.ControleColetaSite.App_Start.Startup))]

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Login/Index")
            });
        }
    }
}