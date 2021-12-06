using System.Threading.Tasks;
using BinanceStatistic.Grabber.BLL.Services.Interfaces;
using Quartz;

namespace BinanceStatistic.Grabber.BLL.Jobs
{
    public class GrabberJob : IJob
    {
        private readonly IBinanceGrabberService _grabberService;

        public GrabberJob(IBinanceGrabberService grabberService)
        {
            _grabberService = grabberService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _grabberService.CreateStatistic();
        }
    }
}