﻿using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.ExternalService;
using PucMinas.SistemaControleLogistica.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application
{
    public class TabelaFreteService
    {
        private TabelaFreteRepository tabelaFreteRepository;
        private ApiGoogleExternalService apiGoogleExternalService;
        private OrganizacaoRepository organizacaoRepository;

        public TabelaFreteService(ApiGoogleExternalService apiGoogleExternalService, OrganizacaoRepository organizacaoRepository, TabelaFreteRepository tabelaFreteRepository)
        {
            this.apiGoogleExternalService = apiGoogleExternalService;
            this.organizacaoRepository = organizacaoRepository;
            this.tabelaFreteRepository = tabelaFreteRepository;
        }

        public double CalcularValorFrete(List<Produto> produtos, string cidadeDestino, string ufDestino)
        {
            try
            {
                double valorTotalFrete = 0;
                double valorFrete = this.tabelaFreteRepository.RetornarTabelaFrete().Valor;
                Organizacao organizacao = this.organizacaoRepository.RetornarOrganizacao();

                if (organizacao != null)
                {
                    double distancia = this.apiGoogleExternalService.RetornarDistanciaEntreDoisLocais(organizacao.Cidade, organizacao.Estado, cidadeDestino, ufDestino);

                    foreach (Produto prod in produtos)
                    {
                        double pesoCubico = (prod.Comprimento * prod.Largura * prod.Altura) / 6000;

                        double pesoConsiderar = pesoCubico;

                        if (prod.Peso > pesoCubico)
                        {
                            pesoConsiderar = prod.Peso;
                        }

                        double valorFreteParcial = pesoConsiderar * valorFrete * distancia * prod.Quantidade;
                        valorTotalFrete += valorFreteParcial;
                    }
                }

                return valorTotalFrete;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}