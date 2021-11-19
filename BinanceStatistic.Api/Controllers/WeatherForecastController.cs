using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.Core.Enums;
using BinanceStatistic.Core.Interfaces;
using BinanceStatistic.Core.Models;
using BinanceStatistic.Core.Views.Request;
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
            var request = new SearchFeaturedTraderRequest
            {
                PeriodType = nameof(PeriodType.DAILY),
                SortType = nameof(SortType.PNL),
                TradeType = nameof(TradeType.PERPETUAL),
                Limit = 200,
                IsShared = true
            };


            var x = await _service.SearchFeaturedTrader(request);
            return Ok(x);
        }
    }
}