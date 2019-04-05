using PucMinas.SistemaControleLogistica.Domain.Entidades;
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

        public SolicitacaoColetaService(SolicitacaoColetaRepository solicitacaoColetaRepository)
        {
            this.solicitacaoColetaRepository = solicitacaoColetaRepository;
        }

        public void InserirNovaSolicitacaoColeta(SolicitacaoColeta entidade)
        {
            try
            {
                entidade.Id = Guid.NewGuid();
                this.solicitacaoColetaRepository.InserirNovaSolicitacaoColeta(entidade);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}