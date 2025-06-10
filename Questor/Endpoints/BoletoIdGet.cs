using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Entities;
using System.Drawing;

namespace Questor.Endpoints
{
    public class BoletoIdGet
    {
        public static string Caminho => "/boletos/{id}";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Comportamento => Action;

        public static async Task<IResult> Action(int id, ApplicationDbContext db)
        {
            BoletoDTO boleto = await db.Boleto.Where(b => b.Id == id)
                .Select(b => new BoletoDTO(
                    b.NomePagador,
                    b.CpfCnpjPagador,
                    b.NomeBeneficiario,
                    b.CpfCnpjBeneficiario,
                    b.Valor,
                    b.DataVencimento,
                    b.Observacao,
                    b.BancoId
                ))
                .FirstOrDefaultAsync();

            if (boleto == null)
            {
                return Results.NotFound("Boleto não encontrado!");
            }

            DateTime hoje = DateTime.UtcNow.Date;

            if (hoje > boleto.DataVencimento.Date)
            {
                Banco banco = await db.Banco
                    .Where(b => b.Id == boleto.BancoId)
                    .FirstAsync();

                double juros = banco.PercentualJuros / 100;

                BoletoDTO boletoAtualizado = boleto with { Valor = boleto.Valor * (1 + juros) };

                return Results.Ok(boletoAtualizado);
            }

            return Results.Ok(boleto);

        }

        public record BoletoDTO(string NomePagador, string CpfCnpjPagador, string NomeBeneficiario, string CpfCnpjBeneficiario, double Valor, DateTime DataVencimento, string Observacao, int BancoId);

    }
}
