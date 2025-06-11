using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Entities;
using System.Drawing;

namespace Questor.Endpoints
{
    public class BoletoPost
    {
        public static string Caminho => "/boletos";
        public static string[] Metodo => new string[] { HttpMethod.Post.ToString() };
        public static Func<BoletoDTO, ApplicationDbContext, Task<IResult>> Comportamento => Acao;

        public static async Task<IResult> Acao(BoletoDTO boletoDTO, ApplicationDbContext db)
        {
            Boleto boleto = new Boleto
            {
                NomePagador = boletoDTO.NomePagador,
                NomeBeneficiario = boletoDTO.NomeBeneficiario,
                CpfCnpjPagador = boletoDTO.CpfCnpjPagador,
                CpfCnpjBeneficiario = boletoDTO.CpfCnpjBeneficiario,
                Valor = boletoDTO.Valor,
                DataVencimento = boletoDTO.DataVencimento,
                Observacao = boletoDTO.Observacao,
                BancoId = boletoDTO.BancoId

            };

            boleto.Validar();

            if (!boleto.IsValid)
            {
                var erro = boleto.Notifications.GroupBy(b => b.Key)
                    .ToDictionary(n => n.Key,
                    b => b.Select(n => n.Message).ToArray());

                return Results.ValidationProblem(erro);
            }

            var bancoExiste = await db.Banco.AnyAsync(b => b.Id == boleto.BancoId);

            if (!bancoExiste)
            {
                return Results.NotFound("Instituição bancária não encontrada!");
            }

            boleto.DataVencimento = DateTime.SpecifyKind(boleto.DataVencimento, DateTimeKind.Utc);

            await db.Boleto.AddAsync(boleto);
            await db.SaveChangesAsync();

            return Results.Created($"/boletos/{boleto.Id}", boleto.Id);
        }
        public record BoletoDTO(string NomePagador, string NomeBeneficiario, string CpfCnpjPagador, string CpfCnpjBeneficiario, double Valor, DateTime DataVencimento, string Observacao, int BancoId);
    }
}
