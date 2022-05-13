using HunterFreemanDev.ClassLibrary;
using HunterFreemanDev.RazorClassLibrary;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace HunterFreemanDev.Hosts.MauiBlazor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            builder.Services.AddHunterFreemanDevClassLibraryServices();

            builder.Services.AddHunterFreemanDevRazorClassLibraryServices();

            return builder.Build();
        }
    }
}