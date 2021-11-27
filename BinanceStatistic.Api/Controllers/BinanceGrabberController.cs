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
        private readonly IBinanceGrabberService _service2;

        public BinanceGrabberController(IBinanceService service, IBinanceGrabberService service2)
        {
            _service = service;
            _service2 = service2;
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
        
        [HttpGet]
        public async Task<IActionResult> GrabbTraders()
        {
            await _service2.GrabbTraders();
            return Ok();
        }

    }
}