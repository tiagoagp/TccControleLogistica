using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.Models
{
    public class ListaSolicitacaoTransporteModel
    {
        public List<SolicitacaoTransporteModel> ListaModel { get; set; }
        public int Quantidade { get; set; }
    }
}