using System.Threading.Tasks;
using BonoApp.API.Bono.Domain.Services;
using BonoApp.API.Bono.Resources;
using Microsoft.AspNetCore.Mvc;

namespace BonoApp.API.Bono.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AmericanBondController : ControllerBase
    {
        private readonly IBondAmericanService _bondAmericanService;

        public AmericanBondController(IBondAmericanService bondAmericanService)
        {
            _bondAmericanService = bondAmericanService;
        }

        [HttpGet("{bondId}")]
        public async Task<IActionResult> GetResult(int bondId)
        {
            var result = _bondAmericanService.GetResult(bondId);
            return Ok(result);
        }
    }
}