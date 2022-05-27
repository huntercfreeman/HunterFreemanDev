using HunterFreemanDev.ClassLibrary;
using HunterFreemanDev.Hosts.BlazorWebAssembly;
using HunterFreemanDev.RazorClassLibrary;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHunterFreemanDevClassLibraryServices();

builder.Services.AddHunterFreemanDevRazorClassLibraryServices(false);

await builder.Build().RunAsync();
