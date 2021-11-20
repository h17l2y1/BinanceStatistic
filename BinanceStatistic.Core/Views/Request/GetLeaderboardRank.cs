using TradeTypeEnum = BinanceStatistic.Core.Enums.TradeType;

namespace BinanceStatistic.Core.Views.Request
{
    public class GetLeaderboardRank
    {
        public GetLeaderboardRank()
        {
            IsShared = true;
            TradeType = nameof(TradeTypeEnum.PERPETUAL);
        }
        
        private bool IsShared { get; set; }
        public string PeriodType { get; set; }
        public string StatisticType { get; set; }
        private string TradeType { get; set; }
    }
}