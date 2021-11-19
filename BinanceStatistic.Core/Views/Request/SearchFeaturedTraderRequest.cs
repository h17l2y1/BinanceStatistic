namespace BinanceStatistic.Core.Views.Request
{
    public class SearchFeaturedTraderRequest
    {
        public string PeriodType { get; set; }
        public string SortType { get; set; }
        public string TradeType { get; set; }
        public int Limit { get; set; }
        public bool IsShared { get; set; }
    }
}