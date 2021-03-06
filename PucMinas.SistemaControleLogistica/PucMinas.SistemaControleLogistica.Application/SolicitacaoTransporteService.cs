﻿using PucMinas.SistemaControleLogistica.Application.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using PucMinas.SistemaControleLogistica.Domain.Filtros;
using PucMinas.SistemaControleLogistica.Domain.Interfaces;
using PucMinas.SistemaControleLogistica.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application
{
    public class SolicitacaoTransporteService : ISolicitacaoTransporteService
    {
        private readonly ISolicitacaoTransporteRepository solicitacaoTransporteRepository;
        private readonly ITabelaFreteService tabelaFreteService;

        public SolicitacaoTransporteService(ISolicitacaoTransporteRepository solicitacaoTransporteRepository,
                                            ITabelaFreteService tabelaFreteService)
        {
            this.solicitacaoTransporteRepository = solicitacaoTransporteRepository;
            this.tabelaFreteService = tabelaFreteService;
        }

        public Guid CriarSolicitacao(SolicitacaoTransporte entidade)
        {
            try
            {
                entidade.UsuarioId = entidade.Usuario.Id;

                if (entidade.Id == Guid.Empty)
                {
                    entidade.Id = Guid.NewGuid();
                    entidade.CodigoControle = this.solicitacaoTransporteRepository.RetornarProximoCodigoControle();
                    entidade.ValorFrete = this.tabelaFreteService.CalcularValorFrete(entidade.Produtos, entidade.CidadeDestino, entidade.EstadoDestino);
                    
                    foreach (Produto prod in entidade.Produtos)
                    {
                        prod.Id = Guid.NewGuid();
                        prod.SolicitacaoId = entidade.Id;
                    }

                    this.solicitacaoTransporteRepository.InserirNovaSolicitacao(entidade);
                }
                else
                {
                    SolicitacaoTransporte solicitacaoGravada = this.solicitacaoTransporteRepository.RetornarSolicitacaoPorId(entidade.Id);

                    entidade.CodigoControle = solicitacaoGravada.CodigoControle;

                    foreach (Produto prod in entidade.Produtos)
                    {
                        prod.Id = Guid.NewGuid();
                        prod.SolicitacaoId = entidade.Id;
                    }

                    this.solicitacaoTransporteRepository.EditarSolicitacao(entidade);
                }

                return entidade.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SolicitacaoTransporte> RetornarSolicitacoes(FiltroSolicitacaoTransporte filtrosConsulta)
        {
            try
            {
                return this.solicitacaoTransporteRepository.RetornarSolicitacoes(filtrosConsulta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int RetornarTotalSolicitacoes(FiltroSolicitacaoTransporte filtrosConsulta)
        {
            try
            {
                return this.solicitacaoTransporteRepository.RetornarTotalSolicitacoes(filtrosConsulta);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        public SolicitacaoTransporte RetornarSolicitacaoPorId(Guid id)
        {
            try
            {
                return this.solicitacaoTransporteRepository.RetornarSolicitacaoPorId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}