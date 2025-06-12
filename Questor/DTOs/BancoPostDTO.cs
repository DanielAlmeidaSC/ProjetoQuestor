namespace Questor.DTOs
{
    public record BancoPostDTO
    {
        public string Nome { get; init; }
        public int CodigoBanco { get; init; }
        public double PercentualJuros { get; init; }

        public BancoPostDTO(string nome, int codigoBanco, double percentualJuros)
        {
            Nome = nome;
            CodigoBanco = codigoBanco;
            PercentualJuros = percentualJuros;
        }
    }
}
