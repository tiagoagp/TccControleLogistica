using PucMinas.SistemaControleLogistica.Application.Interfaces;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Interfaces;
using PucMinas.SistemaControleLogistica.ExternalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application
{
    public class SistemaGestaoFrotaService : ISistemaGestaoFrotaService
    {
        private ISistemaGestaoFrotaExternalService sistemaGestaoFrotaExternalService;

        public SistemaGestaoFrotaService(ISistemaGestaoFrotaExternalService sistemaGestaoFrotaExternalService)
        {
            this.sistemaGestaoFrotaExternalService = sistemaGestaoFrotaExternalService;
        }

        public List<Veiculo> RetornarVeiculosDisponiveis()
        {
            try
            {
                return this.sistemaGestaoFrotaExternalService.RetornarVeiculosDisponiveis();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Motorista> RetornarMotoristasDisponiveis()
        {
            try
            {
                return this.sistemaGestaoFrotaExternalService.RetornarMotoristasDisponiveis();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}