using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BonoApp.API.Bono.Domain.Models;
using BonoApp.API.Bono.Domain.Services;
using BonoApp.API.Bono.Resources;
using BonoApp.API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BonoApp.API.Bono.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BondsController : ControllerBase
    {
        private readonly IBondService _bondService;
        private readonly IMapper _mapper;

        public BondsController(IBondService bondService, IMapper mapper)
        {
            _bondService = bondService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IEnumerable<BondResource>> GetAllAsync()
        {
            var bonds = await _bondService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Bond>, IEnumerable<BondResource>>(bonds);
            return resources;
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveBondResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var bond = _mapper.Map<SaveBondResource, Bond>(resource);

            var result = await _bondService.SaveAsync(bond);

            if (!result.Success)
                return BadRequest(result.Message);

            var bondResource = _mapper.Map<Bond, BondResource>(result.Resource);

            return Ok(bondResource);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBondResource resource)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var item = _mapper.Map<SaveBondResource, Bond>(resource);

            var result = await _bondService.UpdateAsync(id, item);

            if (!result.Success)
                return BadRequest(result.Message);

            var bondResource = _mapper.Map<Bond, BondResource>(result.Resource);

            return Ok(bondResource);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _bondService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var bondResource = _mapper.Map<Bond, BondResource>(result.Resource);

            return Ok(bondResource);
        }
    }
}