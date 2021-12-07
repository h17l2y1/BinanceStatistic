using System;
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Quartz;

namespace BinanceStatistic.Telegram.BLL.Jobs
{
    public class SenderJob : IJob
    {
        private readonly ISenderService _senderService;

        public SenderJob(ISenderService senderService)
        {
            _senderService = senderService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            int interval = 0;
            if (DateTime.Now.Minute % 5 == 0)
            {
                interval = 5;
            }
            if (DateTime.Now.Minute % 15 == 0)
            {
                interval = 5;
            }
            if (DateTime.Now.Minute % 30 == 0)
            {
                interval = 5;
            }
            if (DateTime.Now.Minute == 0)
            {
                interval = 60;
            }
            
            await _senderService.SendMessageToUsers(interval);
        }
    }
}