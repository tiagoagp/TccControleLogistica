using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PucMinas.SistemaControleLogistica.Domain.Entidades
{
    public class SolicitacaoTransporte
    {
        public SolicitacaoTransporte()
        {
            this.NomeRecebedor = string.Empty;
            this.RuaDestino = string.Empty;
            this.NumeroDestino = string.Empty;
            this.ComplementoDestino = string.Empty;
            this.CepDestino = string.Empty;
            this.BairroDestino = string.Empty;
            this.CidadeDestino = string.Empty;
            this.EstadoDestino = string.Empty;
            this.TelefoneRecebedor = string.Empty;
            this.PontoReferenciaEntrega = string.Empty;
            this.NomeRecebedor = string.Empty;
            this.TelefoneRecebedor = string.Empty;
            this.NumeroNF = string.Empty;
            this.SerieNF = string.Empty;
            this.ChaveAcessoNF = string.Empty;
            this.DataEmissaoNF = DateTime.MinValue;
            this.DataEntrega = DateTime.MinValue;
            this.Status = StatusSolicitacao.Pendente;
            this.Produtos = new List<Produto>();
            this.EmailRecebedor = string.Empty;
        }

        public List<Produto> Produtos { get; set; }
        public Guid Id { get; set; }
        public string NomeRecebedor { get; set; }
        public string RuaDestino { get; set; }
        public string TelefoneRecebedor { get; set; }
        public string NumeroDestino { get; set; }
        public string ComplementoDestino { get; set; }
        public string CepDestino { get; set; }
        public string BairroDestino { get; set; }
        public string CidadeDestino { get; set; }
        public string EstadoDestino { get; set; }
        public string PontoReferenciaEntrega { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public string NumeroNF { get; set; }
        public string SerieNF { get; set; }
        public string ChaveAcessoNF { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime DataEmissaoNF { get; set; }
        public double ValorFrete { get; set; }
        public StatusSolicitacao Status { get; set; }
        public string EmailRecebedor { get; set; }
        public int CodigoControle { get; set; }
    }
}