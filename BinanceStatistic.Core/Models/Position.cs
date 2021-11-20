using System.Collections.Generic;

namespace BinanceStatistic.Core.Models
{
    public class Position
    {
        public decimal? Amount { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal MarkPrice { get; set; }
        public decimal Pnl { get; set; }
        public decimal Roe { get; set; }
        public string Symbol { get; set; }
        public bool TradeBefore { get; set; }
        public IEnumerable<int> UpdateTime { get; set; }
        public long UpdateTimeStamp { get; set; }
        public bool Yellow { get; set; }
    }
}