using System.Collections.Generic;
using BinanceStatistic.BinanceClient.Models;

namespace BinanceStatistic.BinanceClient.Views.Response
{
    public class GetAllCurrencyResponse
    {
        public string Timezone { get; set; }
        public long ServerTime { get; set; }
        public string FuturesType { get; set; }
        // public List<RateLimit> RateLimits { get; set; }
        // public List<object> ExchangeFilters { get; set; }
        // public List<Asset> Assets { get; set; }
        public IEnumerable<BinanceCurrency> Symbols { get; set; }
    }
}