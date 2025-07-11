﻿using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.DTOs;
using Questor.Entities;

namespace Questor.Endpoints
{
    public class IdBancoGet
    {
        public static string Caminho => "/bancos/{codigoBanco}";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Func<int, ApplicationDbContext, Task<IResult>> Comportamento => Acao;

        public static async Task<IResult> Acao(int codigoBanco, ApplicationDbContext db)
        {
            BancoGetDTO banco = await db.Banco.Where(b => b.CodigoBanco == codigoBanco)
                .Select(b => new BancoGetDTO(
                        b.Id,
                        b.Nome,
                        b.CodigoBanco,
                        b.PercentualJuros
                ))
                .FirstOrDefaultAsync();

            if (banco == null)
            {
                return Results.NotFound("Não foi encontrado nenhum banco com o código informado!");
            }
            return Results.Ok(banco);
        }
    }
}
