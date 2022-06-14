using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Domain.Repositories;
using BonoApp.API.Shared.Persistence.Contexts;
using BonoApp.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BonoApp.API.Bono.Persistence.Repositories
{
    public class BondRepository : BaseRepository, IBondRepository
    {
        public BondRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Bond>> ListAsync()
        {
            return await _context.Bonds
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task AddAsync(Bond bond)
        {
            await _context.Bonds.AddAsync(bond);
        }

        public async Task<Bond> FindByIdAsync(int id)
        {
            return await _context.Bonds
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Bond>> FindByUserId(int userId)
        {
            return await _context.Bonds
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .ToListAsync();
        }

        public void Update(Bond bond)
        {
            _context.Bonds.Update(bond);
        }

        public void Remove(Bond bond)
        {
            _context.Bonds.Remove(bond);
        }
    }
}