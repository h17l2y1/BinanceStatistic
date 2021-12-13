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
        public async Task<IActionResult> GetPositions()
        {
            GetStatisticResponse response = await _service.GetPositions();
            return Ok(response);
        }
        
    }
}