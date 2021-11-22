using System;
using System.Collections.Concurrent;
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
            var totalTraders = new List<OtherPositionRequest>();

            // List<TopTrader> topTraders = await GetTopByRank();
            // totalTraders.AddRange(topTraders.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            // Console.WriteLine($"topTraders - {topTraders.Count}");

            List<Trader> traders1 = await GetLeaderboardFeaturedTrader();
            totalTraders.AddRange(traders1.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            Console.WriteLine($"traders1 - {traders1.Count}");

            // List<Trader> traders2 = await GetSearchLeaderboard();
            // totalTraders.AddRange(traders2.Select(s => new OtherPositionRequest(s.EncryptedUid)));
            // Console.WriteLine($"traders2 - {traders2.Count}");
            
            Console.WriteLine($"positions - {totalTraders.Count}");

            // List<Position> positions = await GetPositions(totalTraders);
            
            
            // List<Position> positions = _positionHelper.GetMocPositions();
            
            List<Position> positions = GetPositions2(totalTraders);
            // CreateStatistic(positions);

            return null;
        }

        private List<Position> GetPositions2(List<OtherPositionRequest> inputItems)
        {
            var listPositions = new ConcurrentBag<List<Position>>();
            var listPositions2 = new List<Position>();

            int i = 1;
            int j = 0;
            
            var processedItems = inputItems
                .AsParallel()   //Allow parallel processing of items
                .AsOrdered()    //Force items in output enumeration to be in the same order as in input
                .WithMergeOptions(ParallelMergeOptions.NotBuffered) //Allows enumeration of processed items as soon as possible (before all items are processed) at the cost of slightly lower performace
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
                    return item;    //return either input item itself, or processed item (e.g. item.ToString())
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

        private void CreateStatistic(IEnumerable<Position> positions)
        {
            // for debug
            DateTime someDay = new DateTime(2021, 11, 21);

            var groupedPositions = positions.Where(w=>w.FormattedUpdateTime.Day == someDay.Day)
                                                             .GroupBy(g => g.Symbol)
                                                             .ToList();

            var xxx = groupedPositions.Select(s => new PositionData
            {
                PositionName = s.Key,
                Count = s.Count(),
                Short = s.Count(c => c.Amount < 0),
                Long = s.Count(c => c.Amount > 0),
                Amount = s.Sum(f=>f.Amount)
            }).Where(w=>w.Count > 0)
                .ToList();
            
        }
    }
}