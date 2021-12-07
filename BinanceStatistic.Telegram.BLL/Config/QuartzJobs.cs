using System;
using BinanceStatistic.Grabber.BLL.Jobs;
using BinanceStatistic.Telegram.BLL.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BinanceStatistic.Telegram.BLL.Config
{
    public static class QuartzJobs
    {
        public static void Add(IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey5min = new JobKey("5 min job");
                q.AddJob<SenderJob>(jobKey5min);

                q.AddTrigger(t => t
                    .ForJob(jobKey5min)
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(5)).RepeatForever())
                );
                
                var jobKey15min = new JobKey("15 min job");
                q.AddJob<SenderJob>(jobKey15min);
                
                q.AddTrigger(t => t
                    .ForJob(jobKey15min)
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(15)).RepeatForever())
                );
                
                var jobKey30min = new JobKey("30 min job");
                q.AddJob<SenderJob>(jobKey30min);
                
                q.AddTrigger(t => t
                    .ForJob(jobKey30min)
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(30)).RepeatForever())
                );
                
                var jobKey60min = new JobKey("60 min job");
                q.AddJob<SenderJob>(jobKey60min);
                
                q.AddTrigger(t => t
                    .ForJob(jobKey60min)
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromHours(1)).RepeatForever())
                );
                
                services.AddQuartzHostedService(options =>
                {
                    options.WaitForJobsToComplete = false;
                });
                
            });
        }
    }
}