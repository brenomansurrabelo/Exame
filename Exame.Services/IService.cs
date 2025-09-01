using System.Linq.Expressions;

namespace Exame.Services
{
    public interface IService<TEntity, TDTO> where TEntity : class
    {
        public Task<TDTO?> GetByIdAsync(Guid id);

        public Task<IEnumerable<TDTO>> GetAllAsync();

        public IQueryable<TDTO> Find(Expression<Func<TEntity, bool>> predicate);

        public Task<TDTO> CreateAsync(TDTO dto);

        public Task<TDTO> UpdateAsync(TDTO dto);

        public Task DeleteAsync(Guid id);
    }
}
