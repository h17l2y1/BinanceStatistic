using TradeTypeEnum = BinanceStatistic.Core.Enums.TradeType;

namespace BinanceStatistic.Core.Views.Request
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