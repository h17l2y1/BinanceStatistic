﻿using System;
using System.Threading;
using System.Threading.Tasks;
using BinanceStatistic.BLL.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BinanceStatistic.BLL.Jobs
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
            if (DateTime.Now.Minute % 5 == 0)
            {
                
            }
            
            while (!cancellationToken.IsCancellationRequested)
            {
                using (var scope = Services.CreateScope())
                {
                    var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IBinanceGrabberService>();
                    await scopedProcessingService.GrabbAll();
                }
        
                await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
            }
        }
    }
}