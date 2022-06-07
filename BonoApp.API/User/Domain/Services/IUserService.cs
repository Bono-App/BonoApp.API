using System.Collections.Generic;
using System.Threading.Tasks;
using BonoApp.API.User.Domain.Services.Communication;

namespace BonoApp.API.User.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Models.User>> ListAsync();
        Task<UserResponse> FindByEmailAsync(string email);
        Task<UserResponse> SaveAsync(Models.User user);
        Task<UserResponse> UpdateAsync(int id, Models.User user);
        Task<UserResponse> DeleteAsync(int id);
    }
}