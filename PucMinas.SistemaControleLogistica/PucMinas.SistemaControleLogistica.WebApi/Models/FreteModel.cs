using PucMinas.SistemaControleLogistica.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.WebApi.Models
{
    public class FreteModel
    {
        public string CidadeDestino { get; set; }
        public string UfDestino { get; set; }
        public List<ProdutoModel> Produtos { get; set; }
    }
}