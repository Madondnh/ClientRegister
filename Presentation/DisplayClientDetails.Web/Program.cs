using DisplayClientDetails.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using DisplayClientDetails.Web.Settings;
using DisplayClientDetails.Web.Services.AnalyticsService;
using DisplayClientDetails.Web.Services.AnalyticsService.AnalyticsService;

var builder = WebAssemblyHostBuilder.CreateDefault( args );
builder.RootComponents.Add<App>( "#app" );
builder.RootComponents.Add<HeadOutlet>( "head::after" );

// Load configuration from appsettings.json
var apiSettings = new ApiSettings();
builder.Configuration.Bind( "ApiSettings", apiSettings );

builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( sp.GetRequiredService<ApiSettings>().BaseClientManagerUrl ) } );
builder.Services.AddMudServices();
builder.Services.AddSingleton( apiSettings );
builder.Services.AddScoped<IAnalyticsService, AnalyticsService >();

await builder.Build().RunAsync();
