using System.Threading.Tasks;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceHttpClient
    {
        Task<string> SendPostRequest<T>(string url, T request);
        Task<string> SendPostRequestsTest<T>(string url, T request);
    }
}