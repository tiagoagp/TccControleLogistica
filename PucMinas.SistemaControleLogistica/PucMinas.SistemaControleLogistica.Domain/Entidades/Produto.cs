using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Domain.Entidades
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string DescricaoProduto { get; set; }
        public double Quantidade { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public Guid SolicitacaoId { get; set; }
    }
}