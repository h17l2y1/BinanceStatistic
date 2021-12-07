using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Models;

namespace BinanceStatistic.Telegram.BLL.Services.Interfaces
{
    public interface ISenderService
    {
        Task SendMessageToUsers(GetStatisticRequest request);
    }
}