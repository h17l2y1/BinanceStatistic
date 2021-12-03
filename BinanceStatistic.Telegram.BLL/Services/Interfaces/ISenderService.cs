using System.Threading.Tasks;

namespace BinanceStatistic.Telegram.BLL.Services.Interfaces
{
    public interface ISenderService
    {
        Task SendMessageToUsers();
    }
}