using System.Threading.Tasks;
using BinanceStatistic.Grabber.BLL.Services.Interface;
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
            await _service.GrabbCurrencies();
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GrabbAll()
        {
            await _service.GrabbAll();
            return Ok();
        }
        
        [HttpGet]
        public async Task<IActionResult> GrabbTraders()
        {
            await _service.GrabbTraders();
            return Ok();
        }

    }
}