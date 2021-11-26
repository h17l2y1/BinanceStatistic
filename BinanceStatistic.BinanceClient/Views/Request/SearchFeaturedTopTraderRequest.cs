namespace BinanceStatistic.BinanceClient.Views.Request
{
    public class SearchFeaturedTopTraderRequest : BaseSearchTraderRequest
    {
        public SearchFeaturedTopTraderRequest(string periodType, string statisticsType)
        {
            PeriodType = periodType;
            StatisticsType = statisticsType;
        }
        
        public string StatisticsType { get; set; }
    }
}