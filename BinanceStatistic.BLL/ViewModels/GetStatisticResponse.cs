using System.Collections.Generic;

namespace BinanceStatistic.BLL.ViewModels
{
    public class GetStatisticResponse
    {
        public GetStatisticResponse(IEnumerable<PositionView> statistic)
        {
            Statistic = statistic;
        }
        
        public IEnumerable<PositionView> Statistic { get; set; }

    }
}