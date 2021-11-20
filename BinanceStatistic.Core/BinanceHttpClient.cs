using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Response;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BinanceStatistic.Core
{
    public class BinanceHttpClient : BinanceConcurrentRequestsHttpClient, IBinanceHttpClient, IDisposable
    {
        protected const string BaseAddress = "https://www.binance.com";
        protected readonly JsonSerializerOptions Options;
        protected readonly HttpClient HttpClient;

        // private SemaphoreSlim semaphore;
        // private long circuitStatus;
        // private const long CLOSED = 0;
        // private const long TRIPPED = 1;
        // public string UNAVAILABLE = "Unavailable";
        // public int maxConcurrentRequests = 4;
        // private const string url = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";

        public BinanceHttpClient()
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(BaseAddress);
            Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            
            // SetMaxConcurrency(url, maxConcurrentRequests);
            // semaphore = new SemaphoreSlim(maxConcurrentRequests);
            // circuitStatus = CLOSED;
        }

        public async Task<string> SendPostRequest<T>(string url, T request)
        {
            string requestJson = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(url, stringContent);
            string response = CheckResponseForError(httpResponseMessage);
            return response;
        }

        private string CheckResponseForError(HttpResponseMessage httpResponseMessage)
        {
            string responseJson = httpResponseMessage.Content.ReadAsStringAsync().Result;

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                BaseResponse binanceExceptionData = JsonSerializer.Deserialize<BaseResponse>(responseJson, Options);
                throw new BinanceException(httpResponseMessage.StatusCode, httpResponseMessage.ReasonPhrase, binanceExceptionData);
            }

            return responseJson;
        }
        
        
        

        // private void SetMaxConcurrency(string url, int maxConcurrentRequests)
        // {
        //     ServicePointManager.FindServicePoint(new Uri(url)).ConnectionLimit = maxConcurrentRequests;
        // }
        // public void CloseCircuit()
        // {
        //     if (Interlocked.CompareExchange(ref circuitStatus, CLOSED, TRIPPED) == TRIPPED)
        //     {
        //         Console.WriteLine("Closed circuit");
        //     }
        // }
        // private void TripCircuit(string reason)
        // {
        //     if (Interlocked.CompareExchange(ref circuitStatus, TRIPPED, CLOSED) == CLOSED)
        //     {
        //         Console.WriteLine($"Tripping circuit because: {reason}");
        //     }
        // }
        // private bool IsTripped()
        // {
        //     Console.WriteLine("TRIPPED");
        //     return Interlocked.Read(ref circuitStatus) == TRIPPED;
        // }
        //
        // public async Task<string> SendPostRequestsTest<T>(string url, T request)
        // {
        //     try
        //     {
        //         await semaphore.WaitAsync();
        //
        //         if (IsTripped())
        //         {
        //             return UNAVAILABLE;
        //         }
        //
        //         string requestJson = JsonConvert.SerializeObject(request);
        //         var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
        //         HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(url, stringContent);                // var response = await HttpClient.GetAsync(GetRandomNumberUrl);
        //
        //         if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
        //         {
        //             TripCircuit(reason: $"Status not OK. Status={httpResponseMessage.StatusCode}");
        //             return UNAVAILABLE;
        //         }
        //
        //         return await httpResponseMessage.Content.ReadAsStringAsync();
        //     }
        //     catch (Exception ex) when (ex is OperationCanceledException || ex is TaskCanceledException)
        //     {
        //         Console.WriteLine("Timed out");
        //         TripCircuit(reason: $"Timed out");
        //         return UNAVAILABLE;
        //     }
        //     finally
        //     {
        //         semaphore.Release();
        //     }
        // }




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