using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinanceStatistic.BLL.Helpers.Interfaces;
using BinanceStatistic.BLL.Models;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.Core.Enums;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;
using BinanceStatistic.Core.Views.Response;
using Newtonsoft.Json;

namespace BinanceStatistic.BLL.Services
{
    public class BinanceService : IBinanceService
    {
        private readonly IBinanceClient _client;
        private readonly IPositionHelper _positionHelper;

        public BinanceService(IBinanceClient client, IPositionHelper positionHelper)
        {
            _client = client;
            _positionHelper = positionHelper;
        }

        public async Task<SearchFeaturedTraderResponse> Test()
        {
            // List<TopTrader> topTraders = await GetTopByRank();
            // List<Trader> traders1 = await GetLeaderboardFeaturedTrader();
            // List<Trader> traders2 = await GetSearchLeaderboard();

            // var positions = new List<OtherPositionRequest>();

            // tradersIds.AddRange(topTraders.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            // tradersIds.AddRange(traders1.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            // tradersIds.AddRange(traders2.Select(s => new OtherPositionRequest(s.EncryptedUid)));

            // var xxx = await GetPositions(positions);

            List<Position> positions = _positionHelper.GetMocPositions();
            CreateStatistic(positions);

            return null;
        }

        private async Task<List<TopTrader>> GetTopByRank()
        {
            // TODO: redo request generator
            // Max count of request = 6
            var requests = new List<SearchFeaturedTopTraderRequest>
            {
                new SearchFeaturedTopTraderRequest
                {
                    PeriodType = nameof(PeriodType.DAILY),
                    StatisticsType = nameof(StatisticType.PNL)
                },
                new SearchFeaturedTopTraderRequest
                {
                    PeriodType = nameof(PeriodType.DAILY),
                    StatisticsType = nameof(StatisticType.ROI)
                }
            };

            var listTraders = new List<TopTrader>();
            for (int i = 0; i < requests.Count; i++)
            {
                IEnumerable<TopTrader> position = await _client.GetTopTraders(requests[i]);
                listTraders.AddRange(position);
            }

            return listTraders;
        }
        
        private async Task<List<Trader>> GetLeaderboardFeaturedTrader()
        {
            // TODO: redo request generator
            // Max count of request = 9
            var requests = new List<SearchFeaturedTraderRequest>
            {
                new SearchFeaturedTraderRequest
                {
                    PeriodType = nameof(PeriodType.DAILY),
                    SortType = nameof(SortType.PNL)
                },
                new SearchFeaturedTraderRequest
                {
                    PeriodType = nameof(PeriodType.DAILY),
                    SortType = nameof(SortType.ROI)
                },
                new SearchFeaturedTraderRequest
                {
                    PeriodType = nameof(PeriodType.DAILY),
                    SortType = nameof(SortType.FOLLOWERS)
                }
            };

            var listTraders = new List<Trader>();
            for (int i = 0; i < requests.Count; i++)
            {
                IEnumerable<Trader> position = await _client.GetTraders(requests[i]);
                listTraders.AddRange(position);
            }

            return listTraders;
        }
        
        private async Task<List<Trader>> GetSearchLeaderboard()
        {
            // TODO: redo request generator
            // Max count of request = 18
            var requests = new List<SearchLeaderboardRequest>
            {
                new SearchLeaderboardRequest
                {
                    PeriodType = nameof(PeriodType.DAILY),
                    SortType = nameof(SortType.PNL)
                },
                new SearchLeaderboardRequest
                {
                    PeriodType = nameof(PeriodType.DAILY),
                    SortType = nameof(SortType.ROI)
                },
                new SearchLeaderboardRequest
                {
                    PeriodType = nameof(PeriodType.DAILY),
                    SortType = nameof(SortType.FOLLOWERS)
                }
            };

            var listTraders = new List<Trader>();
            for (int i = 0; i < requests.Count; i++)
            {
                IEnumerable<Trader> position = await _client.GetTraders(requests[i]);
                listTraders.AddRange(position);
            }

            return listTraders;
        }
        
        private async Task<List<Position>> GetPositions(List<OtherPositionRequest> requests)
        {

            var listPositions = new List<Position>();
            for (int i = 0; i < requests.Count; i++)
            {
                IEnumerable<Position> position = await _client.GetPositions(requests[i]);
                listPositions.AddRange(position);
            }
            
            string requestJson = JsonConvert.SerializeObject(listPositions);

            return listPositions;
        }

        private void CreateStatistic(List<Position> positions)
        {
            var groupedPositions = positions.GroupBy(g => g.Symbol);

            var xxx = groupedPositions.Select(s => new PositionData
            {
                PositionName = s.Key,
                Count = s.Count(),
                Short = s.Count(c => c.Amount < 0),
                Long = s.Count(c => c.Amount > 0)
            });


        }

    }
}