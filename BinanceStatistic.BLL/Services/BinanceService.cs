using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BinanceStatistic.BLL.Helpers.Interfaces;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.BinanceClient.Enums;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Views.Request;
using BinanceStatistic.BinanceClient.Views.Response;
using BinanceStatistic.DAL.Entities;

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
            // List<OtherPositionRequest> totalTraders = await GetAllTraders();
            // List<Position> positions = GetPositions(totalTraders);

            // MOC Data
            List<BinancePosition> positions = _positionHelper.GetMocPositions();

            List<Position> statistic = CreateStatistic(positions);

            return null;
        }

        private async Task<List<OtherPositionRequest>> GetAllTraders()
        {
            var totalTraders = new List<OtherPositionRequest>();

            List<BinanceTopTrader> topTraders = await GetTopByRank();
            totalTraders.AddRange(topTraders.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            Console.WriteLine($"topTraders - {topTraders.Count}");

            List<BinanceTrader> tradersList1 = await GetLeaderboardFeaturedTrader();
            totalTraders.AddRange(tradersList1.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            Console.WriteLine($"tradersList1 - {tradersList1.Count}");

            List<BinanceTrader> tradersList2 = await GetSearchLeaderboard();
            totalTraders.AddRange(tradersList2.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            Console.WriteLine($"tradersList2 - {tradersList2.Count}");

            Console.WriteLine($"positions - {totalTraders.Count}");

            return totalTraders;
        }

        private List<BinancePosition> GetPositions(List<OtherPositionRequest> inputItems)
        {
            var listPositions = new ConcurrentBag<List<BinancePosition>>();
            var listPositions2 = new List<BinancePosition>();

            int i = 1;
            int j = 0;

            var processedItems = inputItems
                .AsParallel() //Allow parallel processing of items
                .AsOrdered() //Force items in output enumeration to be in the same order as in input
                .WithMergeOptions(ParallelMergeOptions
                    .NotBuffered) //Allows enumeration of processed items as soon as possible (before all items are processed) at the cost of slightly lower performace
                .Select(item =>
                {
                    //Do some processing of item
                    var newPositions = _client.GetPositions(item).Result.ToList();
                    listPositions.Add(newPositions);
                    if (i % 10 != 0)
                    {
                        j += newPositions.Count;
                        Console.WriteLine($"Positions - {newPositions.Count}");
                        Console.WriteLine($"New positions - {j}");
                        Console.WriteLine($"Traders left - {i}/{inputItems.Count}");
                    }

                    i++;
                    return item; //return either input item itself, or processed item (e.g. item.ToString())
                }).ToList();

            foreach (var listPosition in listPositions)
            {
                listPositions2.AddRange(listPosition);
            }

            //You can use processed enumeration just like any other enumeration (send it to the customer, enumerate it yourself using foreach, etc.), items will be in the same order as in input enumeration.
            // foreach (var processedItem in processedItems)
            // {
            //     //Do whatever you want with processed item
            //     Console.WriteLine("Enumerating item " + processedItem);
            // }
            return listPositions2;
        }

        private async Task<List<BinanceTopTrader>> GetTopByRank()
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

            var listTraders = new List<BinanceTopTrader>();
            for (int i = 0; i < requests.Count; i++)
            {
                IEnumerable<BinanceTopTrader> position = await _client.GetTopTraders(requests[i]);
                listTraders.AddRange(position);
            }

            return listTraders;
        }

        private async Task<List<BinanceTrader>> GetLeaderboardFeaturedTrader()
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

            var listTraders = new List<BinanceTrader>();
            for (int i = 0; i < requests.Count; i++)
            {
                IEnumerable<BinanceTrader> position = await _client.GetTraders(requests[i]);
                listTraders.AddRange(position);
            }

            return listTraders;
        }

        private async Task<List<BinanceTrader>> GetSearchLeaderboard()
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

            var listTraders = new List<BinanceTrader>();
            for (int i = 0; i < requests.Count; i++)
            {
                IEnumerable<BinanceTrader> position = await _client.GetTraders(requests[i]);
                listTraders.AddRange(position);
            }

            return listTraders;
        }

        private List<Position> CreateStatistic(IEnumerable<BinancePosition> positions)
        {
            // for debug
            DateTime someDay = new DateTime(2021, 11, 22);

            var groupedPositions = positions
                // .Where(w=>w.FormattedUpdateTime.Day == someDay.Day)
                .GroupBy(g => g.Symbol)
                .ToList();

            List<Position> statistic = groupedPositions.Select(s =>
                {
                    int longPos = s.Count(c => c.Amount > 0);
                    int shortPos = s.Count(c => c.Amount < 0);
                    int totalPos = s.Count();

                    return new Position
                    {
                        PositionName = s.Key,
                        Count = totalPos,
                        Short = shortPos,
                        Long = longPos,
                        Amount = s.Sum(f => f.Amount)
                    };
                }).Where(w => w.Count > 0)
                .OrderByDescending(o => o.Count)
                .ToList();

            return statistic;
        }
    }
}