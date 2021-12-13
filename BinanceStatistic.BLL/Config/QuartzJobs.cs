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

                services.AddQuartzHostedService(options =>
                {
                    options.WaitForJobsToComplete = false;
                });
                
            });
        }
    }
}