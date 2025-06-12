using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.DTOs;
using Questor.Entities;

namespace Questor.Endpoints
{
    public class TodosBancosGet
    {
        public static string Caminho => "/bancos";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Func<ApplicationDbContext, Task<IResult>> Comportamento => Acao;

        public static async Task<IResult> Acao(ApplicationDbContext db)
        {
            List<BancoIdGetDTO> bancos = await db.Banco.Select(b => new BancoIdGetDTO(
                b.Id,
                b.Nome,
                b.CodigoBanco,
                b.PercentualJuros
                ))
                .ToListAsync();

            if (bancos.Count == 0)
            {
                return Results.NotFound("Não há bancos cadastrados no sistema!");
            }

            return Results.Ok(bancos);

        }
    }
}
