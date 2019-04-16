using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using PucMinas.SistemaControleLogistica.Domain.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Domain.Interfaces
{
    public interface ISolicitacaoTransporteRepository
    {
        void InserirNovaSolicitacao(SolicitacaoTransporte entidade);
        void EditarSolicitacao(SolicitacaoTransporte entidade);
        void AlterarStatusSolicitacaoSolicitacao(SolicitacaoTransporte entidade);
        int RetornarProximoCodigoControle();
        SolicitacaoTransporte RetornarSolicitacaoPorId(Guid id);
        int RetornarTotalSolicitacoes(FiltroSolicitacaoTransporte filtrosConsulta);
        List<SolicitacaoTransporte> RetornarSolicitacoes(FiltroSolicitacaoTransporte filtrosConsulta);
    }
}