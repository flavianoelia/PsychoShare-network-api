using entity_library.media;

namespace dao_library.interfaces.media;

public interface IDAOPdf
{
    Task<Pdf?> GetByIdAsync(long id);
    Task<List<Pdf>> GetByUserIdAsync(long userId);
    Task SaveAsync(Pdf pdf);
    Task DeleteAsync(long id);
}