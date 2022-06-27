using BonoApp.API.Bono.Resources;

namespace BonoApp.API.Bono.Domain.Services
{
    public interface IBondResultService
    {
        public BondResultResource GetResultAmerican(int bondId);
        public BondResultResource GetResultFrances(int bondId);
        public BondResultResource GetResultGermany(int bondId);
    }
}