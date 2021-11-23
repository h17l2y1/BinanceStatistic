using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceClient
    {
        Task<IEnumerable<BinanceTrader>> GetTraders(SearchFeaturedTraderRequest request);
        
        Task<IEnumerable<BinanceTrader>> GetTraders(SearchLeaderboardRequest request);

        Task<IEnumerable<BinancePosition>> GetPositions(OtherPositionRequest request);

        Task<IEnumerable<BinanceTopTrader>> GetTopTraders(SearchFeaturedTopTraderRequest request);
    }
}