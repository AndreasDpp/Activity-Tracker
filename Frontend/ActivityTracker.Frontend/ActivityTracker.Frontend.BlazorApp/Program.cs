using ActivityTracker.Frontend.BlazorApp;
using ActivityTracker.Frontend.BlazorApp.Helper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri(BackendEndpointHelper.Base())
    };
    
    return client;
});

await builder.Build().RunAsync();
