using System.Threading.Tasks;
using BinanceStatistic.BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BinanceStatistic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BinanceGrabberController : ControllerBase
    {
        private readonly IBinanceService _service;

        public BinanceGrabberController(IBinanceService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> CreateAllCurrency()
        {
            await _service.CreateCurrencies();
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> CreatePositions()
        {
            await _service.CreatePositions();
            return Ok();
        }
        
    }
}