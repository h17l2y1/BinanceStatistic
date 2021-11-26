using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Models;

namespace BinanceStatistic.Telegram.BLL.Helpers.Interfaces
{
    public interface ISubscribeHelper
    {
        Task<Test> CreateOrRemoveSubscribe(long telegramUserId, string subscribeType);
    }
}