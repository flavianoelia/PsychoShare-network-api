namespace dao_library;

/// <summary>
/// Production implementation of IRepositoryFactory
/// This factory creates repositories with real database connections
/// Used when the application connects to an actual database
/// </summary>
public class DatabaseRepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    /// <summary>
    /// Constructor that accepts IServiceProvider for dependency injection
    /// </summary>
    /// <param name="serviceProvider">Service provider for resolving dependencies</param>
    public DatabaseRepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    
    /// <summary>
    /// Creates a BanRepository with real database connection
    /// </summary>
    /// <returns>BanRepository instance connected to database</returns>
    public IBanRepository CreateBanRepository()
    {
        // TODO: When database context is ready, inject it here
        // return new BanRepository(dbContext);
        return new BanRepository();
    }
    
    /// <summary>
    /// Creates a ReportRepository with real database connection
    /// </summary>
    /// <returns>ReportRepository instance connected to database</returns>
    public IReportRepository CreateReportRepository()
    {
        // TODO: When database context is ready, inject it here
        // return new ReportRepository(dbContext);
        return new ReportRepository();
    }
    
    /// <summary>
    /// Creates a FollowingRepository with real database connection
    /// </summary>
    /// <returns>FollowingRepository instance connected to database</returns>
    public IFollowingRepository CreateFollowingRepository()
    {
        // TODO: When database context is ready, inject it here
        // return new FollowingRepository(dbContext);
        return new FollowingRepository();
    }
}
