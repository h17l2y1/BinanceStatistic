using System.Collections.Generic;
using BinanceStatistic.BinanceClient.Models.Interfaces;

namespace BinanceStatistic.BinanceClient.Models
{
    public class BinanceTopTrader : IBinanceTrader
    {
        public string FutureUid { get; set; }
        public string NickName { get; set; }
        public string UserPhotoUrl { get; set; }
        public int? Rank { get; set; }
        public decimal Value { get; set; }
        public bool PositionShared { get; set; }
        public string TwitterUrl { get; set; }
        public string EncryptedUid { get; set; }
        public IEnumerable<int> UpdateTime { get; set; }
    }
}