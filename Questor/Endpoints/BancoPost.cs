using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.DTOs;
using Questor.Entities;

namespace Questor.Endpoints
{
    public class BancoPost
    {
        public static string Caminho => "/bancos";
        public static string[] Metodo => new string[] { HttpMethod.Post.ToString() };
        public static Func<BancoPostDTO, ApplicationDbContext, Task<IResult>> Comportamento => Acao;

        public static async Task<IResult> Acao (BancoPostDTO bancoDTO, ApplicationDbContext db)
        {
            Banco banco = new Banco
            {
                Nome = bancoDTO.Nome,
                CodigoBanco = bancoDTO.CodigoBanco,
                PercentualJuros = bancoDTO.PercentualJuros
            };
            banco.Validar();

            if (!banco.IsValid)
            {
                var erro = banco.Notifications.GroupBy(n => n.Key)
                    .ToDictionary(b => b.Key,
                    b => b.Select(n => n.Message).ToArray());

                return Results.ValidationProblem(erro);
            }

            var codigoBanco = await db.Banco.AnyAsync(b => b.Id == banco.Id);
            if (codigoBanco)
            {
                return Results.BadRequest("Erro! Código de banco já existente dentro do banco de dados!");
            }

            if (banco.PercentualJuros > 100)
            {
                return Results.BadRequest("A taxa de juros não pode ser maior que 100%! Revise as políticas pré estabelecidas!");
            }
            await db.Banco.AddAsync(banco);
            await db.SaveChangesAsync();

            return Results.Created($"/bancos/{banco.Id}", banco.Id);
        }

    }
}
