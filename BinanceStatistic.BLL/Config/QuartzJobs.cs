using System;
using BinanceStatistic.BLL.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BinanceStatistic.BLL.Config
{
    public static class QuartzJobs
    {
        public static void Add(IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey5min = new JobKey("5 min job");
                q.AddJob<Sender5MinStatisticJob>(jobKey5min);

                q.AddTrigger(t => t
                    .ForJob(jobKey5min)
                    .WithCronSchedule("0 0/5 * * * ?")
                );
                
                // var jobKey15min = new JobKey("15 min job");
                // q.AddJob<Sender15MinStatisticJob>(jobKey15min);
                //
                // q.AddTrigger(t => t
                //     .ForJob(jobKey15min)
                //     .WithCronSchedule("0 0/15 * * * ?")
                // );
                //
                // var jobKey30min = new JobKey("30 min job");
                // q.AddJob<Sender30MinStatisticJob>(jobKey30min);
                //
                // q.AddTrigger(t => t
                //     .ForJob(jobKey30min)
                //     .WithCronSchedule("0 0/30 * * * ?")
                // );
                //
                // var jobKey60min = new JobKey("60 min job");
                // q.AddJob<Sender60MinStatisticJob>(jobKey60min);
                //
                // q.AddTrigger(t => t
                //     .ForJob(jobKey60min)
                //     .WithCronSchedule("0 0 0/1 * * ?")
                // );
                
                services.AddQuartzHostedService(options =>
                {
                    options.WaitForJobsToComplete = false;
                });
                
            });
        }
    }
}