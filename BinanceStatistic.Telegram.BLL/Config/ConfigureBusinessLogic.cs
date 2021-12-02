using BinanceStatistic.DAL.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace BinanceStatistic.Telegram.BLL.Config
{
    public static class ConfigureBusinessLogic
    {
        public static void InjectBusinessLogicDependency(this IServiceCollection services, IConfiguration configuration)
        {
            AutoMapper.Add(services);
            BusinessLogicDependencies.Add(services);
            TelegramBot.Add(services, configuration);
            SerilogLogger.Add(services, configuration);
            services.InjectDataAccessDependency(configuration);
        }
    }
}