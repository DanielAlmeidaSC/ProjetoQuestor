using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Entities;

namespace Questor.Endpoints
{
    public class TodosBancosGet
    {
        public static string Caminho => "/bancos";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Comportamento => Action;

        public static async Task<IResult> Action(ApplicationDbContext db)
        {
            List<BancoDTO> bancos = await db.Banco.Select(b => new BancoDTO(
                b.Id,
                b.CodigoBanco,
                b.Nome,
                b.PercentualJuros

                ))
                .ToListAsync();

            if (bancos.Count == 0)
            {
                return Results.NotFound("Não há bancos cadastrados no sistema!");
            }

            return Results.Ok(bancos);

        }

        public record BancoDTO(int Id, int CodigoBanco, string Nome, double PercentualJuros);




    }
}
