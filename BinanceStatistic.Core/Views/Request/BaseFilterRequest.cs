using BinanceStatistic.Core.Enums;

namespace BinanceStatistic.Core.Views.Request
{
    public class BaseFilterRequest
    {
        public bool IsShared { get; set; }
        public PeriodType PeriodType { get; set; }
        public StatisticType StatisticType { get; set; }
    }
}