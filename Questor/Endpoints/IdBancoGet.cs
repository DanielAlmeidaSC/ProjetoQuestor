using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.Entities;

namespace Questor.Endpoints
{
    public class IdBancoGet
    {
        public static string Caminho => "/bancos/{id}";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Comportamento => Action;

        public static async Task<IResult> Action(int id, ApplicationDbContext db)
        {
            BancoIdDTO banco = await db.Banco.Where(b => b.Id == id)
                .Select(b => new BancoIdDTO(
                        b.Id,
                        b.Nome,
                        b.CodigoBanco,
                        b.PercentualJuros
                ))
                .FirstOrDefaultAsync();

            if (banco == null)
            {
                return Results.NotFound("Não foi encontrado nenhum banco com o Id informado!");
            }
            return Results.Ok(banco);
        }
        public record BancoIdDTO(int Id, string Nome, int CodigoBanco, double PercentualJuros);


    }
}
