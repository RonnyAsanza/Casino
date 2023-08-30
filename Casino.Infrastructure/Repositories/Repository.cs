using Casino.Domain.Interfaces.Repositories;
using Casino.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Casino.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        //protected
        private readonly CasinoDbContext _context;

        public Repository(CasinoDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByPredicateAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity != null;
        }

        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>().FindAsync(predicate);
            return entity != null;
        }
    }
}
