using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Shared.Domain.Services.Communication;

namespace BonoApp.API.Bono.Domain.Services.Communication
{
    public class BondResponse : BaseResponse<Bond>
    {
        public BondResponse(string message) : base(message)
        {
        }

        public BondResponse(Bond resource) : base(resource)
        {
        }
    }
}