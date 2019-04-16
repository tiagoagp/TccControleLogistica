using Microsoft.Owin.Security.OAuth;
using NLog;
using PucMinas.SistemaControleLogistica.Application;
using PucMinas.SistemaControleLogistica.Application.Factory;
using PucMinas.SistemaControleLogistica.Application.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace PucMinas.SistemaControleLogistica.WebApi.Auth
{
    public class ProviderAccessToken : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            IUsuarioService usuarioService = ServiceFactory.RetornarUsuarioService();

            Usuario usuario = usuarioService.RetornarUsuario(context.UserName, context.Password);

            if (usuario != null)
            {
                Guid idUsuario = usuario.Id;
                
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", idUsuario.ToString()));

                if (usuario.Tipo == TipoUsuario.Cliente)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Cliente"));
                }

                if (usuario.Tipo == TipoUsuario.Transportadora)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Transportadora"));
                }

                logger.Info($"Login do usuário {usuario.Email} efetuado com sucesso em {DateTime.Now}.");

                context.Validated(identity);
            }
            else
            {
                context.SetError("acesso inválido", "As credenciais do usuário estão incorretas");
                return;
            }
        }
    }
}