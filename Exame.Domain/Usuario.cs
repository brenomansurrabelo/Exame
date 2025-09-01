using System.ComponentModel.DataAnnotations;

namespace Exame.Domain
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; private set; }

        [Required, MaxLength(100)]
        public string Nome { get; private set; } = default!;

        [Required, MaxLength(200)]
        public string Endereco { get; private set; } = default!;

        public DateTime DataNascimento { get; private set; }

        // Construtor para EF Core
        private Usuario() { }

        public Usuario(Guid id, string nome, string endereco, DateTime dataNascimento)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
            DataNascimento = dataNascimento;
        }

        public int CalcularIdade()
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Year;
            if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        public void AtualizarEndereco(string novoEndereco)
        {
            if (string.IsNullOrWhiteSpace(novoEndereco))
                throw new ArgumentException("Endereço não pode ser vazio.");

            Endereco = novoEndereco;
        }

        public void AtualizarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new ArgumentException("Nome não pode ser vazio.");

            Nome = novoNome;
        }

        public void AtualizarDataNascimento(DateTime novaData)
        {
            if (novaData > DateTime.Today)
                throw new ArgumentException("Data de nascimento não pode ser futura.");

            DataNascimento = novaData;
        }

        public void Atualizar(string nome, string endereco, DateTime dataNascimento)
        {
            AtualizarNome(nome);
            AtualizarEndereco(endereco);
            AtualizarDataNascimento(dataNascimento);
        }
    }
}