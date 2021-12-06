using BinanceStatistic.DAL.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.Grabber.BLL.Config
{
    public static class ConfigureBusinessLogic
    {
        public static void InjectBusinessLogicDependency(this IServiceCollection services, IConfiguration configuration)
        {
            AutoMapper.Add(services);
            BusinessLogicDependencies.Add(services);
            SerilogLogger.Add(services, configuration);
            services.InjectDataAccessDependency(configuration);
        }
    }
}