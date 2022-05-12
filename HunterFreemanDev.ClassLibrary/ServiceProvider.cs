using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
