using DisplayClientDetails.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using DisplayClientDetails.Web.Services;
using DisplayClientDetails.Web.Settings;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault( args );
builder.RootComponents.Add<App>( "#app" );
builder.RootComponents.Add<HeadOutlet>( "head::after" );

// Load configuration from appsettings.json
var apiSettings = new ApiSettings();
builder.Configuration.Bind( "ApiSettings", apiSettings );

builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( sp.GetRequiredService<ApiSettings>().BaseUrl) } );
builder.Services.AddMudServices();
builder.Services.AddSingleton( apiSettings );
builder.Services.AddScoped( sp => new AnalyticsService( sp.GetRequiredService<HttpClient>(), sp.GetRequiredService<ApiSettings>() ) );

await builder.Build().RunAsync();
