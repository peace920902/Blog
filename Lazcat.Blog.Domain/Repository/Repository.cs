using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lazcat.Blog.EntityFramework;
using Lazcat.Blog.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lazcat.Blog.Domain.Repository
{
    public class Repository<TId, T> : IRepository<TId, T> where T : Entity<TId>
    {
        private readonly BlogContext _context;
        private readonly DbSet<T> _set;

        public Repository(BlogContext context)
        {
            _context = context;
            _set = context.Set<T>();
        }

        public IQueryable<T> GetAll() => _set;

        public async Task<T> FindAsync(TId id) => await _set.FindAsync(id);

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> exp) => await _set.FirstOrDefaultAsync(exp);


        public async Task<T> CreateAsync(T item)
        {

            var entry = await _set.AddAsync(item);
            await _context.SaveChangesAsync();
            return entry.Entity;

        }

        public async Task<T> UpdateAsync(TId id, T item, bool insertIfNotExisted = false)
        {
            if (!item.Id.Equals(id)) return null;
            var entity = await FindAsync(id);
            if (entity == null)
            {
                if (insertIfNotExisted)
                    return await CreateAsync(item);
                return null;
            }
            entity = item;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            var entity = await FindAsync(id);
            if (entity == null) return false;
            var entry = _set.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}