using System.Threading.Tasks;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBaseBinanceHttpClient
    {
        Task<string> SendPostRequest<T>(string url, T request);
    }
}