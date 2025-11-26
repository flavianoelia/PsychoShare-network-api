using dao_library.Contexts;
using entity_library.media;
using dao_library.interfaces.media;
namespace dao_library.entity_framework.media;

public class EFDAOAvatar : DAOAvatar
{
    private readonly AppDbContext _context;
    public EFDAOAvatar(AppDbContext context) => _context = context;

    public Avatar? GetAvatarByUserId(long userId)
    {
        return _context.Avatar.FirstOrDefault(a => a.IdUser == userId);
    }

    public void Save(Avatar avatar)
    {
        _context.Avatar.Add(avatar);
        _context.SaveChanges();
    }

    public void UpdateAvatarByUserId(long userId, Avatar avatar)
    {
        // Implementa una actualización segura por userId: actualiza solo el campo URL para evitar sobrescribir otras columnas.
        var existing = _context.Avatar.FirstOrDefault(a => a.IdUser == userId);
        if (existing == null) return; // no hay avatar para actualizar
        existing.Url = avatar.Url;
        _context.SaveChanges();
    }

    public void DeleteByUserId(long userId)
    {
        var avatar = _context.Avatar.FirstOrDefault(a => a.IdUser == userId);

        if (avatar != null)
        {
            _context.Avatar.Remove(avatar);
            _context.SaveChanges();
        }
    }
}
