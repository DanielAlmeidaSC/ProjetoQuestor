using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Entities;

namespace Questor.Endpoints
{
    public class TodosBoletosGet
    {
        public static string Caminho => "/boletos";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Func<ApplicationDbContext, Task<IResult>> Comportamento => Acao;

        public static async Task<IResult> Acao(ApplicationDbContext db)
        {
            List<BoletoDTO> boleto = await db.Boleto.Select(b => new BoletoDTO(
                b.Id,
                b.NomePagador,
                b.CpfCnpjPagador,
                b.NomeBeneficiario,
                b.CpfCnpjBeneficiario,
                b.Valor,
                b.DataVencimento,
                b.Observacao,
                b.BancoId                
                )).ToListAsync();

            if (boleto.Count == 0)
            {
                return Results.NotFound("Nenhum boleto encontrado!");
            }

            return Results.Ok(boleto);
        }

        public record BoletoDTO(int Id, string NomePagador, string CpfCnpjPagador, string NomeBeneficiario, string CpfCnpjBeneficiario, double Valor, DateTime DataVencimento, string Observacao, int BancoId);


    }
}
