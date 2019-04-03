using PucMinas.SistemaControleLogistica.ControleColetaSite.Enumeradores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PucMinas.SistemaControleLogistica.ControleColetaSite.Models
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
        }

        public Guid Id { get; set; }
        public UsuarioModel Usuario { get; set; }
        public List<ProdutoModel> Produtos { get; set; }

        [DisplayName("Nome")]
        [StringLength(255, ErrorMessage = "O nome do recebedor deve ter no máximo 255 caracteres.")]
        [Required(ErrorMessage = "O nome do recebedor deve ser informado.")]
        public string NomeRecebedor { get; set; }

        [DisplayName("E-mail")]
        [StringLength(255, ErrorMessage = "O e-mail do recebedor deve ter no máximo 255 caracteres.")]
        [Required(ErrorMessage = "O e-mail do recebedor deve ser informado.")]
        public string EmailRecebedor { get; set; }

        [DisplayName("Telefone")]
        [StringLength(20, ErrorMessage = "O telefone do recebedor deve ter no máximo 20 caracteres.")]
        [Required(ErrorMessage = "O telefone do recebedor deve ser informado.")]
        public string TelefoneRecebedor { get; set; }

        [DisplayName("Rua")]
        [StringLength(200, ErrorMessage = "A rua do endereço de destino deve ter no máximo 200 caracteres.")]
        [Required(ErrorMessage = "A rua do dendereço de destino deve ser informada.")]
        public string RuaDestino { get; set; }

        [DisplayName("Número")]
        [StringLength(40, ErrorMessage = "O número do endereço de destino deve ter no máximo 40 caracteres.")]
        [Required(ErrorMessage = "O número do endereço de destino deve ser informado.")]
        public string NumeroDestino { get; set; }

        [DisplayName("Complemento")]
        [StringLength(60, ErrorMessage = "O complemento do endereço de destino deve ter no máximo 60 caracteres.")]
        public string ComplementoDestino { get; set; }

        [DisplayName("CEP")]
        [StringLength(9, ErrorMessage = "O CEP do endereço de destino deve ter no máximo 8 caracteres.")]
        [Required(ErrorMessage = "O CEP do endereço de destino deve ser informado.")]
        public string CepDestino { get; set; }

        [DisplayName("Bairro")]
        [StringLength(50, ErrorMessage = "O bairro do endereço de destino deve ter no máximo 50 caracteres.")]
        [Required(ErrorMessage = "O bairro do endereço de destino deve ser informado.")]
        public string BairroDestino { get; set; }

        [DisplayName("Cidade")]
        [StringLength(100, ErrorMessage = "A cidade do endereço de destino deve ter no máximo 100 caracteres.")]
        [Required(ErrorMessage = "A cidade do endereço de destino deve ser informada.")]
        public string CidadeDestino { get; set; }

        [DisplayName("UF")]
        [StringLength(100, ErrorMessage = "A UF do endereço de destino deve ter no máximo 100 caracteres.")]
        [Required(ErrorMessage = "A UF do endereço de destino deve ser informada.")]
        public string EstadoDestino { get; set; }

        [DisplayName("Ponto de Referência")]
        [StringLength(255, ErrorMessage = "O ponto de referência do endereço de destino deve ter no máximo 255 caracteres.")]
        [Required(ErrorMessage = "O ponto de referência do endereço de destino deve ser informado.")]
        public string PontoReferenciaEntrega { get; set; }

        [DisplayName("Data Máxima de Entrega")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "A data máxima de entrega deve ser informada.")]
        public DateTime DataEntrega { get; set; }

        [DisplayName("Número")]
        [StringLength(100, ErrorMessage = "O número da NF deve ter no máximo 100 caracteres.")]
        [Required(ErrorMessage = "O número da NF deve ser informado.")]
        public string NumeroNF { get; set; }

        [DisplayName("Série")]
        [StringLength(20, ErrorMessage = "A série da NF deve ter no máximo 20 caracteres.")]
        [Required(ErrorMessage = "A série da NF deve ser informada.")]
        public string SerieNF { get; set; }

        [DisplayName("Chave de Acesso")]
        [StringLength(100, ErrorMessage = "A chave de acesso da NF deve ter no máximo 100 caracteres.")]
        [Required(ErrorMessage = "A chave de acesso da NF deve ser informada.")]
        public string ChaveAcessoNF { get; set; }

        [DisplayName("Data de Emissao")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "A data de emissão da NF deve ser informada.")]
        public DateTime DataEmissaoNF { get; set; }

        [DisplayName("Valor do Frete")]
        [Required(ErrorMessage = "O valor do frete deve ser simulado antes da gravação.")]
        public double ValorFrete { get; set; }

        public StatusSolicitacao Status { get; set; }
    }
}