using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
  public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
  {
    private readonly List<T> _store = new List<T>();  
    
    public InMemoryRepository()
    {
    }

    #region Methods


    public void Dispose()
    {
    }

    /// <summary>
    ///     Get entity by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Entity</returns>
    public virtual T GetById( string id )
    {
      return _store.FirstOrDefault( e => e.Id == id );
    }


    /// <summary>
    ///     Get async entity by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Entity</returns>
    public virtual async Task<T> GetByIdAsync( string id )
    {
      return await Task.FromResult( GetById( id ) );
    }

    /// <summary>
    ///     Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual T Insert( T entity )
    {
      _store.Add( entity );

      return entity;
    }

    /// <summary>
    ///     Async Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task<T> InsertAsync( T entity )
    {
      Insert( entity );

      return await Task.FromResult( entity );
    }

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual T Update( T entity )
    {
      if(_store.Remove( entity ))
      {
        Insert( entity );
      }

      return entity;
    }

    /// <summary>
    ///     Async Update entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task<T> UpdateAsync( T entity )
    {
      Update( entity );
      return await Task.FromResult( entity );
    }

    public void UpdateProperty( object entity, string propertyName, object value )
    {
      // Get the property information from the type
      var propertyInfo = entity.GetType().GetProperty( propertyName );

      if(propertyInfo != null && propertyInfo.CanWrite)
      {
        // SetValue(instance, value)
        propertyInfo.SetValue( entity, value );
      }
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

    /// <summary>
    ///     Delete entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual void Delete( T entity )
    {
      _store.Remove( entity );
    }

    /// <summary>
    ///     Async Delete entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task<T> DeleteAsync( T entity )
    {
      Delete( entity );
      return await Task.FromResult( entity );
    }

    /// <summary>
    ///     Async Delete entities
    /// </summary>
    /// <param name="entities">Entities</param>
    public virtual async Task DeleteAsync( IEnumerable<T> entities )
    {
      foreach(var entity in entities)
        await DeleteAsync( entity );
    }

    #endregion

    #region Helpers

    private static string GetName( LambdaExpression lambdaexpression )
    {
      var expression = (MemberExpression)lambdaexpression.Body;
      return expression.Member.Name;
    }

    private static string GetName<TSource, TField>( Expression<Func<TSource, TField>> Field )
    {
      if(Equals( Field, null ))
        throw new NullReferenceException( "Field is required" );

      MemberExpression expr = null;

      switch(Field.Body)
      {
        case MemberExpression expression:
        expr = expression;
        break;
        case UnaryExpression expression:
        expr = (MemberExpression)expression.Operand;
        break;
        default:
        {
          const string format = "Expression '{0}' not supported.";
          var message = string.Format( format, Field );

          throw new ArgumentException( message, nameof( Field ) );
        }
      }

      return expr.Member.Name;
    }

    public async Task<IEnumerable<T>> GetAsync( int skip, int pageSize )
    {
      return _store != null ?
       _store.Skip( skip ).
         Take( pageSize ) : null;
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
      return _store!= null ?
      Task.FromResult( _store.AsEnumerable() ) : null; 
    }

    public Task IncField<U>( string id, Expression<Func<T, U>> expression, U value )
    {
      throw new NotImplementedException();
    }

    public Task AddToSet<U>( string id, Expression<Func<T, IEnumerable<U>>> field, U value )
    {
      throw new NotImplementedException();
    }

    public Task Pull( string id, Expression<Func<T, IEnumerable<string>>> field, string element )
    {
      throw new NotImplementedException();
    }

    public Task DeleteManyAsync( Expression<Func<T, bool>> filterExpression )
    {
      throw new NotImplementedException();
    }

    public Task ClearAsync()
    {
      throw new NotImplementedException();
    }

    public Task DeleteById( string id )
    {
      throw new NotImplementedException();
    }


    #endregion
  }
}
