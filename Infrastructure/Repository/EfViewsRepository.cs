using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Repository
{
  public class EfViewsRepository<T> : IViewsRepository<T> where T : class
  {
    #region Fields

    protected readonly ClientRegisterDBContext _context;
    protected readonly DbSet<T> _dbSet;

    #endregion

    #region Ctor

    public EfViewsRepository( DbContextOptions<ClientRegisterDBContext> options )
    {
      _context = new ClientRegisterDBContext( options );
      // This is the most common way for testing: ( ensure that the Schema exists )
      _context.Database.EnsureCreated();
      _dbSet = _context.Set<T>();
    }

    #endregion

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
      return  default( T ); 
    }

    public virtual async Task DeleteById( string id )
    {
      // 1. Find the entity first
      var entity = await _dbSet.FindAsync( id );

      // 2. Check if it exists to avoid NullReferenceException
      if(entity != null)
      {
        // 3. Remove it from the Change Tracker
        _dbSet.Remove( entity );

        // 4. Persist changes to the database
        await _context.SaveChangesAsync();
      }
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
      _dbSet.Add( entity );

      _context.SaveChanges();

      return entity;
    }

    /// <summary>
    ///     Async Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual async Task<T> InsertAsync( T entity )
    {
      Insert( entity );

      await _context.SaveChangesAsync();

      return await Task.FromResult( entity );
    }

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual T Update( T entity )
    {
      return default( T );
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

    /// <summary>
    ///     Update field for entity
    /// </summary>
    /// <typeparam name="U">Value</typeparam>
    /// <param name="id">Ident record</param>
    /// <param name="expression">Linq Expression</param>
    /// <param name="value">value</param>
    public virtual Task UpdateField<U>( string id, Expression<Func<T, U>> expression, U value )
    {
      var entity = _context.Set<T>().Find( id );

      var fieldName = GetName( expression );

      UpdateProperty( entity, fieldName, value );
      UpdateProperty( entity, "UpdatedAt", DateTime.UtcNow );
      

      _context.Set<T>().Update( entity );

      _context.SaveChanges();

      return Task.CompletedTask;
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
    ///     Inc field for entity
    /// </summary>
    /// <typeparam name="U">Value</typeparam>
    /// <param name="id">Ident record</param>
    /// <param name="expression">Linq Expression</param>
    /// <param name="value">value</param>
    public virtual Task IncField<U>( string id, Expression<Func<T, U>> expression, U value )
    {
      var entity = _context.Set<T>().Find( id );
      switch(value)
      {
        case int intValue:

        var intrawValue = Convert.ToInt32( GetProperty( entity, GetName( expression ) ) );
        var bsonIntValue = intrawValue + intValue;

        UpdateProperty( entity, GetName( expression ), bsonIntValue );
        _context.Set<T>().Update( entity );
        break;
        case long longValue:
        var longrawValue = Convert.ToInt64( GetProperty( entity, GetName( expression ) ) );
        var bsonLongValue = longrawValue + longValue;
        UpdateProperty( entity, GetName( expression ), bsonLongValue );
        _context.Set<T>().Update( entity );
        break;
      }

      return Task.CompletedTask;
    }

    /// <summary>
    ///     Add to set - add subdocument
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="id"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public virtual Task AddToSet<U>( string id, Expression<Func<T, IEnumerable<U>>> field, U value )
    {
      //var collection = Database.GetCollection(Collection.Name);
      var entity = _dbSet.Find( id );
      var fieldName = ((MemberExpression)field.Body).Member.Name;

      var entityProp = GetProperty( entity, fieldName );
      var propAsList = (entityProp as IEnumerable<U>)?.ToList();

      if(propAsList != null)
      {
        propAsList.Add( value );

        UpdateProperty( entity, fieldName, propAsList );
        UpdateProperty( entity, "UpdatedAt", DateTime.UtcNow );
        

        _dbSet.Update( entity );

        _context.SaveChanges();
      }

      return Task.CompletedTask;
    }

    /// <summary>
    ///     Delete subdocument
    /// </summary>
    /// <param name="id"></param>
    /// <param name="field"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public virtual Task Pull( string id, Expression<Func<T, IEnumerable<string>>> field, string element )
    {
      return Task.CompletedTask;
    }

    /// <summary>
    ///     Delete entity
    /// </summary>
    /// <param name="entity">Entity</param>
    public virtual void Delete( T entity )
    {
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

    /// <summary>
    ///     Delete a many entities
    /// </summary>
    /// <param name="filterExpression"></param>
    /// <returns></returns>
    public virtual Task DeleteManyAsync( Expression<Func<T, bool>> filterExpression )
    {

      _dbSet.RemoveRange( _dbSet.Where( filterExpression ) );
      return Task.CompletedTask;
    }

    /// <summary>
    ///     Clear entities
    /// </summary>
    public Task ClearAsync()
    {
      _dbSet.ExecuteDelete();
      return Task.CompletedTask;
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
      return _dbSet != null ?
      await _dbSet.Skip( skip ).
         Take( pageSize ).ToListAsync() : null;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
      return await _dbSet.ToListAsync();  
    }

    #endregion
  }
}
