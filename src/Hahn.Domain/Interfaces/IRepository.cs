using System.Linq.Expressions;

namespace Hahn.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        public Task AddAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);
        public void Update(T entity);
        public void Delete(T entity);
        public Task SaveChangesAsync();
    }
}
