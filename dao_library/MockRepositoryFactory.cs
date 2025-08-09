namespace dao_library;

/// <summary>
/// Mock implementation of IRepositoryFactory for development and testing
/// Returns repositories with mock data for Swagger documentation and testing
/// This allows the API to work without a real database connection
/// </summary>
public class MockRepositoryFactory : IRepositoryFactory
{
    /// <summary>
    /// Creates a BanRepository with mock data for development
    /// </summary>
    /// <returns>BanRepository instance with mock implementations</returns>
    public IBanRepository CreateBanRepository()
    {
        // Returns repository with mock data for Swagger and testing
        return new BanRepository();
    }
    
    /// <summary>
    /// Creates a ReportRepository with mock data for development
    /// </summary>
    /// <returns>ReportRepository instance with mock implementations</returns>
    public IReportRepository CreateReportRepository()
    {
        // Returns repository with mock data for Swagger and testing
        return new ReportRepository();
    }
    
    /// <summary>
    /// Creates a FollowingRepository with mock data for development
    /// </summary>
    /// <returns>FollowingRepository instance with mock implementations</returns>
    public IFollowingRepository CreateFollowingRepository()
    {
        // Returns repository with mock data for Swagger and testing
        return new FollowingRepository();
    }
}
