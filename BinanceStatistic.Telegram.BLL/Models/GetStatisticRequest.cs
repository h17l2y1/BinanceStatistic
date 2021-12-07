using System.Collections.Generic;

namespace BinanceStatistic.Telegram.BLL.Models
{
    public class GetStatisticRequest
    {
        public List<PositionView> Statistic { get; set; }
        public List<UserView> Users { get; set; }
    }
}