using System.Collections.Generic;

namespace BinanceStatistic.Telegram.BLL.Models
{
    public class GetStatisticResponse
    {
        public List<PositionView> Statistic { get; set; }
    }
    
    public class PositionView
    {
        // public string Id { get; set; }
        // public DateTime CreationDate { get; set; }
        public string Currency { get; set; }
        public int Count { get; set; }
        public int Short { get; set; }
        public int Long { get; set; }
        // public decimal Amount { get; set; }
        
        public string LongWasNowDiff { get; set; }
        public string ShortWasNowDiff { get; set; }
        public string CountWasNowDiff { get; set; }
    }
}