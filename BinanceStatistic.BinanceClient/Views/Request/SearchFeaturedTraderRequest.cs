using TradeTypeEnum = BinanceStatistic.BinanceClient.Enums.TradeType;

namespace BinanceStatistic.BinanceClient.Views.Request
{
    public class SearchFeaturedTraderRequest : BaseSearchTraderRequest
    {
        public SearchFeaturedTraderRequest()
        {
            Limit = 200;
        }
        
        public string SortType { get; set; }
        public int Limit { get; set; }
    }
}