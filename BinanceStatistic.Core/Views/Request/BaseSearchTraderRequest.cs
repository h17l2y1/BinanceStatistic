
using TradeTypeEnum = BinanceStatistic.Core.Enums.TradeType;

namespace BinanceStatistic.Core.Views.Request
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