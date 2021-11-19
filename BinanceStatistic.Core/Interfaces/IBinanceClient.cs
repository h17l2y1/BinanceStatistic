using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;
using BinanceStatistic.Core.Views.Response;

namespace BinanceStatistic.Core.Interfaces
{
    public interface IBinanceClient
    {
        Task<IEnumerable<Trader>> GetTraders(SearchFeaturedTraderRequest request);
        // Task<IEnumerable<Position>> GetOtherPosition(OtherPositionRequest request);
        Task<IEnumerable<OtherPositionResponse>> GetUsersInParallelInWithBatches(IEnumerable<OtherPositionRequest> requests);
    }
}