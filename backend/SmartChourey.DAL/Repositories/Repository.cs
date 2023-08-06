using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartChourey.DAL.Context;
using SmartChourey.DAL.Repositories.Interfaces;

namespace SmartChourey.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public DbSet<T> Table => _dbSet;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> Get(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> Add(T item)
        {
            var entityEntry = _dbSet.Add(item);
            await Save();

            return entityEntry.Entity;
        }

        public async Task AddRange(List<T> items)
        {
            _dbSet.AddRange(items);
            await Save();
        }

        public async Task Remove(T item)
        {
            _dbSet.Remove(item);
            await Save();
        }
        public async Task RemoveRange(List<T> items)
        {
            _dbSet.RemoveRange(items);
            await Save();
        }

        private async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(T item)
        {
            _dbContext.Update(item);
            await Save();
        }

        public async Task UpdateRange(List<T> items)
        {
            _dbContext.UpdateRange(items);
            await Save();
        }

        public async Task<IQueryable<T>> TableAsNoTracking()
        {
            return _dbSet.AsNoTracking();
        }
    }
}
