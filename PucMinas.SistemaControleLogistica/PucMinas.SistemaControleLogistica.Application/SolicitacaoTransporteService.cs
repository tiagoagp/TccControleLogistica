using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application
{
    public class SolicitacaoTransporteService
    {
        private readonly SolicitacaoTransporteRepository solicitacaoTransporteRepository;

        public SolicitacaoTransporteService(SolicitacaoTransporteRepository solicitacaoTransporteRepository)
        {
            this.solicitacaoTransporteRepository = solicitacaoTransporteRepository;
        }

        public void CriarSolicitacao(SolicitacaoTransporte entidade)
        {
            try
            {
                entidade.Id = Guid.NewGuid();
                entidade.CodigoControle = this.solicitacaoTransporteRepository.RetornarProximoCodigoControle();

                foreach (Produto prod in entidade.Produtos)
                {
                    prod.Id = Guid.NewGuid();
                    prod.SolicitacaoId = entidade.Id;
                }

                this.solicitacaoTransporteRepository.InserirNovaSolicitacao(entidade);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void VincularVeiculoEmSolicitacao()
        {
            // Vincular veiculo na solicitação
        }

        public void FaturarSolicitacao(SolicitacaoTransporte solicitacao)
        {
            // solicitar faturamento da solicitação
        }
    }
}