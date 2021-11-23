using System.Collections.Generic;
using BinanceStatistic.Core.Models;

namespace BinanceStatistic.Core.Views.Response
{
    public class SearchFeaturedTopTraderResponse: BaseResponse
    {
        public IEnumerable<BinanceTopTrader> Data { get; set; }

    }
}