using System.Threading.Tasks;
using BinanceStatistic.Core.Views.Request;
using BinanceStatistic.Core.Views.Response;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceClient
    {
        Task<SearchFeaturedTraderResponse> SearchFeaturedTrader(SearchFeaturedTraderRequest request);
    }
}