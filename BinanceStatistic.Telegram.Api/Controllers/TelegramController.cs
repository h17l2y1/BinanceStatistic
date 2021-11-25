using System;
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace BinanceStatistic.Telegram.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TelegramController : ControllerBase
    {
        private readonly ITelegramBotService _service;
        
        public TelegramController(ITelegramBotService service)
        {
            _service = service;
        }
        
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]Update update)
        {
            if (update == null)
            {
                throw new Exception("Invalid model");
            }
        
            if (update.Message.Text == null)
            {
                return Ok();
            }
        
            await _service.Update(update);
            return Ok();
        }

    }
}