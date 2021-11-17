using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BinanceStatistic.Core.Enums;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Views.Request;
using BinanceStatistic.Core.Views.Response;
using Newtonsoft.Json;

namespace BinanceStatistic.Core
{
    public class BinanceClient : IBinanceClient, IDisposable
    {
        private const string BaseAddress = "https://www.binance.com";
        private readonly HttpClient _httpClient;

        public BinanceClient()
        {
            _httpClient = new HttpClient();
            // _httpClient.BaseAddress = new Uri(BaseAddress);
        }
        
        // public async Task<T> GetPageSource1<T>(string method, object data = null) where T : BaseResponse, new()
        // {
        //     string requestJson = data == null ? "{}" : JsonConvert.SerializeObject(data);
        //     HttpResponseMessage response = await _httpClient.PostAsync(method, new StringContent(requestJson));
        //     string responseJson = await response.Content.ReadAsStringAsync();
        //     T result = JsonConvert.DeserializeObject<T>(responseJson);
        //     if (response.StatusCode == HttpStatusCode.OK)
        //     {
        //         throw new Exception();
        //         // throw new Exception(response.StatusCode, method, requestJson, responseJson);
        //     }
        //
        //     return result;
        // }

        public async Task GetPageSource2()
        // public async Task GetPageSource2(string url, object body = null)
        {
            string url = "/bapi/futures/v1/public/future/leaderboard/searchFeaturedTrader";
            string fullUrl = "https://www.binance.com/bapi/futures/v1/public/future/leaderboard/searchFeaturedTrader";

            var request = new SearchFeaturedTraderRequest();
            request.PeriodType = nameof(PeriodType.DAILY);
            request.SortType = nameof(SortType.PNL);
            request.TradeType = nameof(TradeType.PERPETUAL);
            request.Limit = 200;

            
            // _httpClient.BaseAddress = new Uri(BaseAddress);
            // var content = new FormUrlEncodedContent(new[]
            // {
            //     new KeyValuePair<string, string>("PeriodType", "DAILY"),
            //     new KeyValuePair<string, string>("SortType", "PNL"),
            //     new KeyValuePair<string, string>("TradeType", "PERPETUAL"),
            //     new KeyValuePair<string, string>("Limit", "200")
            // });
            // var result = await _httpClient.PostAsync(url, content);
            // string resultContent = await result.Content.ReadAsStringAsync();
            

           // HttpResponseMessage response1 = await _httpClient.PostAsync(fullUrl, new StringContent(requestJson));
           
           
           string requestJson = JsonConvert.SerializeObject(request);
           
           // _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           // HttpResponseMessage response2 = await _httpClient.PostAsync(fullUrl, new StringContent(requestJson));
           HttpResponseMessage response2 = await _httpClient.PostAsync(fullUrl, new StringContent(requestJson, Encoding.UTF8, "application/json"));
           var xxx = response2.Content.ReadAsStringAsync().Result;
           
           if (response2.StatusCode != HttpStatusCode.OK)
           {
               var x1 = response2.StatusCode;
               var x2 = response2.ReasonPhrase;
               // throw new Exception();
               // throw new Exception(response.StatusCode, method, requestJson, responseJson);
           }
           
            
            
            
            string source = null;
            // if (response.StatusCode == HttpStatusCode.OK)
            // {
            //     source = await response.Content.ReadAsStringAsync();
            // }
            // return source;
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