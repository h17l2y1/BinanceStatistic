using System.Collections.Generic;
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
        private readonly List<string> ids;

        public BinanceService(IBinanceClient client)
        {
            _client = client;
            ids = new List<string>
            {
                "D163BF3ECAAEE98D987CA3D1BDA9C148",
                "05F613131982C9FEA0AB39AB94FEF9FE",
            };
        }
        
        public async Task<SearchFeaturedTraderResponse> Test()
        {
            // var request11 = new OtherPositionRequest(ids[1]);
            // var qwerty = await _client.GetPositions(request11);

            // var request = new SearchFeaturedTraderRequest
            // {
            //     PeriodType = nameof(PeriodType.DAILY),
            //     SortType = nameof(SortType.PNL)
            // };
            // IEnumerable<Trader> traders = await _client.GetTraders(request);

            // IEnumerable<string> traderIds = traders.Select(s => s.EncryptedUid);
            
            // IEnumerable<OtherPositionRequest> requests = traders.Select(s => new OtherPositionRequest
            // {
            //     EncryptedUid = s.EncryptedUid,
            //     TradeType = TradeType.PERPETUAL
            // } );
            
            
            var list = new List<Position>();
            
            for (int i = 0; i < ids.Count; i++)
            {
                var request1 = new OtherPositionRequest(ids[i]);
                
                var position = await _client.SendPostRequestsTest(request1);
                list.AddRange(position);
            }
            
            
            
            return null;
        }
    }
}