using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PucMinas.SistemaControleLogistica.ControleColetaSite.Enumeradores;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.Models
{
    public class DadosUsuarioAutenticado
    {
        public string Token { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public Guid UsuarioId { get; set; }
        public TipoUsuario Tipo { get; internal set; }
    }
}