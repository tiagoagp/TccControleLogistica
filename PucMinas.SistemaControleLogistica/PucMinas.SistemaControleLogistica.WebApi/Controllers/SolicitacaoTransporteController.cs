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
    }
}