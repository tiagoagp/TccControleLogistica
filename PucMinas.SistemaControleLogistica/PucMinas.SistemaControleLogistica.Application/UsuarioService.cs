using PucMinas.SistemaControleLogistica.Application.Interfaces;
using PucMinas.SistemaControleLogistica.Application.Utilitarios;
using PucMinas.SistemaControleLogistica.Domain.Entidades;
using PucMinas.SistemaControleLogistica.Domain.Interfaces;
using PucMinas.SistemaControleLogistica.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Application
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public Usuario RetornarUsuario(string email, string senha)
        {
            try
            {
                return this.usuarioRepository.RetornarUsuario(email, Criptografia.CriprografarSha256(senha));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Usuario RetornarUsuario(Guid id)
        {
            try
            {
                return this.usuarioRepository.RetornarUsuario(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}