namespace BinanceStatistic.Core.Views.Request
{
    public class SearchLeaderboardRequest : BaseSearchTraderRequest
    {
        public SearchLeaderboardRequest()
        {
            Limit = 200;
        }
        
        public string SortType { get; set; }
        public int Limit { get; set; }
        public string PnlGainType { get; set; }
        public string RoiGainType { get; set; }
        public string Symbol { get; set; }
    }
}
