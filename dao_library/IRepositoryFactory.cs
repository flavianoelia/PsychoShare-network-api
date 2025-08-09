namespace dao_library;

/// <summary>
/// Abstract Factory interface for creating repository instances
/// This factory provides a centralized way to create all repository instances
/// following the Abstract Factory Design Pattern
/// </summary>
public interface IRepositoryFactory
{
    /// <summary>
    /// Creates a new instance of IBanRepository
    /// </summary>
    /// <returns>Implementation of IBanRepository</returns>
    IBanRepository CreateBanRepository();
    
    /// <summary>
    /// Creates a new instance of IReportRepository
    /// </summary>
    /// <returns>Implementation of IReportRepository</returns>
    IReportRepository CreateReportRepository();
    
    /// <summary>
    /// Creates a new instance of IFollowingRepository
    /// </summary>
    /// <returns>Implementation of IFollowingRepository</returns>
    IFollowingRepository CreateFollowingRepository();
}
