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
    public class SolicitacaoTransporteController : ApiController
    {
        [Authorize(Roles = "Cliente")]
        [HttpPost]
        public IHttpActionResult Post(SolicitacaoTransporteModel model)
        {
            try
            {
                SolicitacaoTransporteService solicitacaoService = ServiceFactory.RetornarSolicitacaoTransporteService();
                SolicitacaoTransporte entidade = MapearSolicitacaoTransporte(model);
                solicitacaoService.CriarSolicitacao(entidade);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private SolicitacaoTransporte MapearSolicitacaoTransporte(SolicitacaoTransporteModel model)
        {
            SolicitacaoTransporte entidade = new SolicitacaoTransporte();

            entidade.Id = model.Id;
            entidade.NomeRecebedor = model.NomeRecebedor;
            entidade.TelefoneRecebedor = model.TelefoneRecebedor;
            entidade.RuaDestino = model.RuaDestino;
            entidade.NumeroDestino = model.NumeroDestino;
            entidade.ComplementoDestino = model.ComplementoDestino;
            entidade.CepDestino = model.CepDestino;
            entidade.BairroDestino = model.BairroDestino;
            entidade.CidadeDestino = model.CidadeDestino;
            entidade.EstadoDestino = model.EstadoDestino;
            entidade.PontoReferenciaEntrega = model.PontoReferenciaEntrega;
            entidade.DataEntrega = model.DataEntrega;
            entidade.NumeroNF = model.NumeroNF;
            entidade.SerieNF = model.SerieNF;
            entidade.ChaveAcessoNF = model.ChaveAcessoNF;
            entidade.DataEmissaoNF = model.DataEmissaoNF;
            entidade.ValorFrete = model.ValorFrete;
            entidade.Status = model.Status;
            entidade.UsuarioId = model.Usuario.Id;

            foreach (ProdutoModel item in model.Produtos)
            {
                entidade.Produtos.Add(new Produto() {
                    DescricaoProduto = item.DescricaoProduto,
                    Altura = item.Altura,
                    Comprimento = item.Comprimento,
                    Largura = item.Largura,
                    Peso = item.Peso,
                    Quantidade = item.Quantidade
                });
            }

            return entidade;
        }

        [Authorize(Roles = "Cliente, Transportadora")]
        [Route("Listar/{dataInicial}/{dataFinal}")]
        [HttpGet]
        public IHttpActionResult Listar([FromUri] DateTime dataInicial, [FromUri] DateTime dataFinal)
        {
            try
            {
                SolicitacaoTransporteService solicitacaoService = ServiceFactory.RetornarSolicitacaoTransporteService();
                List<SolicitacaoTransporte> lista = solicitacaoService.RetornarSolicitacoes(dataInicial, dataFinal);
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

        private SolicitacaoTransporteModel RetornarModelSolicitacao(SolicitacaoTransporte entidade)
        {
            SolicitacaoTransporteModel model = new SolicitacaoTransporteModel()
            {
                BairroDestino = entidade.BairroDestino,
                CepDestino = entidade.CepDestino,
                ChaveAcessoNF = entidade.ChaveAcessoNF,
                CidadeDestino = entidade.CidadeDestino,
                CodigoControle = entidade.CodigoControle,
                ComplementoDestino = entidade.ComplementoDestino,
                DataEmissaoNF = entidade.DataEmissaoNF,
                DataEntrega = entidade.DataEntrega,
                DataEntregaTexto = entidade.DataEntrega.ToString("dd/MM/yyyy"),
                EmailRecebedor = entidade.EmailRecebedor,
                EstadoDestino = entidade.EstadoDestino,
                Id = entidade.Id,
                NomeRecebedor = entidade.NomeRecebedor,
                NumeroDestino = entidade.NumeroDestino,
                NumeroNF = entidade.NumeroNF,
                PontoReferenciaEntrega = entidade.PontoReferenciaEntrega,
                RuaDestino = entidade.RuaDestino,
                SerieNF = entidade.SerieNF,
                Status = entidade.Status,
                TelefoneRecebedor = entidade.TelefoneRecebedor,
                ValorFrete = entidade.ValorFrete,
                Produtos = RetornarProdutos(entidade.Produtos)
            };
            
            return model;
        }

        private List<ProdutoModel> RetornarProdutos(List<Produto> produtos)
        {
            List<ProdutoModel> listaModel = new List<ProdutoModel>();

            foreach (Produto prod in produtos)
            {
                listaModel.Add(new ProdutoModel() {
                    Altura = prod.Altura,
                    Comprimento = prod.Comprimento,
                    DescricaoProduto = prod.DescricaoProduto,
                    Id = prod.Id,
                    Largura = prod.Largura,
                    Peso = prod.Peso,
                    Quantidade = prod.Quantidade
                });
            }

            return listaModel;
        }

        [Authorize(Roles = "Cliente, Transportadora")]
        [HttpGet]
        public IHttpActionResult Get([FromUri] Guid id)
        {
            try
            {
                SolicitacaoTransporteService solicitacaoService = ServiceFactory.RetornarSolicitacaoTransporteService();
                SolicitacaoTransporte entidade = solicitacaoService.RetornarSolicitacaoPorId(id);
                SolicitacaoTransporteModel model = RetornarModelSolicitacao(entidade);

                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}