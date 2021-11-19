using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;
using BinanceStatistic.Core.Views.Response;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BinanceStatistic.Core
{
    public class BinanceClient : IBinanceClient, IDisposable
    {
        private const string BaseAddress = "https://www.binance.com";
        private const string Url = "/bapi/futures/v1/public/future/leaderboard/searchLeaderboard";
        private const string Url2 = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";
        private readonly JsonSerializerOptions _options;
        private readonly HttpClient _httpClient;

        public BinanceClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseAddress);
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        
        public async Task<IEnumerable<Trader>> GetTraders(SearchFeaturedTraderRequest request)
        {
            string requestJson = JsonConvert.SerializeObject(request);
            HttpResponseMessage response = await _httpClient.PostAsync(Url, new StringContent(requestJson, Encoding.UTF8, "application/json"));
            string responseJson = response.Content.ReadAsStringAsync().Result;
           
           if (response.StatusCode != HttpStatusCode.OK)
           {
               BaseResponse binanceExceptionData = JsonSerializer.Deserialize<BaseResponse>(responseJson, _options);
               throw new BinanceException(response.StatusCode, response.ReasonPhrase, binanceExceptionData);
           }

           SearchFeaturedTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTraderResponse>(responseJson, _options);
           IEnumerable<Trader> traders = responseModel?.Data;
           return traders;
        }
        
        
        
        
        
        
        // public async Task<IEnumerable<Position>> GetOtherPosition(IEnumerable<string> ids)
        // {
        //     string url = "bapi/futures/v1/public/future/leaderboard/getOtherPosition";
        //     
        //     var taskList = new List<Task<JObject>>();
        //
        //     foreach (var myRequest in RequestsBatch)
        //     {
        //         taskList.Add(GetResponse(url, myRequest));
        //     }
        //
        //     try
        //     {
        //         Task.WaitAll(taskList.ToArray());
        //     }
        //     catch (Exception ex)
        //     {
        //     }
        //     
        //     
        //     
        //     string requestJson = JsonConvert.SerializeObject(request);
        //     HttpResponseMessage response = await _httpClient.PostAsync(Url, new StringContent(requestJson, Encoding.UTF8, "application/json"));
        //     string responseJson = response.Content.ReadAsStringAsync().Result;
        //    
        //     if (response.StatusCode != HttpStatusCode.OK)
        //     {
        //         BaseResponse binanceExceptionData = JsonSerializer.Deserialize<BaseResponse>(responseJson, _options);
        //         throw new BinanceException(response.StatusCode, response.ReasonPhrase, binanceExceptionData);
        //     }
        //
        //     OtherPositionResponse responseModel = JsonSerializer.Deserialize<OtherPositionResponse>(responseJson, _options);
        //     IEnumerable<Position> positions = responseModel?.OtherPositionRetList;
        //     return positions;
        // }
        //
        
        
        
        private async Task<IEnumerable<OtherPositionResponse>> GetOtherPosition(OtherPositionRequest request)
        {
            string requestJson = JsonConvert.SerializeObject(request);
            
            var response = await _httpClient.PostAsync(Url2, new StringContent(requestJson, Encoding.UTF8, "application/json"))
                                            .ConfigureAwait(false);

            string responseJson = response.Content.ReadAsStringAsync().Result;
           
            if (response.StatusCode != HttpStatusCode.OK)
            {
                BaseResponse binanceExceptionData = JsonSerializer.Deserialize<BaseResponse>(responseJson, _options);
                throw new BinanceException(response.StatusCode, response.ReasonPhrase, binanceExceptionData);
            }
            
            IEnumerable<OtherPositionResponse> positions = JsonConvert.DeserializeObject<IEnumerable<OtherPositionResponse>>(await response.Content.ReadAsStringAsync());
            return positions;
        }
        
        public async Task<IEnumerable<OtherPositionResponse>> GetUsersInParallelInWithBatches(IEnumerable<OtherPositionRequest> requests)
        {
            var tasks = new List<Task<IEnumerable<OtherPositionResponse>>>();
            var batchSize = 100;
            int numberOfBatches = (int)Math.Ceiling((double)requests.Count() / batchSize);

            for (int i = 0; i < numberOfBatches; i++)
            {
                var currentIds = requests.Skip(i * batchSize).Take(batchSize);
                tasks.Add(GetOtherPosition(currentIds));
            }
            
            return (await Task.WhenAll(tasks)).SelectMany(u => u);
        }
        
        
        
        
        
        
        
        
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
    }
}