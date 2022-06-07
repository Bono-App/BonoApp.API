using System.Threading.Tasks;

namespace BonoApp.API.Shared.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}