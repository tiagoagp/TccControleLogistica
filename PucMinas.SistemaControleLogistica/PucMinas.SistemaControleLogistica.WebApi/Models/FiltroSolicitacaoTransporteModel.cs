using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.WebApi.Models
{
    public class FiltroSolicitacaoTransporteModel
    {
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        public Guid IdCliente { get; set; }
        public StatusSolicitacao Status { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}