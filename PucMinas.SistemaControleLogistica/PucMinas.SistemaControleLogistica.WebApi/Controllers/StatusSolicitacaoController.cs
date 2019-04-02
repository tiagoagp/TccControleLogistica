using PucMinas.SistemaControleLogistica.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PucMinas.SistemaControleLogistica.WebApi.Controllers
{
    public class StatusSolicitacaoController : ApiController
    {
        [HttpPost]
        public IHttpActionResult AtualizarStatus(AtualizacaoStatusSolicitacaoModel model)
        {
            return Ok();
        }
    }
}