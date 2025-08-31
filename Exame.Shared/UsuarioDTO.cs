namespace Exame.Shared
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Endereco { get; set; } = default!;
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }
    }
}
