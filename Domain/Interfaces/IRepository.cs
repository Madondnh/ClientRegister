using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  /// <summary>
  ///     Repository
  /// </summary>
  public interface IRepository<T> 
  {
    /// <summary>
    ///     Get entity by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Entity</returns>
    T GetById( string id );

    Task DeleteById( string id );

    /// <summary>
    ///     Get async entity by identifier
    /// </summary>
    /// <param name="id">Identifier</param>
    /// <returns>Entity</returns>
    Task<T> GetByIdAsync( string id );

    /// <summary>
    ///     Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    T Insert( T entity );

    /// <summary>
    ///     Async Insert entity
    /// </summary>
    /// <param name="entity">Entity</param>
    Task<T> InsertAsync( T entity );

    /// <summary>
    ///     Update entity
    /// </summary>
    /// <param name="entity">Entity</param>
    T Update( T entity );

    /// <summary>
    ///     Async Update entity
    /// </summary>
    /// <param name="entity">Entity</param>
    Task<T> UpdateAsync( T entity );

    /// <summary>
    ///     Delete entity
    /// </summary>
    /// <param name="entity">Entity</param>
    void Delete( T entity );

    /// <summary>
    ///     Async Delete entity
    /// </summary>
    /// <param name="entity">Entity</param>
    Task<T> DeleteAsync( T entity );

    /// <summary>
    ///     Async Delete entities
    /// </summary>
    /// <param name="entities">Entities</param>
    Task DeleteAsync( IEnumerable<T> entities );

    /// <summary>
    ///     Delete a many entities
    /// </summary>
    /// <param name="filterExpression"></param>
    /// <returns></returns>
    Task DeleteManyAsync( Expression<Func<T, bool>> filterExpression );

    /// <summary>
    ///     Clear entities
    /// </summary>
    Task ClearAsync();
    Task<IEnumerable<T>> GetAsync( int skip, int pageSize );
    Task<IEnumerable<T>> GetAllAsync( );
  }
}
