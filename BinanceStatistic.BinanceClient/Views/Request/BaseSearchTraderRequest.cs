
using TradeTypeEnum = BinanceStatistic.BinanceClient.Enums.TradeType;

namespace BinanceStatistic.BinanceClient.Views.Request
{
    public class BaseSearchTraderRequest
    {
        public BaseSearchTraderRequest()
        {
            IsShared = true;
            TradeType = nameof(TradeTypeEnum.PERPETUAL);
        }
        
        public string PeriodType { get; set; }
        public string TradeType { get; set; }
        public bool IsShared { get; set; }
    }
}