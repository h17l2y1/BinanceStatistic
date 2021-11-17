using System.Collections.Generic;
using BinanceStatistic.Core.Models;

namespace BinanceStatistic.Core.Views.Response
{
    public class SearchFeaturedTraderResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        public IEnumerable<Trader> Data { get; set; }
        public bool Success { get; set; }
    }
}