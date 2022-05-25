using Fluxor;
using Microsoft.Extensions.DependencyInjection;

namespace HunterFreemanDev.ClassLibrary;

public static class ServiceProvider
{
    public static IServiceCollection AddHunterFreemanDevClassLibraryServices(this IServiceCollection services)
    {
        return services
            .AddHunterFreemanDevClassLibraryRequiredFluxorServices();
    }
    
    private static IServiceCollection AddHunterFreemanDevClassLibraryRequiredFluxorServices(this IServiceCollection services)
    {
        var currentAssembly = typeof(ServiceProvider).Assembly;

        return services
            .AddFluxor(options => options.ScanAssemblies(currentAssembly));
    }
}
