using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Config;
using BinanceStatistic.DAL.Entities.Interfaces;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BinanceStatistic.DAL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private ApplicationContext _context { get; set; }

        protected DbSet<TEntity> _dbSet { get; set; }

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Create(IEnumerable<TEntity> collection)
        {
            await _dbSet.AddRangeAsync(collection);
            await _context.SaveChangesAsync();
        }

        public virtual async Task RemoveAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
        
    }
}