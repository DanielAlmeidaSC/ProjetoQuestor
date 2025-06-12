namespace CadastroProdutos.DTOs
{
    public class ProdutoReadDTO
    {
        public int Id { get; init; }
        public string Nome { get; init; }
        public decimal Preco { get; init; }
        public int QuantidadeEmEstoque { get; init; }
        public DateTime DataCadastro { get; init; }

        public ProdutoReadDTO(int id, string nome, decimal preco, int quantidadeEmEstoque)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
            QuantidadeEmEstoque = quantidadeEmEstoque;
        }
    }
}
