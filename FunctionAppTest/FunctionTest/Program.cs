using FunctionTest.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
internal class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder();
        host.ConfigureFunctionsWebApplication();
        
        host.ConfigureServices( services =>
        {
            services
            .AddFunctionTestServices()
            ;
        });


        host.ConfigureLogging(logging =>
        {
            logging.Services.Configure<LoggerFilterOptions>(options =>
            {
                LoggerFilterRule defaultRule = options.Rules.FirstOrDefault(rule => rule.ProviderName
                    == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");
                if (defaultRule is not null)
                {
                    options.Rules.Remove(defaultRule);
                }
            });
            logging.SetMinimumLevel(LogLevel.Trace);
            logging.AddFilter("FunctionTest.Models", LogLevel.Information);
            logging.AddFilter("FunctionTest.Services.ManagerCountedServiceBase", LogLevel.Information);
            logging.AddFilter("FunctionTest.Services.CharacterManagerService", LogLevel.Information);
            logging.AddFilter("FunctionTest.Services.WordManagerService", LogLevel.Information);
            logging.AddFilter("FunctionTest.Functions", LogLevel.Trace);
            logging.AddFilter("Microsoft", LogLevel.Information);
            logging.AddJsonConsole();
        });
        // Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
        // builder.Services
        //     .AddApplicationInsightsTelemetryWorkerService()
        //     .ConfigureFunctionsApplicationInsights();

        host.Build().Run();
    }

}

