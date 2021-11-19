using System.Collections.Generic;
using BinanceStatistic.Core.Models;

namespace BinanceStatistic.Core.Views.Response
{
    public class SearchFeaturedTraderResponse: BaseResponse
    {
        public IEnumerable<Trader> Data { get; set; }
    }
}