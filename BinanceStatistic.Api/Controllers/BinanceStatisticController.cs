using System.Threading.Tasks;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BinanceStatistic.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BinanceStatisticController : ControllerBase
    {
        private readonly IBinanceService _service;

        public BinanceStatisticController(IBinanceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatistic()
        {
            GetStatisticResponse response = await _service.GetPositions();
            return Ok(response);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetInterval()
        {
            GetStatisticResponse response = await _service.GetPositionsWithInterval(5);
            return Ok(response);
        }
        
    }
}