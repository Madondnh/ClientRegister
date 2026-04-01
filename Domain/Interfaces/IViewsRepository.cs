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
  public interface IViewsRepository<T> : IDisposable where T : class
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
    ///     Inc field for entity
    /// </summary>
    /// <typeparam name="U">Value</typeparam>
    /// <param name="id">Ident record</param>
    /// <param name="expression">Linq Expression</param>
    /// <param name="value">value</param>
    Task IncField<U>( string id, Expression<Func<T, U>> expression, U value );

    /// <summary>
    ///     Add to set - add subdocument
    /// </summary>
    /// <typeparam name="U"></typeparam>
    /// <param name="id"></param>
    /// <param name="field"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    Task AddToSet<U>( string id, Expression<Func<T, IEnumerable<U>>> field, U value );

    /// <summary>
    ///     Delete subdocument
    /// </summary>
    /// <param name="id"></param>
    /// <param name="field"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    Task Pull( string id, Expression<Func<T, IEnumerable<string>>> field, string element );

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
