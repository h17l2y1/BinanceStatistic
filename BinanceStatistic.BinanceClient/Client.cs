using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Models.Interfaces;
using BinanceStatistic.BinanceClient.Views.Response;

namespace BinanceStatistic.BinanceClient
{
    public class Client : IBinanceClient
    {
        private const string GetAllCurrencies = "/fapi/v1/exchangeInfo?showall=true";

        private readonly IBinanceHttpClient _binanceHttpClient;
        private readonly JsonSerializerOptions _options;

        public Client(IBinanceHttpClient binanceHttpClient)
        {
            _binanceHttpClient = binanceHttpClient;
            _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        }
        
        public async Task<IEnumerable<BinanceCurrency>> GrabbCurrencies()
        {
            string response = await _binanceHttpClient.SendGetRequest(GetAllCurrencies);
            GetAllCurrencyResponse responseModel = JsonSerializer.Deserialize<GetAllCurrencyResponse>(response, _options);
            IEnumerable<BinanceCurrency> positions = responseModel?.Symbols;
            return positions;
        }
        
        public async Task<List<IBinanceTrader>> GrabbTraders(List<BinanceRequestTemplate> requests)
        {
            var binanceTraders = new List<IBinanceTrader>();
            List<HttpResponseMessage> responses = await Grabber(requests, "Request for traders");
            
            foreach (var httpResponseMessage in responses)
            {
                string responseJson = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(responseJson))
                {
                    SearchFeaturedTopTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTopTraderResponse>(responseJson, _options);
                    IEnumerable<IBinanceTrader> traders = responseModel?.Data;
                    if (traders != null)
                    {
                        binanceTraders.AddRange(traders);
                    }
                }
            }

            List<IBinanceTrader> uniqueTraders = binanceTraders.GroupBy(p => p.EncryptedUid)
                .Select(grp => grp.First())
                .ToList();
            
            return uniqueTraders;
        }

        public async Task<List<BinancePosition>> GrabbPositions(List<BinanceRequestTemplate> requests)
        {
            List<HttpResponseMessage> responses = await Grabber(requests, "Traders");
            var totalPositions = new List<BinancePosition>();
            
            foreach (var httpResponseMessage in responses)
            {
                string responseJson = httpResponseMessage.Content.ReadAsStringAsync().Result;
                
                if (!string.IsNullOrEmpty(responseJson))
                {
                    OtherPositionResponse responseModel = JsonSerializer.Deserialize<OtherPositionResponse>(responseJson, _options);
                    List<BinancePosition> positions = responseModel?.Data.OtherPositionRetList;
                    if (positions != null)
                    {
                        // TODO: Fix time
                        // Create DateTime form int[]
                        // foreach (var position in positions)
                        // {
                        //     if (position.UpdateTime.Count == 6)
                        //     {
                        //         position.FormattedUpdateTime = new DateTime(
                        //             position.UpdateTime[0],
                        //             position.UpdateTime[1],
                        //             position.UpdateTime[2],
                        //             position.UpdateTime[3],
                        //             position.UpdateTime[4],
                        //             position.UpdateTime[5]
                        //         );
                        //     }
                        // }
                    
                        totalPositions.AddRange(positions);
                    }
                }
            }

            return totalPositions;
        }

        private async Task<List<HttpResponseMessage>> Grabber(List<BinanceRequestTemplate> requests, string debug)
        {
            var responses = new List<HttpResponseMessage>();
            int from = requests.Count;
            int now = 1;

            requests.AsParallel()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(request =>
                {
                    Console.WriteLine($"{debug} requests - {now++}/{from}");
                    HttpResponseMessage responseMessage = _binanceHttpClient.SendMultiPostRequests2(request).Result;
                    responses.Add(responseMessage);
                    return responseMessage;
                })
                .ToList();

            return responses;
        }
    }
}