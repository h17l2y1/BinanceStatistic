using System.Collections.Generic;

namespace BinanceStatistic.BLL.ViewModels
{
    public class SendStatisticRequest
    {
        public SendStatisticRequest(IEnumerable<PositionView> statistic, IEnumerable<UserView> users)
        {
            Statistic = statistic;
            Users = users;
        }

        public IEnumerable<PositionView> Statistic { get; set; }
        public IEnumerable<UserView> Users { get; set; }

    }
}