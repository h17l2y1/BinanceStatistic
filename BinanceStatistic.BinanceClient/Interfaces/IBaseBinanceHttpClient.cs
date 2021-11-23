using System.Threading.Tasks;

namespace BinanceStatistic.BinanceClient.Interfaces
{
    public interface IRequestSender
    {
        Task<string> SendPostRequest<T>(string url, T request);
    }
}