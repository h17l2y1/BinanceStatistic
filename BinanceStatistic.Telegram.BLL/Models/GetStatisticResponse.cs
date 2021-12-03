using System.Collections.Generic;

namespace BinanceStatistic.Telegram.BLL.Models
{
    public class GetStatisticResponse
    {
        public List<PositionView> Statistic { get; set; }
    }
    
    public class PositionView
    {
        public string Currency { get; set; }
        public int Count { get; set; }
        public int Short { get; set; }
        public int Long { get; set; }
        public int CountDiff { get; set; }
        public int ShortDiff { get; set; }
        public int LongDiff { get; set; }
    }
}