using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.Core.Enums;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;
using BinanceStatistic.Core.Views.Response;

namespace BinanceStatistic.BLL.Services
{
    public class BinanceService: IBinanceService
    {
        private readonly IBinanceClient _client;

        public BinanceService(IBinanceClient client)
        {
            _client = client;
        }
        
        public async Task<SearchFeaturedTraderResponse> Test()
        {
            var request = new SearchFeaturedTraderRequest
            {
                PeriodType = nameof(PeriodType.DAILY),
                SortType = nameof(SortType.PNL)
            };
            IEnumerable<Trader> traders = await _client.GetTraders(request);

            // IEnumerable<string> traderIds = traders.Select(s => s.EncryptedUid);
            
            // IEnumerable<OtherPositionRequest> requests = traders.Select(s => new OtherPositionRequest
            // {
            //     EncryptedUid = s.EncryptedUid,
            //     TradeType = TradeType.PERPETUAL
            // } );
            //
            // var position = await _client.GetUsersInParallelInWithBatches(requests);
            
            return null;
        }
    }
}