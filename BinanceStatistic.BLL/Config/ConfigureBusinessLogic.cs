using Microsoft.Extensions.DependencyInjection;

namespace BinanceStatistic.BLL.Config
{
    public static class ConfigureBusinessLogic
    {
        public static void InjectBusinessLogicDependency(this IServiceCollection services)
        {
            AutoMapper.Add(services);
            BusinessLogicDependencies.Add(services);
        }
    }
}