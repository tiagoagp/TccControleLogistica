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
    public class SimuladorFreteController : ApiController
    {
        [Authorize(Roles = "Cliente, Transportadora")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] FreteModel model)
        {
            try
            {
                TabelaFreteService tabelaFreteService = ServiceFactory.RetornarTabelaFreteService();

                List<Produto> produtos = RetornarProdutos(model.Produtos);

                double valorFrete = tabelaFreteService.CalcularValorFrete(produtos, model.CidadeDestino, model.UfDestino);
                
                return Ok(valorFrete);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private List<Produto> RetornarProdutos(List<ProdutoModel> produtos)
        {
            List<Produto> listaProdutos = new List<Produto>();

            foreach (ProdutoModel prod in produtos)
            {
                listaProdutos.Add(new Produto()
                {
                    Id = prod.Id,
                    Altura = prod.Altura,
                    Largura = prod.Largura,
                    Comprimento = prod.Comprimento,
                    DescricaoProduto = prod.DescricaoProduto,
                    Peso = prod.Peso,
                    Quantidade = prod.Quantidade
                });
            }

            return listaProdutos;
        }
    }
}