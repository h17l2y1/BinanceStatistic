using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using BinanceStatistic.Telegram.BLL.Models;
using BinanceStatistic.Telegram.BLL.Services.Interfaces;
using Telegram.Bot;

namespace BinanceStatistic.Telegram.BLL.Services
{
    public class SenderService : ISenderService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscribeRepository _subscribeRepository;
        private readonly IUserSubscribeRepository _userSubscribeRepository;
        private readonly ITelegramBotClient _telegramClient;
        protected readonly HttpClient HttpClient;
        protected const string BaseAddress = "https://localhost:5001";
        private readonly JsonSerializerOptions _options;

        
        public SenderService(IUserRepository userRepository, ISubscribeRepository subscribeRepository,
            IUserSubscribeRepository userSubscribeRepository, ITelegramBotClient telegramClient)
        {
            _userRepository = userRepository;
            _subscribeRepository = subscribeRepository;
            _userSubscribeRepository = userSubscribeRepository;
            _telegramClient = telegramClient;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(BaseAddress);
            _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        }

        public async Task Test5()
        {
            List<User> users = await _userSubscribeRepository.GetUsersWithIntervalSubscriptions(5);
            string url = "/api/BinanceStatistic/GetInterval";
            HttpResponseMessage httpResponseMessage = await HttpClient.GetAsync(url);
            string responseJson = httpResponseMessage.Content.ReadAsStringAsync().Result;
            GetStatisticResponse responseModel = JsonSerializer.Deserialize<GetStatisticResponse>(responseJson, _options);

            string testStat =
                $"{responseModel.Statistic[0].Currency}\nLong - {responseModel.Statistic[0].Long}\nShort - {responseModel.Statistic[0].Short}";

            foreach (var user in users)
            {
                await _telegramClient.SendTextMessageAsync(user.TelegramId, testStat);
            }
            
            
        }
    }
}