using Microsoft.Owin.Security.OAuth;
using PucMinas.SistemaControleLogistica.Application;
using PucMinas.SistemaControleLogistica.Application.Factory;
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
            UsuarioService usuarioService = ServiceFactory.RetornarUsuarioService();

            Usuario usuario = usuarioService.RetornarUsuario(context.UserName, context.Password);

            if (usuario != null)
            {
                Guid idUsuario = usuario.Id;
                
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", idUsuario.ToString()));

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