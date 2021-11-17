using BinanceStatistic.Core.Enums;

namespace BinanceStatistic.Core.Views.Request
{
    public class GetLeaderboardRank
    {
        public bool IsShared { get; set; }
        public PeriodType PeriodType { get; set; }
        public StatisticType StatisticType { get; set; }
        public TradeType TradeType { get; set; }
    }
}