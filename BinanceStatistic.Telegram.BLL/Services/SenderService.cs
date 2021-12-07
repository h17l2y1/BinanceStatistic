using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceStatistic.Telegram.BLL.Models;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BinanceStatistic.Telegram.BLL.Services
{
    public class SenderService : ISenderService
    {
        private readonly ITelegramBotClient _telegramClient;

        public SenderService(ITelegramBotClient telegramClient)
        {
            _telegramClient = telegramClient;
        }

        public async Task SendMessageToUsers(GetStatisticRequest request)
        {
            string message = CreateMessage(request.Statistic);
            foreach (var user in request.Users)
            {
                await _telegramClient.SendTextMessageAsync(user.TelegramId, message, ParseMode.Html);
            }
        }

        private string CreateMessage(List<PositionView> statistics)
        {
            if (statistics == null || statistics.Count == 0)
            {
                return "Interval data error";
            }
            
            var sb = new StringBuilder();
            foreach (var position in statistics.OrderByDescending(o=>o.Count).Take(20))
            {
                sb.Append("=======/ ");
                sb.Append($"<b>{position.Currency}</b>");
                sb.Append("/=======");
                sb.Append("\n\n");
                sb.Append("Всего: "); sb.Append(position.Count);sb.Append("\n");
                sb.Append("Long: "); sb.Append(position.Long);sb.Append("\n");
                sb.Append("Short: "); sb.Append(position.Short);sb.Append("\n");
                sb.Append("------------- Изменения"); sb.Append("\n");
                sb.Append("Всего: "); sb.Append(AddPlus(position.CountDiff));sb.Append("\n");
                sb.Append("Long: "); sb.Append(AddPlus(position.LongDiff)); sb.Append("\n");
                sb.Append("Short: "); sb.Append(AddPlus(position.ShortDiff)); sb.Append("\n");
                sb.Append("\n");
            }

            return sb.ToString();
        }

        private string AddPlus(int number)
        {
            return number.ToString("+#;-#;0");
        }
    }
}