using System;
using System.Threading;
using System.Threading.Tasks;
using BinanceStatistic.Grabber.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BinanceStatistic.Grabber.BLL.Jobs
{
    public class GrabberJob : BackgroundService
    {
        public IServiceProvider Services { get; }

        public GrabberJob(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            int time = 10;
            while (!cancellationToken.IsCancellationRequested)
            {
                if (DateTime.Now.Minute % 5 == 0)
                {
                    // time = 300;
                    using (var scope = Services.CreateScope())
                    {
                        var scopedProcessingService =
                            scope.ServiceProvider.GetRequiredService<IBinanceGrabberService>();
                        await scopedProcessingService.CreateStatistic();
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(time), cancellationToken);
            }
        }
    }
}