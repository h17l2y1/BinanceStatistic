using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace BinanceStatistic.BLL.Config
{
    public static class SerilogLogger
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            
            var serilogSinkOptions = new MSSqlServerSinkOptions();
            serilogSinkOptions.TableName = "Logs";
            serilogSinkOptions.AutoCreateSqlTable = true;
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.MSSqlServer(connectionString, serilogSinkOptions)
                .WriteTo.Console()
                .CreateLogger();
            
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }
    }
}