using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroProdutos.Entidades
{
    public class Produto : Notifiable<Notification>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public DateTime DataCadastro { get; set; }

        public Produto()
        {
            
        }

        public Produto(int id, string nome, decimal preco, int quantidadeEmEstoque)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            QuantidadeEmEstoque = quantidadeEmEstoque;
            DataCadastro = DateTime.UtcNow;

            AddNotifications(new Contract<Produto>()
                .Requires()
                .IsNotNullOrEmpty(nome, "Nome", "Nome é um requisito obrigatório e não pode ficar vazio!")
                .IsGreaterThan(preco, 0, "Preco", "Preço é um requisito obrigatório e não pode ser menor ou igual a 0")
                .IsGreaterThan(quantidadeEmEstoque, 0, "QuantidadeEmEstoque", "Quantidade em estoque é um requisito obrigatório e não pode ser menor que 0")
                
                );
        }
    }
}
