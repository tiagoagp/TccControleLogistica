using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.ExternalService.Model
{
    public class VeiculoModel
    {
        public VeiculoModel()
        {
            this.Modelo = string.Empty;
            this.Placa = string.Empty;
        }

        public Guid Id { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }
}