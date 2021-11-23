using BinanceStatistic.BinanceClient.Models.Interfaces;

namespace BinanceStatistic.BinanceClient.Models
{
    public class BinanceTrader : IBinanceTrader
    {
        public string EncryptedUid { get; set; }
        public string NickName { get; set; }
        public string UserPhotoUrl { get; set; }
        public bool PositionShared { get; set; }
        public bool DeliveryPositionShared { get; set; }
        public int FollowerCount { get; set; }
        public decimal? PnlValue { get; set; }
        public decimal? RoiValue { get; set; }
        public int? Rank { get; set; }
        public string Change { get; set; }
    }
}