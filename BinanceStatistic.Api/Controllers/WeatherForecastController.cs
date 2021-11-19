using System.Threading.Tasks;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.Core.Views.Response;
using Microsoft.AspNetCore.Mvc;

namespace BinanceStatistic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBinanceService _service;

        public WeatherForecastController(IBinanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPageSource2()
        {
            SearchFeaturedTraderResponse response = await _service.Test();
            return Ok(response);
        }
    }
}