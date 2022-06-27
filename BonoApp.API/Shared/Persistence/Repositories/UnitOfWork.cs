using System.Threading.Tasks;
using BonoApp.API.Shared.Domain.Repositories;
using BonoApp.API.Shared.Persistence.Contexts;

namespace BonoApp.API.Shared.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}