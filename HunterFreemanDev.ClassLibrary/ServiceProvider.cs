using Fluxor;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.ClassLibrary.FileSystem.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HunterFreemanDev.ClassLibrary;

public static class ServiceProvider
{
    public static IServiceCollection AddHunterFreemanDevClassLibraryServices(this IServiceCollection services)
    {
        return services
            .AddHunterFreemanDevClassLibraryRequiredFluxorServices()
            .AddFileBuffer()
            .AddFilePersister();
    }
    
    private static IServiceCollection AddHunterFreemanDevClassLibraryRequiredFluxorServices(this IServiceCollection services)
    {
        var currentAssembly = typeof(ServiceProvider).Assembly;

        return services
            .AddFluxor(options => options.ScanAssemblies(currentAssembly));
    }

    private static IServiceCollection AddFileBuffer(this IServiceCollection services)
    {
        return services.AddScoped<IFileBuffer, FileBuffer>();
    }
    
    private static IServiceCollection AddFilePersister(this IServiceCollection services)
    {
        return services.AddScoped<IFilePersister, FilePersister>();
    }
}
