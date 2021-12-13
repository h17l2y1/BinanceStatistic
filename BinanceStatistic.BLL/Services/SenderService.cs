using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.BLL.ViewModels;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BinanceStatistic.BLL.Services
{
public class SenderService : ISenderService
    {
        private readonly IUserSubscribeRepository _userSubscribeRepository;
        private readonly IMapper _mapper;
        private readonly IBinanceService _binanceService;
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;
        
        public SenderService(UserSubscribeRepository userSubscribeRepository, IConfiguration configuration,
            IMapper mapper, IBinanceService binanceService)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration["TelegramApi"]);
            _userSubscribeRepository = userSubscribeRepository;
            _mapper = mapper;
            _binanceService = binanceService;
            _endpoint = configuration["TelegramApiEndpoint"];
        }
        
        public async Task Send5MinutesUpdate()
        {
            // int interval = 5;
            // List<User> users = await _userSubscribeRepository.GetUsersWithIntervalSubscriptions(interval);
            // List<PositionView> statisticView = await _binanceService.GetPositionsWithInterval2(interval);
            // var usersView = _mapper.Map<List<UserView>>(users);
            // var request = new SendStatisticRequest(statisticView, usersView);
            //
            // await SendToTelegram(request);
        }

        // private async Task SendToTelegram(SendStatisticRequest request)
        // {
        //     string requestJson = JsonConvert.SerializeObject(request);
        //     var stringContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
        //     await _httpClient.PostAsync(_endpoint, stringContent);
        // }
    }
}