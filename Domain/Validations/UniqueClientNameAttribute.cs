using Domain.DTOs.ClientDetailsDtos;
using Domain.Interfaces;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Domain.Validation
{
  [AttributeUsage( AttributeTargets.Property )]
  public class UniqueClientNameAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid( object value, ValidationContext validationContext )
    {
      var repository = (IRepository<ClientDetails>)validationContext.GetService( typeof( IRepository<ClientDetails> ) );

      var currentValue = value?.ToString();
      var instance = validationContext.ObjectInstance;

      // Check if the value is null or empty
      if(string.IsNullOrEmpty( currentValue ))
      {
        return ValidationResult.Success;
      }

      var category = repository.GetAllAsync().Result
        .FirstOrDefault( c => c.ClientName.ToLower() == currentValue.ToLower() &&
         (instance as ClientDetails == null || c.Id != ((ClientDetails)instance).Id ));

      if(category != null)
      {
        return new ValidationResult( ErrorMessage );
      }

      return ValidationResult.Success;
    }

    public object GetProperty( object entity, string propertyName )
    {
      // Get the property information from the type
      var propertyInfo = entity.GetType().GetProperty( propertyName );

      if(propertyInfo != null && propertyInfo.CanWrite)
      {
        // SetValue(instance, value)
        return propertyInfo.GetValue( entity );
      }

      return null;
    }
  }
}
