using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Views.Request;

namespace BinanceStatistic.BinanceClient.Interfaces
{
    public interface IBinanceClient
    {
        Task<IEnumerable<BinanceTrader>> GetTraders(SearchFeaturedTraderRequest request);
        
        Task<IEnumerable<BinanceTrader>> GetTraders(SearchLeaderboardRequest request);

        Task<IEnumerable<BinancePosition>> GetPositions(OtherPositionRequest request);

        Task<IEnumerable<BinanceTopTrader>> GetTopTraders(SearchFeaturedTopTraderRequest request);
    }
}