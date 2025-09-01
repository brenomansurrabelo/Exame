using Exame.Domain;
using Exame.Shared;

namespace Exame.Services
{
    public interface IUsuarioService : IService<Usuario, UsuarioDTO>
    {
        Task<IEnumerable<UsuarioDTO>> Find(string? nome = null, string? endereco = null);
    }
}
