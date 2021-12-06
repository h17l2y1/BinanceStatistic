using System;
using System.Threading;
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BinanceStatistic.Telegram.BLL.Jobs
{
    public class SenderJob : BackgroundService
    {
        public IServiceProvider Services { get; }

        public SenderJob(IServiceProvider services)
        {
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (DateTime.Now.Minute % 5 == 0)
                {
                    using (var scope = Services.CreateScope())
                    {
                        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ISenderService>();
                        await scopedProcessingService.SendMessageToUsers();
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
        }
    }
}