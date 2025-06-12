using Questor.Entities;

namespace Questor.DTOs
{
    public record BoletoPostDTO
    {
        public string NomePagador { get; init; }
        public string CpfCnpjPagador { get; init; }
        public string NomeBeneficiario { get; init; }
        public string CpfCnpjBeneficiario { get; init; }
        public double Valor { get; init; }
        public DateTime DataVencimento { get; init; }
        public string Observacao { get; init; }
        public int BancoId { get; init; }
        
        public BoletoPostDTO(string nomePagador, string cpfCnpjPagador, string nomeBeneficiario, string cpfCnpjBeneficiario, double valor, DateTime dataVencimento, string observacao, int bancoId)
        {
            NomePagador = nomePagador;
            CpfCnpjPagador = cpfCnpjPagador;
            NomeBeneficiario = nomeBeneficiario;
            CpfCnpjBeneficiario = cpfCnpjBeneficiario;
            Valor = valor;
            DataVencimento = dataVencimento;
            Observacao = observacao;
            BancoId = bancoId;
        }
    }
}
