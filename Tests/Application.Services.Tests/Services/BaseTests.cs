namespace Application.Services
{
  public class BaseTests
  {
    public enum ConnectionType
    {
      EntityFramework,
      InMemoryDb
    }

    protected string databaseName;
    protected static int dbCount = 0;

    public BaseTests()
    {
      dbCount += 1;
      databaseName = "TestDb" + dbCount.ToString();
    }
  }
}
