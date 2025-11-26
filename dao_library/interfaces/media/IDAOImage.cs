using entity_library.media;

namespace dao_library.interfaces.media;

public interface IDAOImage
{
    Task<Image?> GetByIdAsync(long id);
    Task<List<Image>> GetByUserIdAsync(long userId);
    Task SaveAsync(Image image);
    Task DeleteAsync(long id);
}