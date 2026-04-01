
using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Catalog.API;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogAPI
{
  public class Program
  {
    public static void Main( string[] args )
    {
      var builder = WebApplication.CreateBuilder( args );

      var connectionString = builder.Configuration.GetConnectionString( "DefaultConnection" );

      builder.Services.AddDbContext<ClientRegisterDBContext>( options =>
          options.UseSqlite( connectionString ) );

      //builder.Services.AddDbContext<ClientRegisterDBContext>( options =>
      //    options.UseInMemoryDatabase( "ClientRegisterDb" ) );

      AddServices( builder );

      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();

      builder.Services.AddSwaggerGen( options =>
      {
        // 1. Define the XML file name (usually matches your Project Name)
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

        // 2. Combine it with the application's base directory
        var xmlPath = Path.Combine( AppContext.BaseDirectory, xmlFilename );


        // 3. Tell Swagger to use those comments
        options.IncludeXmlComments( xmlPath );

      } );

      // Fix for CS1503: Use the correct overload of AddAutoMapper that accepts an assembly
      builder.Services.AddAutoMapper( cfg => { }, typeof( MappingProfile ).Assembly );

      builder.Services.AddCors( options =>
      {
        options.AddPolicy( "AllowAngularApp",
            policy =>
            {
              policy.WithOrigins( "http://localhost:5036",
                                   "http://localhost:5194" ) // Your Angular URL
                .AllowAnyHeader()
                .AllowAnyMethod();
            } );
      } );

      var app = builder.Build();

      app.UseCors( "AllowAngularApp" );

      // --- SEED DATA START ---
      using(var scope = app.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ClientRegisterDBContext>();

        // Ensure the database is created
        context.Database.EnsureCreated();

        SeedData.AddSeedData( context );
      }

      // Configure the HTTP request pipeline.
      if(app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseAuthorization();

      app.MapControllers();

      using(var scope = app.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          var context = services.GetRequiredService<ClientRegisterDBContext>();
          // This line applies migrations and creates the .db file if it doesn't exist
          context.Database.Migrate();
        }
        catch(Exception ex)
        {
          // Log errors if necessary
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError( ex, "An error occurred during database migration." );
        }
      }

      app.Run();
    }

    private static void AddServices( WebApplicationBuilder builder )
    {
      builder.Services.AddScoped<IClientCaptureService, ClientCaptureService>();
      builder.Services.AddScoped<IClientAnalyticsService, ClientAnalyticsService>();
      builder.Services.AddScoped<ILocationService, LocationService>();

      builder.Services.AddScoped( typeof( IRepository<> ), typeof( EfRepository<> ) );
      builder.Services.AddScoped( typeof( IViewsRepository<> ), typeof( EfViewsRepository<> ) );

      
    }
  }
}
