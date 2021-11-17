using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BinanceStatistic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBinanceClient _service;

        public WeatherForecastController(IBinanceClient service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPageSource2()
        {
            await _service.GetPageSource2();
            return Ok();
        }
    }
}