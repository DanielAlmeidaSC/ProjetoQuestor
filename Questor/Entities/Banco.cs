﻿using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Questor.Entities
{
    public class Banco : Notifiable<Notification>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CodigoBanco { get; set; }
        public double PercentualJuros { get; set; }

        public Banco()
        {

        }
        public Banco(int id, string nome, int codigoBanco, double percentualJuros)
        {
            Id = id;
            Nome = nome;
            CodigoBanco = codigoBanco;
            PercentualJuros = percentualJuros;
        }
        public void Validar()
        {
            var contract = new Contract<Banco>()
                .Requires()
                .IsNotNullOrEmpty(Nome, "Nome", "O nome do banco é um requisito obrigatório e não pode ficar vazio ou nulo!")
                .IsGreaterThan(CodigoBanco, 0, "CodigoBanco", "O código do banco é um requisito obrigatório e deve ser maior que 0")
                .IsGreaterThan(PercentualJuros, 0, "PercentualJuros", "O percentual de juros é um requisito obrigatório e deve ser maior que 0");

            AddNotifications(contract);
        }
    }
}
