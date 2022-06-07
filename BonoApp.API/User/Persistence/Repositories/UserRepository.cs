using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonoApp.API.Shared.Persistence.Contexts;
using BonoApp.API.Shared.Persistence.Repositories;
using BonoApp.API.User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BonoApp.API.User.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Domain.Models.User>> ListAsync()
        {
            return await _context.Users
                .ToListAsync();
        }

        public async Task AddAsync(Domain.Models.User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<Domain.Models.User> FindByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Domain.Models.User> FindByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(p => p.Email == email);
        }

        public bool ExistByEmail(string email)
        {
            return _context.Users.Any(p => p.Email == email);
        }

        public void Update(Domain.Models.User user)
        {
            _context.Users.Update(user);
        }

        public void Remove(Domain.Models.User user)
        {
            _context.Users.Remove(user);
        }
    }
}