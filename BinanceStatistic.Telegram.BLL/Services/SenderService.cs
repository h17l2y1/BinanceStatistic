using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using BinanceStatistic.Telegram.BLL.Models;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BinanceStatistic.Telegram.BLL.Services
{
    public class SenderService : ISenderService
    {
        private readonly IUserSubscribeRepository _userSubscribeRepository;
        private readonly ITelegramBotClient _telegramClient;
        protected readonly HttpClient HttpClient;
        protected const string BaseAddress = "https://localhost:5001";
        private readonly JsonSerializerOptions _options;
        
        public SenderService(IUserSubscribeRepository userSubscribeRepository,
            ITelegramBotClient telegramClient)
        {
            _userSubscribeRepository = userSubscribeRepository;
            _telegramClient = telegramClient;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(BaseAddress);
            _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        }

        public async Task SendMessageToUsers()
        {
            List<User> users = await _userSubscribeRepository.GetUsersWithIntervalSubscriptions(5);
            string url = "/api/BinanceStatistic/GetInterval";
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(url);
            string responseJson = httpResponseMessage.Content.ReadAsStringAsync().Result;
            GetStatisticResponse responseModel = JsonSerializer.Deserialize<GetStatisticResponse>(responseJson, _options);
            
            string message = CreateMessage(responseModel.Statistic);
            foreach (var user in users)
            {
                await _telegramClient.SendTextMessageAsync(user.TelegramId, message, ParseMode.Html);
            }
        }

        private string CreateMessage(List<PositionView> statistics)
        {
            var sb = new StringBuilder();
            foreach (var position in statistics.OrderByDescending(o=>o.Count).Take(20))
            {
                sb.Append("==========/ ");
                sb.Append($"<b>{position.Currency}</b>");
                sb.Append("/ ========");
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