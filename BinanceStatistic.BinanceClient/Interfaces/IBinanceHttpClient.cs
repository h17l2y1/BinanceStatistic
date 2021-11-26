using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Models;

namespace BinanceStatistic.BinanceClient.Interfaces
{
    public interface IBinanceHttpClient : IRequestSender
    {
        Task<string> SendMultiPostRequests<T>(string url, T request);

        Task<string> SendMultiPostRequests2(BinanceRequestTemplate request);
    }
}