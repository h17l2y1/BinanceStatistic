using System;

namespace BinanceStatistic.Core.Models
{
    public class Position
    {
        public long Amount { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal MarkPrice { get; set; }
        public decimal Pnl { get; set; }
        public decimal Roe { get; set; }
        public string Symbol { get; set; }
        public bool TradeBefore { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateTimeStamp { get; set; }
        public bool Yellow { get; set; }
    }
}