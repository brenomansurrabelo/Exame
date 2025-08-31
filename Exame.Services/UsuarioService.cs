using Exame.Domain;
using Exame.Infrastructure;
using Exame.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class UsuarioService : IService<Usuario>
{
    private readonly IUoW _uow;

    public UsuarioService(IUoW uow)
    {
        _uow = uow;
    }

    public async Task AddAsync(Usuario entity)
    {
        try
        {
            await _uow.BeginTransactionAsync();

            await _uow.Repository<Usuario>().AddAsync(entity);

            await _uow.CommitAsync();
        }
        catch
        {
            await _uow.RollbackAsync();

            throw;
        }
    }

    public IQueryable<Usuario> Find(Expression<Func<Usuario, bool>> predicate)
    {
        return _uow.Repository<Usuario>().Find(predicate);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var usuarios = await _uow.Repository<Usuario>()
            .GetAllAsync();

        return usuarios;
    }

    public async Task<Usuario?> GetByIdAsync(Guid id)
    {
        var usuario = await _uow.Repository<Usuario>()
            .Find(u => u.Id == id)
            .FirstOrDefaultAsync();

        return usuario;
    }

    public async Task Remove(Guid id)
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

    public async Task Update(Usuario entity)
    {
        try
        {
            await _uow.BeginTransactionAsync();

            _uow.Repository<Usuario>().Update(entity);

            await _uow.CommitAsync();
        }
        catch
        {
            await _uow.RollbackAsync();

            throw;
        }
    }
}