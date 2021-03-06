﻿using PucMinas.SistemaControleLogistica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Domain.Interfaces
{
    public interface ISistemaGestaoFrotaExternalService
    {
        List<Veiculo> RetornarVeiculosDisponiveis();
        List<Motorista> RetornarMotoristasDisponiveis();
    }
}