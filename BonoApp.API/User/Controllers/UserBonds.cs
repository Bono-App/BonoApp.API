using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Domain.Services;
using BonoApp.API.Bono.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BonoApp.API.User.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/users/{userId}/bonds")]
    public class UserBonds : ControllerBase
    {
        private readonly IBondService _bondService;
        private readonly IMapper _mapper;

        public UserBonds(IBondService bondService, IMapper mapper)
        {
            _bondService = bondService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IEnumerable<BondResource>> GetAllByUserIdAsync(int userId)
        {
            var bonds = await _bondService.ListByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Bond>, IEnumerable<BondResource>>(bonds);

            return resources;
        }
    }
}