using PucMinas.SistemaControleLogistica.ExternalService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.ExternalService
{
    public class GestaoFrotaExternalService
    {
        public VeiculoModel RetornarVeiculoPorEnderecoDataEntrega(string CEP, DateTime dataEntrega)
        {
            return new VeiculoModel();
        }

        public VeiculoModel RetornarVeiculoPorId(Guid id)
        {
            return new VeiculoModel();
        }
    }
}