using System.Threading.Tasks;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IRequestSender
    {
        Task<string> SendPostRequest<T>(string url, T request);
    }
}