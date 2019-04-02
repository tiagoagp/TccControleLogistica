using PucMinas.SistemaControleLogistica.Domain.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.WebApi.Models
{
    public class SolicitacaoTransporteModel
    {
        public SolicitacaoTransporteModel()
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
            this.Produtos = new List<ProdutoModel>();
            this.EmailRecebedor = string.Empty;
        }

        public Guid Id { get; set; }
        public int CodigoControle { get; set; }
        public UsuarioModel Usuario { get; set; }
        public List<ProdutoModel> Produtos { get; set; }
        public string NomeRecebedor { get; set; }
        public string EmailRecebedor { get; set; }
        public string TelefoneRecebedor { get; set; }
        public string RuaDestino { get; set; }
        public string NumeroDestino { get; set; }
        public string ComplementoDestino { get; set; }
        public string CepDestino { get; set; }
        public string BairroDestino { get; set; }
        public string CidadeDestino { get; set; }
        public string EstadoDestino { get; set; }
        public string PontoReferenciaEntrega { get; set; }
        public DateTime DataEntrega { get; set; }
        public string NumeroNF { get; set; }
        public string SerieNF { get; set; }
        public string ChaveAcessoNF { get; set; }
        public DateTime DataEmissaoNF { get; set; }
        public double ValorFrete { get; set; }
        public StatusSolicitacao Status { get; set; }
    }
}