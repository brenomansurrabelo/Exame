namespace Exame.Infrastructure
{
    public interface IUoW : IDisposable
    {
        Repository<T> Repository<T>() where T : class;
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
