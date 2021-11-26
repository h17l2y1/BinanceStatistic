using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Views.Request;
using BinanceStatistic.BinanceClient.Views.Response;

namespace BinanceStatistic.BinanceClient
{
    public class Client : IBinanceClient
    {
        private const string LeaderboardEndpoint = "/bapi/futures/v1/public/future/leaderboard/searchLeaderboard";
        private const string LeaderboardRankEndpoint = "/bapi/futures/v2/public/future/leaderboard/getLeaderboardRank";
        private const string OtherPositionEndpoint = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";
        private const string GetAllCurrencies = "/fapi/v1/exchangeInfo?showall=true";

        private readonly IBinanceHttpClient _binanceHttpClient;
        private readonly JsonSerializerOptions _options;

        public Client(IBinanceHttpClient binanceHttpClient)
        {
            _binanceHttpClient = binanceHttpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<BinanceTrader>> GetTraders(SearchFeaturedTraderRequest request)
        {
            string response = await _binanceHttpClient.SendMultiPostRequests(LeaderboardEndpoint, request);
            SearchFeaturedTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTraderResponse>(response, _options);
            IEnumerable<BinanceTrader> traders = responseModel?.Data;
            return traders;
        }
        
        public async Task<IEnumerable<BinanceTrader>> GetTraders(SearchLeaderboardRequest request)
        {
            string response = await _binanceHttpClient.SendMultiPostRequests(LeaderboardEndpoint, request);
            SearchFeaturedTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTraderResponse>(response, _options);
            IEnumerable<BinanceTrader> traders = responseModel?.Data;
            return traders;
        }
        
        public async Task<IEnumerable<BinanceTopTrader>> GetTopTraders(SearchFeaturedTopTraderRequest request)
        {
            string response = await _binanceHttpClient.SendMultiPostRequests(LeaderboardRankEndpoint, request);
            SearchFeaturedTopTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTopTraderResponse>(response, _options);
            IEnumerable<BinanceTopTrader> traders = responseModel?.Data;
            return traders;
        }
        
        public async Task<IEnumerable<BinanceTopTrader>> GetTopTraders2(List<BinanceRequestTemplate> requests)
        {
            // var xxx = new BinanceRequestTemplate
            
            var listTraders = new List<BinanceTopTrader>();
            for (int i = 0; i < requests.Count; i++)
            {
                string response = await _binanceHttpClient.SendMultiPostRequests2(requests[i]);
                SearchFeaturedTopTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTopTraderResponse>(response, _options);
                IEnumerable<BinanceTopTrader> traders = responseModel?.Data;
                listTraders.AddRange(traders);
            }

            return listTraders;
        }
        
        public async Task<IEnumerable<BinancePosition>> GetPositions(OtherPositionRequest request)
        {
            string response = await _binanceHttpClient.SendMultiPostRequests(OtherPositionEndpoint, request);
            OtherPositionResponse responseModel = JsonSerializer.Deserialize<OtherPositionResponse>(response, _options);
            IEnumerable<BinancePosition> positions = responseModel?.Data.OtherPositionRetList;
            return positions;
        }
        
        public async Task<IEnumerable<BinancePosition>> GetPositionsSingle(OtherPositionRequest request)
        {
            string response = await _binanceHttpClient.SendPostRequest(OtherPositionEndpoint, request);
            OtherPositionResponse responseModel = JsonSerializer.Deserialize<OtherPositionResponse>(response, _options);
            IEnumerable<BinancePosition> positions = responseModel?.Data.OtherPositionRetList;
            return positions;
        }

        public async Task<IEnumerable<BinanceCurrency>> GetCurrencies()
        {
            string response = await _binanceHttpClient.SendGetRequest(GetAllCurrencies);
            GetAllCurrencyResponse responseModel = JsonSerializer.Deserialize<GetAllCurrencyResponse>(response, _options);
            IEnumerable<BinanceCurrency> positions = responseModel?.Symbols;
            return positions;
        }
    }
}