namespace Domain.Interfaces
{
  public interface IViewsRepository<T>
  {
    Task<IEnumerable<T>> GetAsync( int skip, int pageSize );
    Task<IEnumerable<T>> GetAllAsync();
  }
}
