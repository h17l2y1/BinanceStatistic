using TradeTypeEnum = BinanceStatistic.Core.Enums.TradeType;

namespace BinanceStatistic.Core.Views.Request
{
    public class SearchFeaturedTopTraderRequest : BaseSearchTraderRequest
    {
        public string StatisticsType { get; set; }
    }
}