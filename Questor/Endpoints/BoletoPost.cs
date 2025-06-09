using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Entities;

namespace Questor.Endpoints
{
    public class BoletoPost
    {
        public static string Caminho => "/boletos";
        public static string[] Metodo => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Comportamento => Action;

        public static async Task<IResult> Action(Boleto boletoInsert, ApplicationDbContext db)
        {
            Boleto boleto = new Boleto
            {
                NomePagador = boletoInsert.NomePagador,
                NomeBeneficiario = boletoInsert.NomeBeneficiario,
                CpfCnpjPagador = boletoInsert.CpfCnpjPagador,
                CpfCnpjBeneficiario = boletoInsert.CpfCnpjBeneficiario,
                Valor = boletoInsert.Valor,
                DataVencimento = boletoInsert.DataVencimento,
                Observacao = boletoInsert.Observacao,
                BancoId = boletoInsert.BancoId
                
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
    }
}
