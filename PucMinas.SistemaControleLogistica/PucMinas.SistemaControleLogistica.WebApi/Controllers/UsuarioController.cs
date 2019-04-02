using PucMinas.SistemaControleLogistica.Application;
using PucMinas.SistemaControleLogistica.Application.Factory;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace PucMinas.SistemaControleLogistica.WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                Guid idUsuario = Guid.Empty;

                foreach (var claim in principal.Claims)
                {
                    if (claim.Type == "sub")
                    {
                        Guid.TryParse(claim.Value.ToString(), out idUsuario);
                    }
                }

                UsuarioService usuarioService = ServiceFactory.RetornarUsuarioService();
                Usuario usuario = usuarioService.RetornarUsuario(idUsuario);

                UsuarioModel usuarioModel = new UsuarioModel()
                {
                    Id = usuario.Id,
                    Email = usuario.Email,
                    NomeUsuario = usuario.NomeUsuario,
                    Tipo = usuario.Tipo
                };
                
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, usuarioModel);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, e);
            }
        }
    }
}