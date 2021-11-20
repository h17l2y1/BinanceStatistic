using TradeTypeEnum = BinanceStatistic.Core.Enums.TradeType;

namespace BinanceStatistic.Core.Views.Request
{
    public class SearchFeaturedTraderRequest
    {
        public SearchFeaturedTraderRequest()
        {
            Limit = 200;    // Max by Binance
            IsShared = true;
            TradeType = nameof(TradeTypeEnum.PERPETUAL);
        }
        
        public string PeriodType { get; set; }
        public string SortType { get; set; }
        private string TradeType { get; set; }
        private int Limit { get; set; }
        private bool IsShared { get; set; }
    }
}