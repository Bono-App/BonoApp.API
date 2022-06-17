using BonoApp.API.Bono.Resources;

namespace BonoApp.API.Bono.Domain.Services
{
    public interface IBondAmericanService
    {
        public AmericanBondResource GetResult(int bondId);
    }
}