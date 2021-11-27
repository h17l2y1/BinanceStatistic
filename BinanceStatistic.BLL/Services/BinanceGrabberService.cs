using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Enums;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Models.Interfaces;
using BinanceStatistic.BinanceClient.Views.Request;
using BinanceStatistic.BLL.Services.Interface;
using Newtonsoft.Json;

namespace BinanceStatistic.BLL.Services
{
    public class BinanceGrabberService : IBinanceGrabberService
    {
        private const string LeaderboardEndpoint = "/bapi/futures/v1/public/future/leaderboard/searchLeaderboard";
        private const string LeaderboardRankEndpoint = "/bapi/futures/v2/public/future/leaderboard/getLeaderboardRank";
        private const string OtherPositionEndpoint = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";
        private readonly IBinanceClient _client;

        public BinanceGrabberService(IBinanceClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<BinanceCurrency>> GrabbCurrencies()
        {
            IEnumerable<BinanceCurrency> binanceCurrencies = await _client.GrabbCurrencies();
            return binanceCurrencies;
        }
        
        public async Task<List<BinancePosition>> GrabbAll()
        {
            List<BinanceRequestTemplate> tradersRequestTemplates = CreateRequestsForTraders();
            List<IBinanceTrader> traders = await _client.GrabbTraders(tradersRequestTemplates);

            List<BinanceRequestTemplate> positionRequestTemplates = CreateRequestsForPositions(traders);
            List<BinancePosition> positions = await _client.GrabbPositions(positionRequestTemplates);
            return positions;
        }

        public async Task<List<IBinanceTrader>> GrabbTraders()
        {
            List<BinanceRequestTemplate> tradersRequestTemplates = CreateRequestsForTraders();
            List<IBinanceTrader> traders = await _client.GrabbTraders(tradersRequestTemplates);
            return traders;
        }
        
        public async Task<List<BinancePosition>> GrabbPositions(List<IBinanceTrader> traders)
        {
            List<BinanceRequestTemplate> positionTequestTemplates = CreateRequestsForPositions(traders);
            List<BinancePosition> positions = await _client.GrabbPositions(positionTequestTemplates);
            return positions;
        }
        
        private List<BinanceRequestTemplate> CreateRequestsForTraders()
        {
            var requests = new List<BinanceRequestTemplate>();
            
            foreach (string statistic in Enum.GetNames(typeof(StatisticType)))
            {
                foreach (string period in Enum.GetNames(typeof(PeriodType)))
                {
                    var requestData = new SearchFeaturedTopTraderRequest(period, statistic);
                    BinanceRequestTemplate template = CreateBinanceRequestTemplate(LeaderboardRankEndpoint, requestData);
                    requests.Add(template);
                }
            }
            
            // TODO: need check this
            // foreach (string sortType in Enum.GetNames(typeof(SortType)))
            // {
            //     foreach (string periodType in Enum.GetNames(typeof(PeriodType)))
            //     {
            //         var requestData = new SearchFeaturedTraderRequest(sortType, periodType);
            //         BinanceRequestTemplate template = CreateBinanceRequestTemplate(LeaderboardEndpoint, requestData);
            //         requests.Add(template);
            //     }
            // }
            
            foreach (string sortType in Enum.GetNames(typeof(SortType)))
            {
                foreach (string periodType in Enum.GetNames(typeof(AdditionalPeriodType)))
                {
                    var requestData = new SearchLeaderboardRequest(sortType, periodType);
                    BinanceRequestTemplate template = CreateBinanceRequestTemplate(LeaderboardEndpoint, requestData);
                    requests.Add(template);
                }
            }
            
            return requests;
        }

        private BinanceRequestTemplate CreateBinanceRequestTemplate<T>(string endpoint, T requestData)
        {
            StringContent content = CreateStringContent(requestData);
            return new BinanceRequestTemplate(endpoint, content);
        }

        private StringContent CreateStringContent<T>(T request)
        {
            string requestJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            return stringContent;
        }

        private List<BinanceRequestTemplate> CreateRequestsForPositions(List<IBinanceTrader> traders)
        {
            var requests = new List<BinanceRequestTemplate>();

            foreach (var binanceTrader in traders)
            {
                var requestData = new OtherPositionRequest(binanceTrader.EncryptedUid);
                BinanceRequestTemplate template = CreateBinanceRequestTemplate(OtherPositionEndpoint, requestData);
                requests.Add(template);
            }

            return requests;
        }

    }
}