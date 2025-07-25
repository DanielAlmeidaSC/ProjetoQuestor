﻿using Microsoft.EntityFrameworkCore;
using Questor.Database;
using Questor.DTOs;
using Questor.Entities;
using System.Drawing;

namespace Questor.Endpoints
{
    public class BoletoIdGet
    {
        public static string Caminho => "/boletos/{id}";
        public static string[] Metodo => new string[] { HttpMethod.Get.ToString() };
        public static Func<int, ApplicationDbContext, Task<IResult>> Comportamento => Acao;

        public static async Task<IResult> Acao(int id, ApplicationDbContext db)
        {
            BoletoGetDTO boleto = await db.Boleto.Where(b => b.Id == id)
                .Select(b => new BoletoGetDTO(
                    b.Id,
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

                BoletoGetDTO boletoAtualizado = boleto with { Valor = boleto.Valor * (1 + juros) };

                return Results.Ok(boletoAtualizado);
            }

            return Results.Ok(boleto);

        }
    }
}
