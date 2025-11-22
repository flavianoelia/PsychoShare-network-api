using entity_library.media;
using dao_library.Contexts;
using dao_library.interfaces.media;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.social_media_core;

public class EFDAOPdf : IDAOPdf
{
    private readonly AppDbContext _context;

    public EFDAOPdf(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pdf?> GetByIdAsync(long id)
    {
        return await _context.Set<Pdf>().FindAsync(id);
    }

    public async Task<List<Pdf>> GetByUserIdAsync(long userId)
    {
        return await _context.Set<Pdf>()
            .Where(p => p.IdUser == userId)
            .ToListAsync();
    }

    public async Task SaveAsync(Pdf pdf)
    {
        _context.Set<Pdf>().Add(pdf);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var pdf = await GetByIdAsync(id);
        if (pdf != null)
        {
            _context.Set<Pdf>().Remove(pdf);
            await _context.SaveChangesAsync();
        }
    }
}