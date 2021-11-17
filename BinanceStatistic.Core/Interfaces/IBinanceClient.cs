using System.Threading.Tasks;
using BinanceStatistic.Core.Views.Response;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceClient
    {
        // Task<T> GetPageSource1<T>(string method, object data = null) where T : SearchFeaturedTraderResponse, new();

        Task GetPageSource2();
    }
}