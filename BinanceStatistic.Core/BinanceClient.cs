using System;
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
        private readonly HttpClient _httpClient;

        public BinanceClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseAddress);
        }
        
        public async Task<SearchFeaturedTraderResponse> SearchFeaturedTrader(SearchFeaturedTraderRequest request)
        {
            // string url = "/bapi/futures/v1/public/future/leaderboard/searchFeaturedTrader";
            string url = "/bapi/futures/v1/public/future/leaderboard/searchLeaderboard";

            string requestJson = JsonConvert.SerializeObject(request);
            HttpResponseMessage response = await _httpClient.PostAsync(url, new StringContent(requestJson, Encoding.UTF8, "application/json"));
            string responseJson = response.Content.ReadAsStringAsync().Result;
           
           if (response.StatusCode != HttpStatusCode.OK)
           {
               BaseResponse binanceExceptionData = JsonSerializer.Deserialize<BaseResponse>(responseJson);
               throw new BinanceException(response.StatusCode, response.ReasonPhrase, binanceExceptionData);
           }
           
           var options = new JsonSerializerOptions
           {
               PropertyNameCaseInsensitive = true
           };

           SearchFeaturedTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTraderResponse>(responseJson, options);
           return responseModel;
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