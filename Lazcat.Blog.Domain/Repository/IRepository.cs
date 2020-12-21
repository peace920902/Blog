using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lazcat.Blog.Models.Domain;

namespace Lazcat.Blog.Domain.Repository
{
    public interface IRepository<TId, T> where T : Entity<TId>
    {
        IQueryable<T> GetAll();
        Task<T> FindAsync(TId id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(TId id, T item, bool insertIfNotExisted = false);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp);
        Task<bool> DeleteAsync(TId id);
    }
}