using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
  [Index( nameof( LocationName ), IsUnique = true )]
  public class Location : BaseEntity
  {
    [Required( ErrorMessage = "Location Name is required." )]
    [StringLength( 150, MinimumLength = 2, ErrorMessage = "Location Name must be between 2 and 150 characters." )]
    
    public string LocationName { get; set; } = string.Empty;
  }
}
