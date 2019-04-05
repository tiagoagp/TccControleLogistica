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
    public class ListaSolicitacoesController : ApiController
    {
        [Authorize(Roles = "Cliente, Transportadora")]
        [HttpGet]
        public IHttpActionResult Get([FromUri] string dataInicial, [FromUri] string dataFinal, [FromUri] Guid idCliente)
        {
            try
            {
                DateTime dataIni;
                DateTime.TryParse(dataInicial, out dataIni);

                DateTime dataFim;
                DateTime.TryParse(dataFinal, out dataFim);

                SolicitacaoTransporteService solicitacaoService = ServiceFactory.RetornarSolicitacaoTransporteService();
                List<SolicitacaoTransporte> lista = solicitacaoService.RetornarSolicitacoes(dataIni, dataFim, idCliente);
                List<SolicitacaoTransporteModel> listaModel = RetornarListaModelSolicitacoes(lista);

                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private List<SolicitacaoTransporteModel> RetornarListaModelSolicitacoes(List<SolicitacaoTransporte> lista)
        {
            List<SolicitacaoTransporteModel> listaModel = new List<SolicitacaoTransporteModel>();

            foreach (SolicitacaoTransporte solic in lista)
            {
                listaModel.Add(new SolicitacaoTransporteModel()
                {
                    BairroDestino = solic.BairroDestino,
                    CepDestino = solic.CepDestino,
                    ChaveAcessoNF = solic.ChaveAcessoNF,
                    CidadeDestino = solic.CidadeDestino,
                    CodigoControle = solic.CodigoControle,
                    ComplementoDestino = solic.ComplementoDestino,
                    DataEmissaoNF = solic.DataEmissaoNF,
                    DataEntrega = solic.DataEntrega,
                    DataEntregaTexto = solic.DataEntrega.ToString("dd/MM/yyyy"),
                    EmailRecebedor = solic.EmailRecebedor,
                    EstadoDestino = solic.EstadoDestino,
                    Id = solic.Id,
                    NomeRecebedor = solic.NomeRecebedor,
                    NumeroDestino = solic.NumeroDestino,
                    NumeroNF = solic.NumeroNF,
                    PontoReferenciaEntrega = solic.PontoReferenciaEntrega,
                    RuaDestino = solic.RuaDestino,
                    SerieNF = solic.SerieNF,
                    Status = solic.Status,
                    TelefoneRecebedor = solic.TelefoneRecebedor,
                    ValorFrete = solic.ValorFrete
                });
            }

            return listaModel;
        }
    }
}