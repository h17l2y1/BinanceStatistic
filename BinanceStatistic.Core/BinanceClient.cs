using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;
using BinanceStatistic.Core.Views.Response;

namespace BinanceStatistic.Core
{
    public class BinanceClient : IBinanceClient
    {
        private const string LeaderboardEndpoint = "/bapi/futures/v1/public/future/leaderboard/searchLeaderboard";
        private const string OtherPositionEndpoint = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";
        
        private readonly IBaseBinanceHttpClient _baseBinanceHttpClient;
        private readonly JsonSerializerOptions _options;

        public BinanceClient(IBaseBinanceHttpClient baseBinanceHttpClient)
        {
            _baseBinanceHttpClient = baseBinanceHttpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<Trader>> GetTraders(SearchFeaturedTraderRequest request)
        {
            string response = await _baseBinanceHttpClient.SendPostRequest(LeaderboardEndpoint, request);

            SearchFeaturedTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTraderResponse>(response, _options);
            IEnumerable<Trader> traders = responseModel?.Data;
            return traders;
        }
        
        public async Task<IEnumerable<Position>> GetPositions(OtherPositionRequest request)
        {
            string response = await _baseBinanceHttpClient.SendPostRequest(OtherPositionEndpoint, request);

            OtherPositionResponse responseModel = JsonSerializer.Deserialize<OtherPositionResponse>(response, _options);
            IEnumerable<Position> positions = responseModel?.Data.OtherPositionRetList;
            return positions;
        }
        
        public async Task<IEnumerable<Position>> SendPostRequestsTest(OtherPositionRequest request)
        {
            string response = await _baseBinanceHttpClient.SendPostRequestsTest(OtherPositionEndpoint, request);

            OtherPositionResponse responseModel = JsonSerializer.Deserialize<OtherPositionResponse>(response, _options);
            
            IEnumerable<Position> positions = responseModel?.Data.OtherPositionRetList;
            return positions;
        }
        
    }
}