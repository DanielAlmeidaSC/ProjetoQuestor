namespace CadastroProdutos.DTOs
{
    public record ProdutoCreateDTO
    {
        public string Nome { get; init; }
        public decimal Preco { get; init; }
        public int QuantidadeEmEstoque { get; init; }

        public ProdutoCreateDTO(string nome, decimal preco, int quantidadeEmEstoque)
        {
            Nome = nome;
            Preco = preco;
            QuantidadeEmEstoque = quantidadeEmEstoque;
        }
    }
}
