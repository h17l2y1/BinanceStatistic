using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;
using BinanceStatistic.Core.Views.Response;

namespace BinanceStatistic.Core
{
    public class BinanceClient : IBinanceClient
    {
        private const string LeaderboardEndpoint = "/bapi/futures/v1/public/future/leaderboard/searchLeaderboard";
        private const string OtherPositionEndpoint = "/bapi/futures/v1/public/future/leaderboard/getOtherPosition";
        
        private readonly IBinanceHttpClient _binanceHttpClient;
        private readonly JsonSerializerOptions _options;
        private readonly List<string> ids;

        public BinanceClient(IBinanceHttpClient binanceHttpClient)
        {
            _binanceHttpClient = binanceHttpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            ids = new List<string>
            {
                "D163BF3ECAAEE98D987CA3D1BDA9C148",
                "05F613131982C9FEA0AB39AB94FEF9FE",
            };
        }

        public async Task<IEnumerable<Trader>> GetTraders(SearchFeaturedTraderRequest request)
        {
            string response = await _binanceHttpClient.SendPostRequest(LeaderboardEndpoint, request);

            SearchFeaturedTraderResponse responseModel = JsonSerializer.Deserialize<SearchFeaturedTraderResponse>(response, _options);
            IEnumerable<Trader> traders = responseModel?.Data;
            return traders;
        }
        
        public async Task<IEnumerable<Position>> GetPositions(OtherPositionRequest request)
        {
            string response = await _binanceHttpClient.SendPostRequest(OtherPositionEndpoint, request);

            OtherPositionResponse responseModel = JsonSerializer.Deserialize<OtherPositionResponse>(response, _options);
            IEnumerable<Position> positions = responseModel?.OtherPositionRetList;
            return positions;
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
        
        
        
        // private async Task<IEnumerable<OtherPositionResponse>> GetOtherPosition(OtherPositionRequest request)
        // {
        //     string requestJson = JsonConvert.SerializeObject(request);
        //     
        //     var response = await _httpClient.PostAsync(Url2, new StringContent(requestJson, Encoding.UTF8, "application/json"))
        //                                     .ConfigureAwait(false);
        //
        //     string responseJson = response.Content.ReadAsStringAsync().Result;
        //    
        //     if (response.StatusCode != HttpStatusCode.OK)
        //     {
        //         BaseResponse binanceExceptionData = JsonSerializer.Deserialize<BaseResponse>(responseJson, _options);
        //         throw new BinanceException(response.StatusCode, response.ReasonPhrase, binanceExceptionData);
        //     }
        //     
        //     IEnumerable<OtherPositionResponse> positions = JsonConvert.DeserializeObject<IEnumerable<OtherPositionResponse>>(await response.Content.ReadAsStringAsync());
        //     return positions;
        // }
        //
        // public async Task<IEnumerable<OtherPositionResponse>> GetUsersInParallelInWithBatches(IEnumerable<OtherPositionRequest> requests)
        // {
        //     var tasks = new List<Task<IEnumerable<OtherPositionResponse>>>();
        //     var batchSize = 100;
        //     int numberOfBatches = (int)Math.Ceiling((double)requests.Count() / batchSize);
        //
        //     for (int i = 0; i < numberOfBatches; i++)
        //     {
        //         var currentIds = requests.Skip(i * batchSize).Take(batchSize);
        //         tasks.Add(GetOtherPosition(currentIds));
        //     }
        //     
        //     return (await Task.WhenAll(tasks)).SelectMany(u => u);
        // }
        //
        
    }
}