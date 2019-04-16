using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using PucMinas.SistemaControleLogistica.Domain.Filtros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application.Interfaces
{
    public interface ISolicitacaoTransporteService
    {
        Guid CriarSolicitacao(SolicitacaoTransporte entidade);
        List<SolicitacaoTransporte> RetornarSolicitacoes(FiltroSolicitacaoTransporte filtrosConsulta);
        int RetornarTotalSolicitacoes(FiltroSolicitacaoTransporte filtrosConsulta);
        SolicitacaoTransporte RetornarSolicitacaoPorId(Guid id);
    }
}