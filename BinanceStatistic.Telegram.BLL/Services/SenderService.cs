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
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace BinanceStatistic.Telegram.BLL.Services
{
    public class SenderService : ISenderService
    {
        private readonly IUserSubscribeRepository _userSubscribeRepository;
        private readonly ITelegramBotClient _telegramClient;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;
        private readonly string _endpoint;
        
        public SenderService(IUserSubscribeRepository userSubscribeRepository,
            ITelegramBotClient telegramClient, IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration["StatisticApi"]);
            _userSubscribeRepository = userSubscribeRepository;
            _telegramClient = telegramClient;
            _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            _endpoint = configuration["StatisticApiEndpoint"];
        }

        public async Task SendMessageToUsers(int interval)
        {
            List<User> users = await _userSubscribeRepository.GetUsersWithIntervalSubscriptions(interval);

            var xxx = $"{_endpoint}?={interval}";
            
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(xxx);
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
            if (statistics == null || statistics.Count == 0)
            {
                return "Interval data error";
            }
            
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