using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using PucMinas.SistemaControleLogistica.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application
{
    public class SolicitacaoColetaService
    {
        private SolicitacaoColetaRepository solicitacaoColetaRepository;
        private SolicitacaoTransporteRepository solicitacaoTransporteRepository;

        public SolicitacaoColetaService(SolicitacaoColetaRepository solicitacaoColetaRepository, SolicitacaoTransporteRepository solicitacaoTransporteRepository)
        {
            this.solicitacaoColetaRepository = solicitacaoColetaRepository;
            this.solicitacaoTransporteRepository = solicitacaoTransporteRepository;
        }

        public void InserirNovaSolicitacaoColeta(SolicitacaoColeta entidade)
        {
            try
            {
                if (string.IsNullOrEmpty(entidade.PlacaVeiculo))
                {
                    throw new ApplicationException("Informe um veículo");
                }

                if (string.IsNullOrEmpty(entidade.RegistroMotorista))
                {
                    throw new ApplicationException("Informe um motorista");
                }

                entidade.Id = Guid.NewGuid();
                this.solicitacaoColetaRepository.InserirNovaSolicitacaoColeta(entidade);

                SolicitacaoTransporte solicitacaoTransporte = this.solicitacaoTransporteRepository.RetornarSolicitacaoPorId(entidade.IdSolicitacaoTransporte);

                if (solicitacaoTransporte != null)
                {
                    solicitacaoTransporte.Status = StatusSolicitacao.ColetaEmAndamento;
                    this.solicitacaoTransporteRepository.AlterarStatusSolicitacaoSolicitacao(solicitacaoTransporte);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<SolicitacaoColeta> RetornarSolicitacoesColeta()
        {
            try
            {
                return this.solicitacaoColetaRepository.RetornarSolicitacoesColeta();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}