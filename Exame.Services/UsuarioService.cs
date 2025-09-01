using Exame.Domain;
using Exame.Infrastructure;
using Exame.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Exame.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUoW _uow;

        public UsuarioService(IUoW uow)
        {
            _uow = uow;
        }

        public async Task<UsuarioDTO?> GetByIdAsync(Guid id)
        {
            var usuario = await _uow.Repository<Usuario>()
                .Find(u => u.Id == id)
                .FirstOrDefaultAsync();

            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Endereco = usuario.Endereco,
                DataNascimento = usuario.DataNascimento,
                Idade = usuario.CalcularIdade()
            };
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _uow.Repository<Usuario>().GetAllAsync();

            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Endereco = u.Endereco,
                DataNascimento = u.DataNascimento,
                Idade = u.CalcularIdade()
            });
        }

        public async Task<UsuarioDTO> CreateAsync(UsuarioDTO dto)
        {
            var usuario = new Usuario(
                dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
                dto.Nome,
                dto.Endereco,
                dto.DataNascimento
            );

            try
            {
                await _uow.BeginTransactionAsync();
                await _uow.Repository<Usuario>().AddAsync(usuario);
                await _uow.CommitAsync();

                return new UsuarioDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Endereco = usuario.Endereco,
                    DataNascimento = usuario.DataNascimento,
                    Idade = usuario.CalcularIdade()
                };
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        public async Task<UsuarioDTO> UpdateAsync(UsuarioDTO dto)
        {
            try
            {
                await _uow.BeginTransactionAsync();

                var usuario = await _uow.Repository<Usuario>()
                    .Find(u => u.Id == dto.Id)
                    .FirstOrDefaultAsync();

                if (usuario == null)
                    throw new ArgumentException("Usuário não encontrado");

                // Usa o método Atualizar que foi criado na entidade
                usuario.Atualizar(dto.Nome, dto.Endereco, dto.DataNascimento);

                _uow.Repository<Usuario>().Update(usuario);
                await _uow.CommitAsync();

                return new UsuarioDTO
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Endereco = usuario.Endereco,
                    DataNascimento = usuario.DataNascimento,
                    Idade = usuario.CalcularIdade()
                };
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _uow.BeginTransactionAsync();
                _uow.Repository<Usuario>().Remove(id);
                await _uow.CommitAsync();
            }
            catch
            {
                await _uow.RollbackAsync();
                throw;
            }
        }

        public IQueryable<UsuarioDTO> Find(Expression<Func<UsuarioDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task IService<UsuarioDTO>.CreateAsync(UsuarioDTO dto)
        {
            return CreateAsync(dto);
        }

        Task IService<UsuarioDTO>.UpdateAsync(UsuarioDTO dto)
        {
            return UpdateAsync(dto);
        }
    }
}