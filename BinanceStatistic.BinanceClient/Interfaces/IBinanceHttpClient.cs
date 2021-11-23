using System.Net.Http;
using System.Threading.Tasks;

namespace BinanceStatistic.BinanceClient.Interfaces
{
    public interface IBinanceHttpClient
    {
        Task<string> SendMultiPostRequests<T>(string url, T request);
    }
}