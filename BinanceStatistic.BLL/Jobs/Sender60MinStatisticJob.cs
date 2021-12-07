using System.Threading.Tasks;
using BinanceStatistic.BLL.Services.Interface;
using Quartz;

namespace BinanceStatistic.BLL.Jobs
{
    public class Sender60MinStatisticJob : IJob
    {
        private readonly ISenderService _senderService;

        public Sender60MinStatisticJob(ISenderService senderService)
        {
            _senderService = senderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _senderService.Send60MinutesUpdate();
        }
    }
}