using System.Threading.Tasks;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceHttpClient : IBaseBinanceHttpClient
    {
        Task<string> SendMultiPostRequests<T>(string url, T request);
    }
}