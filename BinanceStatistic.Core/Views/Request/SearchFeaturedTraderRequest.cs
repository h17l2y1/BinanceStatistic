using TradeTypeEnum = BinanceStatistic.Core.Enums.TradeType;

namespace BinanceStatistic.Core.Views.Request
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