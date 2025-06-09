using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Entities;

namespace Questor.Endpoints
{
    public class BancoPost
    {
        public static string Caminho => "/bancos";
        public static string[] Metodo => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Comportamento => Action;

        public static async Task<IResult> Action(Banco bancoInsert, ApplicationDbContext db)
        {
            Banco banco = new Banco
            {
                Nome = bancoInsert.Nome,
                CodigoBanco = bancoInsert.CodigoBanco,
                PercentualJuros = bancoInsert.PercentualJuros
            };
            banco.Validar();

            if (!banco.IsValid)
            {
                var erro = banco.Notifications.GroupBy(n => n.Key)
                    .ToDictionary(b => b.Key,
                    b => b.Select(n => n.Message).ToArray());

                return Results.ValidationProblem(erro);
            }

            var codigoBanco = await db.Banco.AnyAsync(b => b.Id == banco.CodigoBanco);
            if (codigoBanco)
            {
                return Results.BadRequest("Erro! Código de banco já existente dentro do banco de dados!");
            }

            await db.Banco.AddAsync(banco);
            await db.SaveChangesAsync();

            return Results.Created($"/bancos/{banco.Id}", banco.Id);
        }
    }
}
