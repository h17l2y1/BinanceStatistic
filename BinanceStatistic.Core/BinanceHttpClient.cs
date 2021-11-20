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
    public class BinanceHttpClient : IBinanceHttpClient, IDisposable
    {
        private const string BaseAddress = "https://www.binance.com";
        private readonly JsonSerializerOptions _options;
        private readonly HttpClient _httpClient;

        public BinanceHttpClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(BaseAddress);
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<string> SendPostRequest<T>(string url, T request)
        {
            string requestJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(url, stringContent);
            string response = CheckResponseForError(httpResponseMessage);
            return response;
        }

        private string CheckResponseForError(HttpResponseMessage httpResponseMessage)
        {
            string responseJson = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                BaseResponse binanceExceptionData = JsonSerializer.Deserialize<BaseResponse>(responseJson, _options);
                throw new BinanceException(httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase, binanceExceptionData);
            }

            return responseJson;
        }
        
        
        
        
        public Task<string> SendPostRequestsTest<T>(string url, T request)
        {
            throw new NotImplementedException();
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