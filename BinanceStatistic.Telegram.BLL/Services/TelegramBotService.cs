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
        private IEnumerable<ICommand> _commands;

        public TelegramBotService(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
            InitCommands();
        }
        
        public async Task Update(Update update)
        {
            if (update.Message?.Text != null)
            {
                foreach (ICommand command in _commands)
                {
                    var text = update.Message?.Text;
                    bool isCommandFound = command.Contains(update.Message?.Text);
                    if (isCommandFound)
                    {
                        await command.Execute(update, _telegramClient);
                        break;
                    }
                }
            }

            if (update.CallbackQuery?.Data != null)
            {
                // var xxx = await command.Execute(update, _telegramClient);

            }
        }
        
        private void InitCommands()
        {
            _commands = new List<ICommand>
            {
                // new ErrorCommand(),
                new WelcomeCommand(),
                new MainMenuCommand(),
                new SubscribeCommand(),
            };
        }
    }
}