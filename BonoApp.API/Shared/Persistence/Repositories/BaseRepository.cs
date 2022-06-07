using BonoApp.API.Shared.Persistence.Contexts;

namespace BonoApp.API.Shared.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}