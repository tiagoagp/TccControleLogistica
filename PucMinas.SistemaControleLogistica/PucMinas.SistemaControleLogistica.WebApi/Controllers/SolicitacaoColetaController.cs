using PucMinas.SistemaControleLogistica.Application;
using PucMinas.SistemaControleLogistica.Application.Factory;
using PucMinas.SistemaControleLogistica.Application.Interfaces;
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
                ISolicitacaoColetaService solicitacaoColetaService = ServiceFactory.RetornarSolicitacaoColetaService();
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
        
        [Authorize(Roles = "Transportadora")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                ISolicitacaoColetaService solicitacaoColetaService = ServiceFactory.RetornarSolicitacaoColetaService();
                List<SolicitacaoColeta> lista = solicitacaoColetaService.RetornarSolicitacoesColeta();
                List<SolicitacaoColetaModel> listaModel = RetornarListaModel(lista);

                return Ok(listaModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private List<SolicitacaoColetaModel> RetornarListaModel(List<SolicitacaoColeta> lista)
        {
            List<SolicitacaoColetaModel> listaModel = new List<SolicitacaoColetaModel>();

            foreach (SolicitacaoColeta item in lista)
            {
                listaModel.Add(new SolicitacaoColetaModel() {
                    Id = item.Id,
                    IdSolicitacaoTransporte = item.IdSolicitacaoTransporte,
                    PlacaVeiculo = item.PlacaVeiculo,
                    CodigoControleSolicitacao = item.CodigoControleSolicitacao,
                    RegistroMotorista = item.RegistroMotorista,
                    StatusSolicitacao = item.StatusSolicitacao
                });
            }

            return listaModel;
        }
    }
}