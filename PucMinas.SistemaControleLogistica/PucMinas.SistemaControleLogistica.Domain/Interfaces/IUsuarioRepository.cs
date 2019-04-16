using PucMinas.SistemaControleLogistica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario RetornarUsuario(string email, string senha);
        Usuario RetornarUsuario(Guid id);
    }
}