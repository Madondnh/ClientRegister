using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
  public abstract class BaseEntity
  {
    private string _id = Guid.NewGuid().ToString();

    protected BaseEntity()
    { 
    }

    [Key]
    public string Id
    {
      get => _id;
      set => _id = string.IsNullOrEmpty( value ) ? Guid.NewGuid().ToString() : value;
    }
  }
}
