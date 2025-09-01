using System.ComponentModel.DataAnnotations;

namespace Exame.Shared
{
    public class UsuarioDTO
    {
        public Guid Id { get; set; }
        
        [Required]
        public string Nome { get; set; } = default!;
        
        [Required] 
        public string Endereco { get; set; } = default!;
        
        public DateTime DataNascimento { get; set; }
        
        public int Idade { get; set; }
    }
}