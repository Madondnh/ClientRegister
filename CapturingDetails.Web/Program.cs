using CapturingDetails.Web;
using CapturingDetails.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault( args );
builder.RootComponents.Add<App>( "#app" );
builder.RootComponents.Add<HeadOutlet>( "head::after" );

// Load configuration from appsettings.json
var http = new HttpClient { BaseAddress = new Uri( builder.HostEnvironment.BaseAddress ) };
builder.Services.AddScoped( sp => http );
using var response = await http.GetAsync( "appsettings.json" );
using var stream = await response.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream( stream );

// Register services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
