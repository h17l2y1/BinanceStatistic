using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Response;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BinanceStatistic.Core
{
    public class BaseBinanceHttpClient : IBaseBinanceHttpClient, IDisposable
    {
        protected const string BaseAddress = "https://www.binance.com";
        protected readonly JsonSerializerOptions Options;
        protected readonly HttpClient HttpClient;
        
        public BaseBinanceHttpClient()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(BaseAddress);
            Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<string> SendPostRequest<T>(string url, T request)
        {
            string requestJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(url, stringContent);
            string response = CheckResponseForError(httpResponseMessage);
            return response;
        }

        public string CheckResponseForError(HttpResponseMessage httpResponseMessage)
        {
            string responseJson = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                BaseResponse binanceExceptionData = JsonSerializer.Deserialize<BaseResponse>(responseJson, Options);
                // throw new BinanceException(httpResponseMessage.StatusCode, binanceExceptionData.Message, binanceExceptionData);
            }

            return responseJson;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                HttpClient.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
    }
}