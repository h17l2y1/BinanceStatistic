using System.Collections.Generic;
using BinanceStatistic.BinanceClient.Models;

namespace BinanceStatistic.BinanceClient.Views.Response
{
    public class SearchFeaturedTopTraderResponse: BaseResponse
    {
        public IEnumerable<BinanceTopTrader> Data { get; set; }

    }
}