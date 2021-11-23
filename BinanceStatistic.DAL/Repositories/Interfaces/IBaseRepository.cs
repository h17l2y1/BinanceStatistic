using System.Collections.Generic;
using System.Threading.Tasks;

namespace BinanceStatistic.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<List<TEntity>> GetAll();

        Task Create(TEntity entity);

        Task Create(IEnumerable<TEntity> collection);

        Task RemoveAsync(IEnumerable<TEntity> entities);
    }
}