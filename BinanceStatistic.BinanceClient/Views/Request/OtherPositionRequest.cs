using TradeTypeEnum = BinanceStatistic.BinanceClient.Enums.TradeType;

namespace BinanceStatistic.BinanceClient.Views.Request
{
    public class OtherPositionRequest
    {
        public OtherPositionRequest(string encryptedUid)
        {
            EncryptedUid = encryptedUid;
            TradeType = nameof(TradeTypeEnum.PERPETUAL);
        }

        public string EncryptedUid { get; set; }
        public string TradeType { get; set; }
    }
}