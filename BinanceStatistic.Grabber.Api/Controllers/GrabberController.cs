using System.Threading.Tasks;
using BinanceStatistic.Grabber.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinanceStatistic.Grabber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BinanceGrabberController : ControllerBase
    {
        private readonly IBinanceGrabberService _service;

        public BinanceGrabberController(IBinanceGrabberService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GrabbCurrencies()
        {
            var response = await _service.GrabbCurrencies();
            return Ok(response);
        }
        
        [HttpGet]
        public async Task<IActionResult> GrabbAll()
        {
            await _service.CreateStatistic();
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GrabbTraders()
        {
            var response = await _service.GrabbTraders();
            return Ok(response);
        }

    }
}