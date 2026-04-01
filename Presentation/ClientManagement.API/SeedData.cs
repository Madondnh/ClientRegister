using Domain.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API
{
  public class SeedData
  {
    public static async Task AddSeedData( ClientRegisterDBContext context )
    {
      // 1. Check if we already have data
      if(await context.ClientDetails.AnyAsync())
        return;

      // 2. Define Seed Data
      var clients = new List<ClientDetails>
        {
            // Location: New York (Testing 'Users per Location')
            new ClientDetails {
                ClientName = "TechCorp Solutions",
                Location = "New York",
                NumberOfUsers = 150,
                DateRegistered = DateTime.Parse("2024-01-15")
            },
            new ClientDetails {
                ClientName = "Gotham Media",
                Location = "New York",
                NumberOfUsers = 300,
                DateRegistered = DateTime.Parse("2024-01-15") // Same date for 'Clients per Date' test
            },

            // Location: London
            new ClientDetails {
                ClientName = "Global Finance Ltd",
                Location = "London",
                NumberOfUsers = 500,
                DateRegistered = DateTime.Parse("2024-02-10")
            },

            // Location: Tokyo
            new ClientDetails {
                ClientName = "Neo-Tokyo Robotics",
                Location = "Tokyo",
                NumberOfUsers = 1200,
                DateRegistered = DateTime.Parse("2024-03-05")
            },

            // Location: Cape Town (Multiple clients, different dates)
            new ClientDetails {
                ClientName = "Table Mountain Tech",
                Location = "Cape Town",
                NumberOfUsers = 50,
                DateRegistered = DateTime.Parse("2024-03-05")
            },
            new ClientDetails {
                ClientName = "Atlantic Devs",
                Location = "Cape Town",
                NumberOfUsers = 80,
                DateRegistered = DateTime.UtcNow.AddDays(-1) // Testing dynamic dates
            }
        };

      await context.ClientDetails.AddRangeAsync( clients );
      await context.SaveChangesAsync();

      // Ensure the database is created
      //context.Database.EnsureCreated();

    }
  }
}
