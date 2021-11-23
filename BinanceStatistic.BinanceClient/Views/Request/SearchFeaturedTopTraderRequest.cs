using TradeTypeEnum = BinanceStatistic.BinanceClient.Enums.TradeType;

namespace BinanceStatistic.BinanceClient.Views.Request
{
    public class SearchFeaturedTopTraderRequest : BaseSearchTraderRequest
    {
        public string StatisticsType { get; set; }
    }
}