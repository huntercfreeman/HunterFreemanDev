using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.ClassLibrary.FileSystem.Classes;
using HunterFreemanDev.RazorClassLibrary.Dimensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace HunterFreemanDev.RazorClassLibrary;

public static class ServiceProvider
{
    public static IServiceCollection AddHunterFreemanDevRazorClassLibraryServices(this IServiceCollection services,
        bool allowFileSystemAccess)
    {
        return services
            .AddViewportDimensionsService()
            .AddFileSystemPermissionSettings(allowFileSystemAccess);
    }
    
    private static IServiceCollection AddViewportDimensionsService(this IServiceCollection services)
    {
        return services
            .AddScoped<IViewportDimensionsService, ViewportDimensionsService>(serviceProvider =>
                new ViewportDimensionsService(serviceProvider.GetRequiredService<IJSRuntime>()));
    }

    private static IServiceCollection AddFileSystemPermissionSettings(this IServiceCollection services, bool allowFileSystemAccess)
    {
        return services
            .AddScoped<FileSystemAccessSettings>(serviceProvider =>
                new FileSystemAccessSettings { AllowFileSystemAccess = allowFileSystemAccess });
    }
}
