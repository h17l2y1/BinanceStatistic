using TradeTypeEnum = BinanceStatistic.BinanceClient.Enums.TradeType;

namespace BinanceStatistic.BinanceClient.Views.Request
{
    public class SearchFeaturedTraderRequest : BaseSearchTraderRequest
    {
        public SearchFeaturedTraderRequest(string sortType, string periodType)
        {
            SortType = sortType;
            PeriodType = periodType;
        }

        public SearchFeaturedTraderRequest()
        {
            Limit = 200;
        }
        
        public string SortType { get; set; }
        public int Limit { get; set; }
    }
}