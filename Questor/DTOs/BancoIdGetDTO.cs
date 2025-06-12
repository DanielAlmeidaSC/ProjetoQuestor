namespace Questor.DTOs
{
    public record BancoIdGetDTO
    {
        public int Id { get; init; }
        public string Nome { get; init; }
        public int CodigoBanco { get; init; }
        public double PercentualJuros { get; init; }

        public BancoIdGetDTO(int id, string nome, int codigoBanco, double percentualJuros)
        {
            Id = id;
            Nome = nome;
            CodigoBanco = codigoBanco;
            PercentualJuros = percentualJuros;
        }
    }
}
