using System.Net.Http;
using System.Threading.Tasks;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceHttpClient
    {
        Task<string> SendMultiPostRequests<T>(string url, T request);
    }
}