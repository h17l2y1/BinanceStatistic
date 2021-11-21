using System;
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
            // Console.WriteLine($"topTraders - {topTraders.Count}");

            // List<Trader> traders1 = await GetLeaderboardFeaturedTrader();
            // Console.WriteLine($"traders1 - {traders1.Count}");

            // List<Trader> traders2 = await GetSearchLeaderboard();
            // Console.WriteLine($"traders2 - {traders2.Count}");

            var totalTraders = new List<OtherPositionRequest>();

            // totalTraders.AddRange(topTraders.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            // totalTraders.AddRange(traders1.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            // totalTraders.AddRange(traders2.Select(s => new OtherPositionRequest(s.EncryptedUid)));

            // Console.WriteLine($"positions - {totalTraders.Count}");

            // List<Position> positions = await GetPositions(totalTraders);

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
                if (i % 10 != 0)
                {
                    Console.WriteLine($"positions - {listPositions.Count}");
                    Console.WriteLine($"Traders left - {i}/{requests.Count}");
                }
            }

            return listPositions;
        }

        private void CreateStatistic(List<Position> positions)
        {
            var groupedPositions = positions.Where(w=>w.FormattedUpdateTime.Day == DateTime.Today.Day)
                                                             .GroupBy(g => g.Symbol)
                                                             .ToList();

            var xxx = groupedPositions.Select(s => new PositionData
            {
                PositionName = s.Key,
                Count = s.Count(),
                Short = s.Count(c => c.Amount < 0),
                Long = s.Count(c => c.Amount > 0)
            }).Where(w=>w.Count > 0);
            
        }
    }
}