using System;
using BinanceStatistic.Grabber.BLL.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BinanceStatistic.Grabber.BLL.Config
{
    public static class QuartzJobs
    {
        public static void Add(IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = new JobKey("Grabber job");
                q.AddJob<GrabberJob>(jobKey);

                q.AddTrigger(t => t
                    .ForJob(jobKey)
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMinutes(5)).RepeatForever())
                );
                
                services.AddQuartzHostedService(options =>
                {
                    options.WaitForJobsToComplete = true;
                });
                
            });
        }
    }
}