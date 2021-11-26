using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Enums;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Views.Request;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.DAL.Entities;
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
            IEnumerable<BinanceCurrency> binanceCurrencies = await _client.GetCurrencies();
            return binanceCurrencies;
        }
        
        public async Task<List<BinancePosition>> GetPositions()
        {
            // List<OtherPositionRequest> totalTraders = await GetAllTraders();

            List<BinanceRequestTemplate> xxx = CreateRequestsForTraders();
            
            List<BinancePosition> binancePositions = GetPositions(totalTraders);
            return binancePositions;
        }

        public List<BinancePosition> GetPositions(List<OtherPositionRequest> requests)
        {
            var concurrentListPositions = new ConcurrentBag<IEnumerable<BinancePosition>>();
            var positions = new List<BinancePosition>();

            IEnumerable<Position> xxx = new List<Position>();

            // Only for debug console output
            int trader = 1;
            int traderPositions = 0;

            requests.AsParallel()
                .AsOrdered()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(item =>
                {
                    IEnumerable<BinancePosition> positionsResponse = _client.GetPositions(item).Result;
                    if (positionsResponse != null)
                    {
                        // List<BinancePosition> binancePositions = positionsResponse.ToList();
                        concurrentListPositions.Add(positionsResponse);
                        
                        // Only for debug console output
                        // if (i % 10 != 0)
                        // {
                            traderPositions += positionsResponse.Count();
                            Console.WriteLine($"New positions - {traderPositions}");
                            Console.WriteLine($"Traders left - {trader}/{requests.Count}");
                        // }

                        trader++;
                    }
                    return item;
                }).ToList();
            
            // Create DateTime form int[]
            foreach (var listPosition in concurrentListPositions)
            {
                var binancePositions = listPosition.ToList();
                binancePositions.ForEach(position => position.FormattedUpdateTime = new DateTime(
                    position.UpdateTime[0],
                    position.UpdateTime[1],
                    position.UpdateTime[2],
                    position.UpdateTime[3],
                    position.UpdateTime[4],
                    position.UpdateTime[5]
                ));
                positions.AddRange(binancePositions);
            }
            
            return positions;
        }
        
        // public async Task<List<OtherPositionRequest>> GetAllTraders()
        // {
        //     var totalTraders = new List<OtherPositionRequest>();
        //
        //     List<BinanceTopTrader> topTraders = await GetTopByRank();
        //     totalTraders.AddRange(topTraders.Select(s => new OtherPositionRequest(s.EncryptedUid)));
        //     Console.WriteLine($"topTraders - {topTraders.Count}");
        //
        //     List<BinanceTrader> tradersList1 = await GetLeaderboardFeaturedTrader();
        //     totalTraders.AddRange(tradersList1.Select(s => new OtherPositionRequest(s.EncryptedUid)));
        //     Console.WriteLine($"tradersList1 - {tradersList1.Count}");
        //
        //     List<BinanceTrader> tradersList2 = await GetSearchLeaderboard();
        //     totalTraders.AddRange(tradersList2.Select(s => new OtherPositionRequest(s.EncryptedUid)));
        //     Console.WriteLine($"tradersList2 - {tradersList2.Count}");
        //
        //     Console.WriteLine($"positions - {totalTraders.Count}");
        //
        //     return totalTraders;
        // }
        
        // private async Task<List<BinanceTopTrader>> GetTopByRank()
        // {
        //     var requests = new List<SearchFeaturedTopTraderRequest>();
        //     
        //     foreach (string statistic in Enum.GetNames(typeof(StatisticType)))
        //     {
        //         foreach (string period in Enum.GetNames(typeof(PeriodType)))
        //         {
        //             requests.Add(new SearchFeaturedTopTraderRequest(period, statistic));
        //         }
        //     }
        //
        //     var listTraders = new List<BinanceTopTrader>();
        //     for (int i = 0; i < requests.Count; i++)
        //     {
        //         IEnumerable<BinanceTopTrader> position = await _client.GetTopTraders(requests[i]);
        //         listTraders.AddRange(position);
        //     }
        //
        //     return listTraders;
        // }
        
        // private async Task<List<BinanceTrader>> GetLeaderboardFeaturedTrader()
        // {
        //     // TODO: redo request generator
        //     // Max count of request = 9
        //     var requests = new List<SearchFeaturedTraderRequest>
        //     {
        //         new SearchFeaturedTraderRequest
        //         {
        //             PeriodType = nameof(PeriodType.DAILY),
        //             SortType = nameof(SortType.PNL)
        //         },
        //         new SearchFeaturedTraderRequest
        //         {
        //             PeriodType = nameof(PeriodType.DAILY),
        //             SortType = nameof(SortType.ROI)
        //         },
        //         new SearchFeaturedTraderRequest
        //         {
        //             PeriodType = nameof(PeriodType.DAILY),
        //             SortType = nameof(SortType.FOLLOWERS)
        //         }
        //     };
        //
        //     var listTraders = new List<BinanceTrader>();
        //     for (int i = 0; i < requests.Count; i++)
        //     {
        //         IEnumerable<BinanceTrader> position = await _client.GetTraders(requests[i]);
        //         listTraders.AddRange(position);
        //     }
        //
        //     return listTraders;
        // }
        //
        // private async Task<List<BinanceTrader>> GetSearchLeaderboard()
        // {
        //     // TODO: redo request generator
        //     // Max count of request = 18
        //     var requests = new List<SearchLeaderboardRequest>
        //     {
        //         new SearchLeaderboardRequest
        //         {
        //             PeriodType = nameof(PeriodType.DAILY),
        //             SortType = nameof(SortType.PNL)
        //         },
        //         new SearchLeaderboardRequest
        //         {
        //             PeriodType = nameof(PeriodType.DAILY),
        //             SortType = nameof(SortType.ROI)
        //         },
        //         new SearchLeaderboardRequest
        //         {
        //             PeriodType = nameof(PeriodType.DAILY),
        //             SortType = nameof(SortType.FOLLOWERS)
        //         }
        //     };
        //
        //     var listTraders = new List<BinanceTrader>();
        //     for (int i = 0; i < requests.Count; i++)
        //     {
        //         IEnumerable<BinanceTrader> position = await _client.GetTraders(requests[i]);
        //         listTraders.AddRange(position);
        //     }
        //
        //     return listTraders;
        // }

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

            foreach (string sortType in Enum.GetNames(typeof(SortType)))
            {
                foreach (string periodType in Enum.GetNames(typeof(PeriodType)))
                {
                    var requestData = new SearchFeaturedTraderRequest(sortType, periodType);
                    BinanceRequestTemplate template = CreateBinanceRequestTemplate(LeaderboardRankEndpoint, requestData);
                    requests.Add(template);
                }
            }
            
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
            var xxx = new BinanceRequestTemplate(endpoint, content);
        }

        private StringContent CreateStringContent<T>(T request)
        {
            string requestJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            return stringContent;
        }
    }
}