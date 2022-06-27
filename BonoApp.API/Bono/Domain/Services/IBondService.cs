using System.Collections.Generic;
using System.Threading.Tasks;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Domain.Services.Communication;
using BonoApp.API.Bono.Persistence.Repositories;

namespace BonoApp.API.Bono.Domain.Services
{
    public interface IBondService
    {
        Task<IEnumerable<Bond>> ListAsync();
        Task<IEnumerable<Bond>> ListByUserIdAsync(int userId);
        Task<BondResponse> SaveAsync(Bond bond);
        Task<BondResponse> UpdateAsync(int id, Bond bond);
        Task<BondResponse> DeleteAsync(int id);
    }
}