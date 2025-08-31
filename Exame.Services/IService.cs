using System.Linq.Expressions;

namespace Exame.Services
{
    public interface IService<T> where T : class
    {
        public Task<T?> GetByIdAsync(Guid id);

        public Task<IEnumerable<T>> GetAllAsync();

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        public Task AddAsync(T entity);

        public Task Update(T entity);

        public Task Remove(Guid id);
    }
}
