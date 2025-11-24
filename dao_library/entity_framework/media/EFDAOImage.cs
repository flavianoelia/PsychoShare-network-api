using entity_library.media;
using dao_library.Contexts;
using dao_library.interfaces.media;
using Microsoft.EntityFrameworkCore;

public class EFDAOImage : IDAOImage
{
    private readonly AppDbContext _context;

    public EFDAOImage(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Image?> GetByIdAsync(long id)
    {
        return await _context.Set<Image>().FindAsync(id);
    }

    public async Task<List<Image>> GetByUserIdAsync(long userId)
    {
        return await _context.Set<Image>().Where(i => i.IdUser == userId).ToListAsync();
    }

    public async Task SaveAsync(Image image)
    {
        _context.Set<Image>().Add(image);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var image = await GetByIdAsync(id);
        if (image != null)
        {
            _context.Set<Image>().Remove(image);
            await _context.SaveChangesAsync();
        }
    }
}