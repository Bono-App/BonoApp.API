using System.Threading.Tasks;
using BonoApp.API.Bono.Domain.Services;
using BonoApp.API.Bono.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BonoApp.API.Bono.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BondResultController : ControllerBase
    {
        private readonly IBondResultService _bondResultService;

        public BondResultController(IBondResultService bondResultService)
        {
            _bondResultService = bondResultService;
        }

        [HttpGet("{bondId}/american")]
        public async Task<IActionResult> GetResultAmericanMethod(int bondId)
        {
            var result = _bondResultService.GetResultAmerican(bondId);
            return Ok(result);
        }
        [HttpGet("{bondId}/frances")]
        public async Task<IActionResult> GetResultFrancesMethod(int bondId)
        {
            var result = _bondResultService.GetResultFrances(bondId);
            return Ok(result);
        }
        [HttpGet("{bondId}/germany")]
        public async Task<IActionResult> GetResultGermanyMethod(int bondId)
        {
            var result = _bondResultService.GetResultGermany(bondId);
            return Ok(result);
        }
    }
}