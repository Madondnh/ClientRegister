using Domain.DTOs.ClientDetailsDtos;
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
      var locations = Countries();

      await context.Location.AddRangeAsync( locations );
      await context.SaveChangesAsync();

      var random = new Random();//
      // 2. Define Seed Data
      var clients = new List<ClientDetails>
        {
            // Location: New York (Testing 'Users per Location')
            new ClientDetails {
                ClientName = "TechCorp Solutions",
                Location = locations[ random.Next( locations.Count - 1 )],
                NumberOfUsers = 150,
                DateRegistered = DateTime.Parse("2024-01-15")
            },
            new ClientDetails {
                ClientName = "Gotham Media",
                Location = locations[ random.Next( locations.Count - 1 )],
                NumberOfUsers = 300,
                DateRegistered = DateTime.Parse("2024-01-15") // Same date for 'Clients per Date' test
            },

            // Location: London
            new ClientDetails {
                ClientName = "Global Finance Ltd",
                Location = locations[ random.Next( locations.Count - 1 )],
                NumberOfUsers = 500,
                DateRegistered = DateTime.Parse("2024-02-10")
            },

            // Location: Tokyo
            new ClientDetails {
                ClientName = "Neo-Tokyo Robotics",
                Location = locations[ random.Next( locations.Count - 1 )],
                NumberOfUsers = 1200,
                DateRegistered = DateTime.Parse("2024-03-05")
            },

            // Location: Cape Town (Multiple clients, different dates)
            new ClientDetails {
                ClientName = "Table Mountain Tech",
                Location =  locations[ random.Next( locations.Count - 1 )],
                NumberOfUsers = 50,
                DateRegistered = DateTime.Parse("2024-03-05")
            },
            new ClientDetails {
                ClientName = "Atlantic Devs",
                Location =  locations[ random.Next( locations.Count - 1 )],
                NumberOfUsers = 80,
                DateRegistered = DateTime.UtcNow.AddDays(-1) // Testing dynamic dates
            }
        };

      await context.ClientDetails.AddRangeAsync( clients );
      await context.SaveChangesAsync();
    }


    public static List<Location> Countries()
    {
      return new List<Location>()
      {
        new Location { LocationName = "Afghanistan" },

        new Location { LocationName = "Albania" },

        new Location { LocationName = "Algeria" },

        new Location { LocationName = "Andorra" },

        new Location { LocationName = "Angola" },

        new Location { LocationName = "Antigua and Barbuda" },

        new Location { LocationName = "Argentina" },

        new Location { LocationName = "Armenia" },

        new Location { LocationName = "Australia" },

        new Location { LocationName = "Austria" },
       
        new Location { LocationName = "Azerbaijan" },
       
        new Location { LocationName = "Bahamas" },
       
        new Location { LocationName = "Bahrain" },
       
        new Location { LocationName = "Bangladesh" },
       
        new Location { LocationName = "Barbados" },
       
        new Location { LocationName = "Belarus" },
       
        new Location { LocationName = "Belgium" },
       
        new Location { LocationName = "Belize" },
       
        new Location { LocationName = "Benin" },
       
        new Location { LocationName = "Bhutan" },
       
        new Location { LocationName = "Bolivia" },
       
        new Location { LocationName = "Bosnia and Herzegovina" },
       
        new Location { LocationName = "Botswana" },
       
        new Location { LocationName = "Brazil" },
       
        new Location { LocationName = "Brunei" },
       
        new Location { LocationName = "Bulgaria" },
       
        new Location { LocationName = "Burkina Faso" },
       
        new Location { LocationName = "Burundi" },
       
        new Location { LocationName = "Cabo Verde" },
       
        new Location { LocationName = "Cambodia" },
       
        new Location { LocationName = "Cameroon" },
       
        new Location { LocationName = "Canada" },
       
        new Location { LocationName = "Central African Republic" },
       
        new Location { LocationName = "Chad" },
       
        new Location { LocationName = "Chile" },
       
        new Location { LocationName = "China" },
       
        new Location { LocationName = "Colombia" },
       
        new Location { LocationName = "Comoros" },
       
        new Location { LocationName = "Congo (Congo-Brazzaville)" },
       
        new Location { LocationName = "Costa Rica" },
       
        new Location { LocationName = "Côte d'Ivoire" },
       
        new Location { LocationName = "Croatia" },
       
        new Location { LocationName = "Cuba" },
       
        new Location { LocationName = "Cyprus" },
       
        new Location { LocationName = "Czechia (Czech Republic)" },
       
        new Location { LocationName = "Democratic People's Republic of Korea (North Korea)" },
       
        new Location { LocationName = "Democratic Republic of the Congo" },
       
        new Location { LocationName = "Denmark" },
       
        new Location { LocationName = "Djibouti" },
       
        new Location { LocationName = "Dominica" },
       
        new Location { LocationName = "Dominican Republic" },
       
        new Location { LocationName = "Ecuador" },
       
        new Location { LocationName = "Egypt" },
       
        new Location { LocationName = "El Salvador" },
       
        new Location { LocationName = "Equatorial Guinea" },
       
        new Location { LocationName = "Eritrea" },
       
        new Location { LocationName = "Estonia" },
       
        new Location { LocationName = "Eswatini" },
       
        new Location { LocationName = "Ethiopia" },
       
        new Location { LocationName = "Fiji" },
       
        new Location { LocationName = "Finland" },
       
        new Location { LocationName = "France" },
       
        new Location { LocationName = "Gabon" },
       
        new Location { LocationName = "Gambia" },
       
        new Location { LocationName = "Georgia" },
       
        new Location { LocationName = "Germany" },
       
        new Location { LocationName = "Ghana" },
       
        new Location { LocationName = "Greece" },
       
        new Location { LocationName = "Grenada" },
       
        new Location { LocationName = "Guatemala" },
       
        new Location { LocationName = "Guinea" },
       
        new Location { LocationName = "Guinea-Bissau" },
       
        new Location { LocationName = "Guyana" },
       
        new Location { LocationName = "Haiti" },
       
        new Location { LocationName = "Holy See (Observer)" },
       
        new Location { LocationName = "Honduras" },
       
        new Location { LocationName = "Hungary" },
       
        new Location { LocationName = "Iceland" },
       
        new Location { LocationName = "India" },
       
        new Location { LocationName = "Indonesia" },
       
        new Location { LocationName = "Iran" },
       
        new Location { LocationName = "Iraq" },
       
        new Location { LocationName = "Ireland" },
       
        new Location { LocationName = "Israel" },
       
        new Location { LocationName = "Italy" },
       
        new Location { LocationName = "Jamaica" },
       
        new Location { LocationName = "Japan" },
       
        new Location { LocationName = "Jordan" },
       
        new Location { LocationName = "Kazakhstan" },
       
        new Location { LocationName = "Kenya" },
       
        new Location { LocationName = "Kiribati" },
       
        new Location { LocationName = "Kuwait" },
       
        new Location { LocationName = "Kyrgyzstan" },
       
        new Location { LocationName = "Laos" },
       
        new Location { LocationName = "Latvia" },
       
        new Location { LocationName = "Lebanon" },
       
        new Location { LocationName = "Lesotho" },
       
        new Location { LocationName = "Liberia" },
       
        new Location { LocationName = "Libya" },
       
        new Location { LocationName = "Liechtenstein" },
       
        new Location { LocationName = "Lithuania" },
       
        new Location { LocationName = "Luxembourg" },
       
        new Location { LocationName = "Madagascar" },
       
        new Location { LocationName = "Malawi" },
       
        new Location { LocationName = "Malaysia" },
       
        new Location { LocationName = "Maldives" },
       
        new Location { LocationName = "Mali" },
       
        new Location { LocationName = "Malta" },
       
        new Location { LocationName = "Marshall Islands" },
       
        new Location { LocationName = "Mauritania" },
       
        new Location { LocationName = "Mauritius" },
       
        new Location { LocationName = "Mexico" },
       
        new Location { LocationName = "Micronesia" },
       
        new Location { LocationName = "Moldova" },
       
        new Location { LocationName = "Monaco" },
       
        new Location { LocationName = "Mongolia" },
       
        new Location { LocationName = "Montenegro" },
       
        new Location { LocationName = "Morocco" },
       
        new Location { LocationName = "Mozambique" },
       
        new Location { LocationName = "Myanmar (Burma)" },
       
        new Location { LocationName = "Namibia" },
       
        new Location { LocationName = "Nauru" },
       
        new Location { LocationName = "Nepal" },
       
        new Location { LocationName = "Netherlands" },
       
        new Location { LocationName = "New Zealand" },
       
        new Location { LocationName = "Nicaragua" },
       
        new Location { LocationName = "Niger" },
       
        new Location { LocationName = "Nigeria" },
       
        new Location { LocationName = "North Macedonia" },
       
        new Location { LocationName = "Norway" },
       
        new Location { LocationName = "Oman" },
       
        new Location { LocationName = "Pakistan" },
       
        new Location { LocationName = "Palau" },
       
        new Location { LocationName = "Palestine (Observer)" },
       
        new Location { LocationName = "Panama" },
       
        new Location { LocationName = "Papua New Guinea" },
       
        new Location { LocationName = "Paraguay" },
       
        new Location { LocationName = "Peru" },
       
        new Location { LocationName = "Philippines" },
       
        new Location { LocationName = "Poland" },
       
        new Location { LocationName = "Portugal" },
       
        new Location { LocationName = "Qatar" },
       
        new Location { LocationName = "Republic of Korea (South Korea)" },
       
        new Location { LocationName = "Romania" },
       
        new Location { LocationName = "Russia" },
       
        new Location { LocationName = "Rwanda" },
       
        new Location { LocationName = "Saint Kitts and Nevis" },
       
        new Location { LocationName = "Saint Lucia" },
       
        new Location { LocationName = "Saint Vincent and the Grenadines" },
       
        new Location { LocationName = "Samoa" },
       
        new Location { LocationName = "San Marino" },
       
        new Location { LocationName = "São Tomé and Príncipe" },
       
        new Location { LocationName = "Saudi Arabia" },
       
        new Location { LocationName = "Senegal" },
       
        new Location { LocationName = "Serbia" },
       
        new Location { LocationName = "Seychelles" },
       
        new Location { LocationName = "Sierra Leone" },
       
        new Location { LocationName = "Singapore" },
       
        new Location { LocationName = "Slovakia" },
       
        new Location { LocationName = "Slovenia" },
       
        new Location { LocationName = "Solomon Islands" },
       
        new Location { LocationName = "Somalia" },
       
        new Location { LocationName = "South Africa" },
       
        new Location { LocationName = "South Sudan" },
       
        new Location { LocationName = "Spain" },
       
        new Location { LocationName = "Sri Lanka" },
       
        new Location { LocationName = "Sudan" },
       
        new Location { LocationName = "Suriname" },
       
        new Location { LocationName = "Sweden" },
       
        new Location { LocationName = "Switzerland" },
       
        new Location { LocationName = "Syria" },
       
        new Location { LocationName = "Tajikistan" },
       
        new Location { LocationName = "Tanzania" },
       
        new Location { LocationName = "Thailand" },
       
        new Location { LocationName = "Timor-Leste" },
       
        new Location { LocationName = "Togo" },
       
        new Location { LocationName = "Tonga" },
       
        new Location { LocationName = "Trinidad and Tobago" },
       
        new Location { LocationName = "Tunisia" },
       
        new Location { LocationName = "Turkey" },
       
        new Location { LocationName = "Turkmenistan"},
       
        new Location { LocationName = "Tuvalu" },
       
        new Location { LocationName = "Uganda" },
       
        new Location { LocationName = "Ukraine" },
       
        new Location { LocationName = "United Arab Emirates" },
       
        new Location { LocationName = "United Kingdom" },
       
        new Location { LocationName = "United States of America" },
       
        new Location { LocationName = "Uruguay" },
       
        new Location { LocationName = "Uzbekistan" },
       
        new Location { LocationName = "Vanuatu" },
       
        new Location { LocationName = "Venezuela" },
       
        new Location { LocationName = "Vietnam" },
       
        new Location { LocationName = "Yemen" },
       
        new Location { LocationName = "Zambia" },
       
        new Location { LocationName = "Zimbabwe" }

      };
    }
  }
}
