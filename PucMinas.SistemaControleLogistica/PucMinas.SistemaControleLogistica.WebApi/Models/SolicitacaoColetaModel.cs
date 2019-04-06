using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.WebApi.Models
{
    public class SolicitacaoColetaModel
    {
        public Guid Id { get; set; }
        public Guid IdSolicitacaoTransporte { get; set; }
        public string RegistroMotorista { get; set; }
        public string PlacaVeiculo { get; set; }
        public double CodigoControleSolicitacao { get; set; }
        public StatusSolicitacao StatusSolicitacao { get; set; }
    }
}