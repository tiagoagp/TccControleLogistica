using PucMinas.SistemaControleLogistica.Application;
using PucMinas.SistemaControleLogistica.Application.Factory;
using PucMinas.SistemaControleLogistica.Application.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using PucMinas.SistemaControleLogistica.Domain.Filtros;
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
        [HttpPost]
        public IHttpActionResult Post([FromBody] FiltroSolicitacaoTransporteModel filtros)
        {
            try
            {
                FiltroSolicitacaoTransporte filtrosConsulta = RetornarFiltrosConsulta(filtros);
                ISolicitacaoTransporteService solicitacaoService = ServiceFactory.RetornarSolicitacaoTransporteService();
                List<SolicitacaoTransporte> lista = solicitacaoService.RetornarSolicitacoes(filtrosConsulta);
                List<SolicitacaoTransporteModel> listaModel = RetornarListaModelSolicitacoes(lista);

                int quantidade = solicitacaoService.RetornarTotalSolicitacoes(filtrosConsulta);

                ListaSolicitacaoTransporteModel listaComQuantidade = new ListaSolicitacaoTransporteModel()
                {
                    ListaModel = listaModel,
                    Quantidade = quantidade
                };

                return Ok(listaComQuantidade);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private FiltroSolicitacaoTransporte RetornarFiltrosConsulta(FiltroSolicitacaoTransporteModel filtros)
        {
            DateTime dataIni;
            DateTime.TryParse(filtros.DataInicial, out dataIni);

            DateTime dataFim;
            DateTime.TryParse(filtros.DataFinal, out dataFim);

            return new FiltroSolicitacaoTransporte()
            {
                DataInicial = dataIni,
                DataFinal = dataFim,
                IdCliente = filtros.IdCliente,
                Status = filtros.Status,
                Limit = filtros.Limit,
                Offset = filtros.Offset
            };
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
                    ValorFrete = solic.ValorFrete,
                    Usuario = new UsuarioModel()
                    {
                        Id = solic.Usuario.Id,
                        Email = solic.Usuario.Email,
                        NomeUsuario = solic.Usuario.NomeUsuario,
                        Tipo = solic.Usuario.Tipo,
                    }
                });
            }

            return listaModel;
        }
    }
}