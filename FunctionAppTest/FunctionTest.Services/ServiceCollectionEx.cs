using FunctionTest.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FunctionTest.Services
{
    public static class ServiceCollectionEx
    {
        public static IServiceCollection AddFunctionTestServices(this IServiceCollection service)
        {
            service.AddSingleton<IStatisticsService, StatisticsService>();
            service.AddSingleton<ICountedObjectManager<string>, WordManagerService>();
            service.AddSingleton<ICountedObjectManager<char>, CharacterManagerService>();
            service.AddSingleton<IWordStreamService, WordStreamService>();
            return service;
        }
    }
}
