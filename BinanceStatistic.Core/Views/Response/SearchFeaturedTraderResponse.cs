using System.Collections.Generic;
using BinanceStatistic.Core.Models;

namespace BinanceStatistic.Core.Views.Response
{
    public class SearchFeaturedTraderResponse: BaseResponse
    {
        public IEnumerable<BinanceTrader> Data { get; set; }
    }
}