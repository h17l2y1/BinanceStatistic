using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BinanceStatistic.BinanceClient.Enums;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Models.Interfaces;
using BinanceStatistic.BinanceClient.Views.Request;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using BinanceStatistic.Grabber.BLL.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BinanceStatistic.Grabber.BLL.Services
{
public class BinanceGrabberService : IBinanceGrabberService
    {
        private const string LeaderboardEndpoint = "/bapi/futures/v1/public/future/leaderboard/searchLeaderboard";
        private const string LeaderboardRankEndpoint = "/bapi/futures/v2/public/future/leaderboard/getLeaderboardRank";
        private const string OtherPositionEndpoint = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";
        private readonly IBinanceClient _client;
        private readonly ILogger<BinanceGrabberService> _logger;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public BinanceGrabberService(IBinanceClient client, ILogger<BinanceGrabberService> logger,
            ICurrencyRepository currencyRepository, IPositionRepository positionRepository,
            IMapper mapper)
        {
            _client = client;
            _logger = logger;
            _currencyRepository = currencyRepository;
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BinanceCurrency>> GrabbCurrencies()
        {
            IEnumerable<BinanceCurrency> binanceCurrencies = await _client.GrabbCurrencies();
            IEnumerable<Currency> currencies = _mapper.Map<IEnumerable<Currency>>(binanceCurrencies);
            await _currencyRepository.Create(currencies);
            return binanceCurrencies;
        }
        
        public async Task<IEnumerable<BinancePosition>> CreateStatistic()
        {
            List<BinancePosition> binancePositions = await GrabbAll();
            IEnumerable<Position> positions = await CreateStatistic(binancePositions);
            await _positionRepository.Create(positions);
            return binancePositions;
        }

        public async Task<List<BinancePosition>> GrabbAll()
        {
            List<IBinanceTrader> traders = await GrabbTraders();
            List<BinancePosition> binancePositions = await GrabbPositions(traders);
            return binancePositions;
        }

        public async Task<List<IBinanceTrader>> GrabbTraders()
        {
            List<BinanceRequestTemplate> tradersRequestTemplates = CreateRequestsForTraders();
            _logger.LogDebug("Started GrabbTraders");
            List<IBinanceTrader> traders = await _client.GrabbTraders(tradersRequestTemplates);
            _logger.LogDebug("Ended GrabbTraders - {0} grabbed", traders.Count);
            return traders;
        }
        
        private async Task<IEnumerable<Position>> CreateStatistic(IEnumerable<BinancePosition> positions)
        {
            List<Currency> currencies = await _currencyRepository.GetAll();

            IEnumerable<Position> groupedPositions = positions
                .Where(w => w.FormattedUpdateTime.Day == DateTime.Now.Day)
                .GroupBy(g => g.Symbol)
                .Select(s =>
                {
                    int longPos = s.Count(c => c.Amount > 0);
                    int shortPos = s.Count(c => c.Amount < 0);
                    int totalPos = s.Count();

                    return new Position
                    {
                        CurrencyId = currencies.FirstOrDefault(prop => prop.Name == s.Key)?.Id,
                        Count = totalPos,
                        Short = shortPos,
                        Long = longPos,
                        Amount = s.Sum(f => f.Amount)
                    };
                })
                .Where(w => w.Count > 0);

            return groupedPositions;
        }
        
        private async Task<List<BinancePosition>> GrabbPositions(List<IBinanceTrader> traders)
        {
            List<BinanceRequestTemplate> positionTequestTemplates = CreateRequestsForPositions(traders);
            _logger.LogDebug("Started GrabbPositions");
            List<BinancePosition> positions = await _client.GrabbPositions(positionTequestTemplates);
            _logger.LogDebug("Ended GrabbPositions - {0} grabbed", positions.Count);
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