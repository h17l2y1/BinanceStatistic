using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceClient
    {
        Task<IEnumerable<Trader>> GetTraders(SearchFeaturedTraderRequest request);

        Task<IEnumerable<Position>> GetPositions(OtherPositionRequest request);

        Task<IEnumerable<Position>> SendPostRequestsTest(OtherPositionRequest request);
    }
}