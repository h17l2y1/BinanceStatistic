using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using Newtonsoft.Json;

namespace BinanceStatistic.BinanceClient
{
    public class BinanceHttpClient : RequestSender, IBinanceHttpClient
    {
        private SemaphoreSlim semaphore;
        private long _circuitStatus;
        private const long CLOSED = 0;
        private const long TRIPPED = 1;
        public string UNAVAILABLE = "Unavailable";
        public int maxConcurrentRequests = 10;
        private const string ENDPOINT = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";

        public BinanceHttpClient()
        {
            // SetMaxConcurrency(ENDPOINT, maxConcurrentRequests);
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

        public async Task<string> SendMultiPostRequests<T>(string endPoint, T request)
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
                HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(endPoint, stringContent);

                string response = CheckResponseForError(httpResponseMessage);

                return response;
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
        
        public async Task<string> SendMultiPostRequests2(BinanceRequestTemplate request)
        {
            try
            {
                await semaphore.WaitAsync();

                if (IsTripped())
                {
                    return UNAVAILABLE;
                }
                
                HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(request.Endpoint, request.Content);
                
                // TODO: try remove serialize process out of ddos process
                string response = CheckResponseForError(httpResponseMessage);

                return response;
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