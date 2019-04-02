using PucMinas.SistemaControleLogistica.ControleColetaSite.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.Models
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}