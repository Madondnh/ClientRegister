using CapturingDetails.Web.Services;
using CapturingDetails.Web.Services.ClientService;
using CapturingDetails.Web.Settings;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder( args );
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Load configuration from appsettings.json
var apiSettings = new ApiSettings();
builder.Configuration.Bind( "ApiSettings", apiSettings );

builder.Services.AddScoped( sp => new HttpClient { BaseAddress = new Uri( sp.GetRequiredService<ApiSettings>().BaseClientManagerUrl ) } );
builder.Services.AddSingleton( apiSettings );

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddMudServices();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage( "/_Host" );

app.Run();
