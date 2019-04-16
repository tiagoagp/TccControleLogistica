using PucMinas.SistemaControleLogistica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application.Interfaces
{
    public interface ISistemaGestaoFrotaService
    {
        List<Veiculo> RetornarVeiculosDisponiveis();
        List<Motorista> RetornarMotoristasDisponiveis();
    }
}