using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
  public class ClientDetails : BaseEntity
  {
    [Required( ErrorMessage = "Client Name is required." )]
    [StringLength( 150, MinimumLength = 2, ErrorMessage = "Client Name must be between 2 and 150 characters." )]
    public string ClientName { get; set; } = string.Empty;

    [Required]
    [DataType( DataType.DateTime )]
    [Display( Name = "Date Registered" )]
    // Ensures the capture app doesn't accidentally log a future date
    public DateTime DateRegistered { get; set; } = DateTime.UtcNow;

    [Required( ErrorMessage = "Location is required." )]
    [StringLength( 200 )]
    public string Location { get; set; } = string.Empty;

    [Required]
    [Range( 1, 1000000, ErrorMessage = "Number of users must be at least 1." )]
    public int NumberOfUsers
    {
      get; set;
    }
  }
}
