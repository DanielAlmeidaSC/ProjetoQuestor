using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Questor.Entities
{
    public class Boleto : Notifiable<Notification>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NomePagador { get; set; }
        public string CpfCnpjPagador { get; set; }
        public string NomeBeneficiario { get; set; }
        public string CpfCnpjBeneficiario { get; set; }

        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Observacao { get; set; }
        public int BancoId { get; set; }
        public Banco Banco { get; set; }

        public Boleto()
        {

        }
        public Boleto(string nomePagador, string cpfCnpjPagador, string nomeBeneficiario, string cpfCnpjBeneficiario, double valor, string dataVencimento, string observacao, int bancoId)
        {
            NomePagador = nomePagador;
            CpfCnpjPagador = cpfCnpjPagador;
            NomeBeneficiario = nomeBeneficiario;
            CpfCnpjBeneficiario = cpfCnpjBeneficiario;
            Valor = valor;
            DataVencimento = DateTime.ParseExact(dataVencimento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Observacao = observacao;
            BancoId = bancoId;

            if (!DateTime.TryParseExact(dataVencimento, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                AddNotification("DataVencimento", "A data de vencimento está em um formato inválido. Use dd/MM/yyyy");
            }
            else
            {
                DataVencimento = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
            }
        }

        public void Validar()
        {
            var contract = new Contract<Boleto>()
               .Requires()
               .IsNotNullOrEmpty(NomePagador, "NomePagador", "O nome do pagador é um requisito obrigatório e não pode ficar vazio ou nulo!")
               .IsNotNullOrEmpty(NomeBeneficiario, "NomeBeneficiario", "O nome do beneficiário é um requisito obrigatório e não pode ficar vazio ou nulo!")
               .IsNotNullOrEmpty(CpfCnpjPagador, "CPF/CNPJPagador", "O CPF/CNPJ do pagador é um requisito obrigatório e não pode ficar vazio ou nulo!")
               .IsNotNullOrEmpty(CpfCnpjBeneficiario, "CPF/CNPJBeneficiario", "O CPF/CNPJ do beneficiário é um requisito obrigatório e não pode ficar vazio ou nulo!")
               .IsGreaterThan(Valor, 0.0, "Valor", "O valor do boleto é um requisito obrigatório e deve ser maior que R$ 0,00")
               .IsGreaterThan(BancoId, 0, "BancoId", "O Id do banco é um requisito obrigatório e deve ser maior que 0");

            AddNotifications(contract);
        }
    }
}
