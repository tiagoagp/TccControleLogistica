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
    public class SolicitacaoTransporteController : ApiController
    {
        [Authorize(Roles = "Cliente")]
        [HttpPost]
        public IHttpActionResult Post(SolicitacaoTransporteModel model)
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
                
                SolicitacaoTransporteService solicitacaoService = ServiceFactory.RetornarSolicitacaoTransporteService();
                SolicitacaoTransporte entidade = MapearSolicitacaoTransporte(model);

                entidade.Usuario = usuario;

                Guid id = solicitacaoService.CriarSolicitacao(entidade);

                return Ok(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
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
            entidade.EmailRecebedor = model.EmailRecebedor;

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
    }
}