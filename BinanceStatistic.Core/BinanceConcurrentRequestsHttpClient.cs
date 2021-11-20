using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BinanceStatistic.Core.Interfaces;
using Newtonsoft.Json;

namespace BinanceStatistic.Core
{
    public class BinanceConcurrentRequestsHttpClient : BinanceHttpClient, IBinanceConcurrentRequestsHttpClient
    {
        private SemaphoreSlim semaphore;
        private long _circuitStatus;
        private const long CLOSED = 0;
        private const long TRIPPED = 1;
        public string UNAVAILABLE = "Unavailable";
        public int maxConcurrentRequests = 4;
        private const string ENDPOINT = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";

        public BinanceConcurrentRequestsHttpClient()
        {
            SetMaxConcurrency(ENDPOINT, maxConcurrentRequests);
            semaphore = new SemaphoreSlim(maxConcurrentRequests);
            _circuitStatus = CLOSED;
        }
        
        private void SetMaxConcurrency(string url, int maxConcurrentRequests)
        {
            ServicePointManager.FindServicePoint(new Uri(url)).ConnectionLimit = maxConcurrentRequests;
        }
        
        public void CloseCircuit()
        {
            if (Interlocked.CompareExchange(ref _circuitStatus, CLOSED, TRIPPED) == TRIPPED)
            {
                Console.WriteLine("Closed circuit");
            }
        }
        
        private void TripCircuit(string reason)
        {
            if (Interlocked.CompareExchange(ref _circuitStatus, TRIPPED, CLOSED) == CLOSED)
            {
                Console.WriteLine($"Tripping circuit because: {reason}");
            }
        }
        
        private bool IsTripped()
        {
            Console.WriteLine("TRIPPED");
            return Interlocked.Read(ref _circuitStatus) == TRIPPED;
        }
        
        // public async Task<string> SendPostRequestsTest<T>(string url, T request)
        public async Task<string> SendPostRequestsTest<T>(T request)
        {
            try
            {
                await semaphore.WaitAsync();

                if (IsTripped())
                {
                    return UNAVAILABLE;
                }

                string requestJson = JsonConvert.SerializeObject(request);
                var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(ENDPOINT, stringContent);                // var response = await HttpClient.GetAsync(GetRandomNumberUrl);

                if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    TripCircuit(reason: $"Status not OK. Status={httpResponseMessage.StatusCode}");
                    return UNAVAILABLE;
                }

                var xxx = httpResponseMessage.Content.ReadAsStringAsync().Result;
                
                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex) when (ex is OperationCanceledException || ex is TaskCanceledException)
            {
                Console.WriteLine("Timed out");
                TripCircuit(reason: $"Timed out");
                return UNAVAILABLE;
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}