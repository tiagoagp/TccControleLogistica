using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.WebApi.Models
{
    public class AtualizacaoStatusSolicitacaoModel
    {
        public AtualizacaoStatusSolicitacaoModel()
        {
            this.Observacao = string.Empty;
            this.DataAtualizacaoStatus = DateTime.MinValue;
        }

        public DateTime DataAtualizacaoStatus { get; set; }
        public string Observacao { get; set; }
        public SolicitacaoTransporteModel SolicitacaoTransporte { get; set; }
    }
}