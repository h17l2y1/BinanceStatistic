using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace BinanceStatistic.Telegram.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=tcp:db-binance-statistic-server.database.windows.net,1433;Initial Catalog=dbBinanceStatistic;Persist Security Info=False;User ID=BinanceAdmin;Password=real2010++;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            
            var serilogSinkOptions = new MSSqlServerSinkOptions();
            serilogSinkOptions.TableName = "Logs";
            serilogSinkOptions.AutoCreateSqlTable = true;
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.MSSqlServer(connectionString, serilogSinkOptions)
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application failed to start...");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}