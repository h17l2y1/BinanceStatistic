using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.DAL.Repositories.Interfaces
{
    public interface ISubscribeRepository : IBaseRepository<Subscribe>
    {
        Task<Subscribe> FindByMinutes(int minutes);
    }
}