using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
  public class ClientRegisterDBContext : DbContext
  {
    public DbSet<ClientDetails> ClientDetails
    {
      get; set;
    }

    public DbSet<ClientDetailsAnalytics> ClientDetailsAnalytics
    {
      get; set;
    }

    public DbSet<Location> Location
    {
      get; set;
    }

    public ClientRegisterDBContext( DbContextOptions options ) : base( options )
    {
      
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
      // Important: Call the base method first
      base.OnModelCreating( modelBuilder );


      modelBuilder.Entity<Location>().HasAlternateKey( c => c.LocationName );

      // Tell EF Core that 'ClientDetailsAnalytics' is a View and has no Primary Key
      modelBuilder.Entity<ClientDetailsAnalytics>( entity =>
      {
        entity.HasNoKey();
        entity.ToView( "View_ClientAnalytics" ); // Ensure this matches your Migration name
      } );
    }
  }
}
