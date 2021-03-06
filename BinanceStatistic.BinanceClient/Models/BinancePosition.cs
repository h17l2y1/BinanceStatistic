using System;
using System.Collections.Generic;

namespace BinanceStatistic.BinanceClient.Models
{
    public class BinancePosition
    {
        public decimal Amount { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal MarkPrice { get; set; }
        public decimal Pnl { get; set; }
        public decimal Roe { get; set; }
        public string Symbol { get; set; }
        public bool TradeBefore { get; set; }
        public int[] UpdateTime { get; set; }
        public DateTime FormattedUpdateTime { get; set; }
        // public string UpdateTimeStamp { get; set; }
        public bool Yellow { get; set; }
    }
}