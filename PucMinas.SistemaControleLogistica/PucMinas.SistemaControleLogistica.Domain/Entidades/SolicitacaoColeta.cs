using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Domain.Entidades
{
    public class SolicitacaoColeta
    {
        public Guid Id { get; set; }
        public Guid IdSolicitacaoTransporte { get; set; }
        public string RegistroMotorista { get; set; }
        public string PlacaVeiculo { get; set; }
    }
}