using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Domain.Filtros
{
    public class FiltroSolicitacaoTransporte
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public Guid IdCliente { get; set; }
        public StatusSolicitacao Status { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}