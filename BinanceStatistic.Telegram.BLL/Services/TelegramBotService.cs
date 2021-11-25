using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BinanceStatistic.Telegram.BLL.Commands;
using BinanceStatistic.Telegram.BLL.Commands.Interfaces;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace BinanceStatistic.Telegram.BLL.Services
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IMapper _mapper;
        private IEnumerable<ICommand> _commands;

        public TelegramBotService(ITelegramBotClient telegramClient, IMapper mapper)
        {
            _telegramClient = telegramClient;
            _mapper = mapper;
            InitCommands();
        }
        
        public async Task Update(Update update)
        {
            foreach (ICommand command in _commands)
            {
                bool isCommandFound = command.Contains(update.Message?.Text);
                if (isCommandFound)
                {
                    await command.Execute(update, _telegramClient);
                    break;
                }
            }
        }
        
        private void InitCommands()
        {
            _commands = new List<ICommand>
            {
                // TODO: add validation
                //new ErrorCommand(),
                new WelcomeCommand(),
                new MainMenuCommand(),
                new SubscribeCommand(),
                // new DevelopCommand(_leagueRepository, lastVersion)
            };
        }
    }
}