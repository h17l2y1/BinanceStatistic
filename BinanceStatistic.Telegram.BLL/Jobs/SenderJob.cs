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
        private int seconds = 10;

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
                    if (seconds != 300)
                    {
                        seconds = 300;
                    }
                    
                    using (var scope = Services.CreateScope())
                    {
                        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ISenderService>();
                        await scopedProcessingService.SendMessageToUsers(5);
                    }
                }
                
                if (DateTime.Now.Minute % 15 == 0)
                {
                    using (var scope = Services.CreateScope())
                    {
                        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ISenderService>();
                        await scopedProcessingService.SendMessageToUsers(15);
                    }
                }
                
                if (DateTime.Now.Minute % 30 == 0)
                {
                    using (var scope = Services.CreateScope())
                    {
                        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ISenderService>();
                        await scopedProcessingService.SendMessageToUsers(30);
                    }
                }
                
                if (DateTime.Now.Minute % 60 == 0)
                {
                    using (var scope = Services.CreateScope())
                    {
                        var scopedProcessingService = scope.ServiceProvider.GetRequiredService<ISenderService>();
                        await scopedProcessingService.SendMessageToUsers(60);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(seconds), cancellationToken);
            }
            
        }
    }
}