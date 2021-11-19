using BinanceStatistic.Core.Enums;

namespace BinanceStatistic.Core.Views.Request
{
    public class OtherPositionRequest
    {
        public string EncryptedUid { get; set; }
        public TradeType TradeType { get; set; }
    }
}