using CapturingDetails.Web;
using CapturingDetails.Web.Services;
using CapturingDetails.Web.Services.ClientService;
using DisplayClientDetails.Web.Settings;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault( args );
builder.RootComponents.Add<App>( "#app" );
builder.RootComponents.Add<HeadOutlet>( "head::after" );

// Load configuration from appsettings.json
var apiSettings = new ApiSettings();
builder.Configuration.Bind( "ApiSettings", apiSettings );

builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( sp.GetRequiredService<ApiSettings>().BaseClientManagerUrl ) } );
builder.Services.AddSingleton( apiSettings );

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
