using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Table { get; }
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(long id);
        Task<TEntity> Add(TEntity entity);
        Task AddRange(List<TEntity> entity);
        Task Update(TEntity entity);
        Task UpdateRange(List<TEntity> entity);
        Task Remove(TEntity entity);
        Task RemoveRange(List<TEntity> entity);
        Task<IQueryable<TEntity>> TableAsNoTracking();
    }
}
