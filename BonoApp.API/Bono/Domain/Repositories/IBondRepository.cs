using System.Collections.Generic;
using System.Threading.Tasks;
using BonoApp.API.Bono.Domain.Models;

namespace BonoApp.API.Bono.Domain.Repositories
{
    public interface IBondRepository
    {
        Task<IEnumerable<Bond>> ListAsync();
        Task AddAsync(Bond bond);
        Task<Bond> FindByIdAsync(int id);
        Task<IEnumerable<Bond>> FindByUserId(int userId);
        void Update(Bond bond);
        void Remove(Bond bond);
    }
}