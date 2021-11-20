using System.Threading.Tasks;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceConcurrentRequestsHttpClient
    {
        // Task<string> SendPostRequestsTest<T>(string url, T request);
        Task<string> SendPostRequestsTest<T>(T request);
    }
}