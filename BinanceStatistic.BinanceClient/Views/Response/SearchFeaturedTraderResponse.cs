using System.Collections.Generic;
using BinanceStatistic.BinanceClient.Models;

namespace BinanceStatistic.BinanceClient.Views.Response
{
    public class SearchFeaturedTraderResponse: BaseResponse
    {
        public IEnumerable<BinanceTrader> Data { get; set; }
    }
}