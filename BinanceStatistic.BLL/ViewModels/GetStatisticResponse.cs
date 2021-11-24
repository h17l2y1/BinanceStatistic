using System.Collections.Generic;

namespace BinanceStatistic.BLL.ViewModels
{
    public class GetStatisticResponse
    {
        public GetStatisticResponse(List<PositionView> statistic)
        {
            Statistic = statistic;
        }
        
        public List<PositionView> Statistic { get; set; }

    }
}