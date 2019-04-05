using PucMinas.SistemaControleLogistica.Application;
using PucMinas.SistemaControleLogistica.Application.Factory;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PucMinas.SistemaControleLogistica.WebApi.Controllers
{
    public class SolicitacaoColetaController : ApiController
    {
        [Authorize(Roles = "Transportadora")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] SolicitacaoColetaModel model)
        {
            try
            {
                SolicitacaoColeta solicitacaoColeta = RetornarSolicitacaoColeta(model);
                SolicitacaoColetaService solicitacaoColetaService = ServiceFactory.RetornarSolicitacaoColetaService();
                solicitacaoColetaService.InserirNovaSolicitacaoColeta(solicitacaoColeta);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private SolicitacaoColeta RetornarSolicitacaoColeta(SolicitacaoColetaModel model)
        {
            return new SolicitacaoColeta() {
                IdSolicitacaoTransporte = model.IdSolicitacaoTransporte,
                PlacaVeiculo = model.PlacaVeiculo,
                RegistroMotorista = model.RegistroMotorista
            };
        }
    }
}