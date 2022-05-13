using Fluxor;
using HunterFreemanDev.ClassLibrary.Dimension;
using HunterFreemanDev.RazorClassLibrary.Dimensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HunterFreemanDev.RazorClassLibrary;

public static class ServiceProvider
{
    public static IServiceCollection AddHunterFreemanDevRazorClassLibraryServices(this IServiceCollection services)
    {
        return services
            .AddViewportDimensionsService();
    }
    
    private static IServiceCollection AddViewportDimensionsService(this IServiceCollection services)
    {
        return services
            .AddScoped<IViewportDimensionsService, ViewportDimensionsService>(serviceProvider =>
                new ViewportDimensionsService(serviceProvider.GetRequiredService<IJSRuntime>()));
    }
}
