using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BinanceStatistic.Telegram.BLL.Commands;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BinanceStatistic.Telegram.BLL.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly IConfiguration _configuration;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IMapper _mapper;
        private readonly string _webHook;
        private readonly string _telegramUrl;
        private List<Command> _commands;

        public TelegramBotService(IConfiguration configuration, ITelegramBotClient telegramClient, IMapper mapper)
        {
            _configuration = configuration;
            _telegramClient = telegramClient;
            _mapper = mapper;
            _webHook = configuration["WebHook"];
            _telegramUrl = "/api/telegram/update";
        }
                
        public async Task SetWebhookAsync()
        {
            WebhookInfo> GetWebhookInfoAsync
            
            var telegramWebHook = $"{_webHook}{_telegramUrl}";
            await _telegramClient.SetWebhookAsync(telegramWebHook);
        }

        public async Task Update(Update update)
        {
            // await GetUserDetails(update);
            await InitCommands();

            foreach (Command command in _commands)
            {
                bool isTeamExist = await command.Contains(update.Message.Text);
                if (isTeamExist)
                {
                    await command.Execute(update?.Message, _telegramClient);
                    break;
                }
            }
        }
        
        private async Task InitCommands()
        {
            _commands = new List<Command>
            {
                // TODO: add validation
                //new ErrorCommand(),
                new WelcomeCommand(),
                // new DevelopCommand(_leagueRepository, lastVersion)
            };
        }
    }
}