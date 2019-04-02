using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.ExternalService.Model
{
    public class FaturamentoModel
    {
        public FaturamentoModel()
        {
            this.CnpjCliente = string.Empty;
        }

        public string CnpjCliente { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataEntrega { get; set; }
    }
}