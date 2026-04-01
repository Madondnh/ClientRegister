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
