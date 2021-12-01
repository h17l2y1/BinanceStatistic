using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BLL.ViewModels;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace BinanceStatistic.BLL.Services
{
    public class BinanceService : IBinanceService
    {
        private readonly IBinanceClient _client;
        private readonly IMapper _mapper;
        private readonly IStatisticRepository _statisticRepository;
        private readonly ILogger<BinanceService> _logger;

        public BinanceService(IBinanceClient client, IMapper mapper,
            IStatisticRepository statisticRepository, ILogger<BinanceService> logger)
        {
            _client = client;
            _mapper = mapper;
            _statisticRepository = statisticRepository;
            _logger = logger;
        }
        
        public async Task<GetStatisticResponse> GetPositions()
        {
            List<Statistic> statistics = await _statisticRepository.GetAll();
            List<PositionView> statisticResponse = _mapper.Map<List<PositionView>>(statistics);
            var response = new GetStatisticResponse(statisticResponse);
            return response;
        }
        
        public async Task<GetStatisticResponse> GetPositionsWithInterval(int interval)
        {
            DateTime lastUpdate = await _statisticRepository.GetLastUpdate();

            List<Statistic> positions = await _statisticRepository.GetWithInterval(lastUpdate, interval);

            List<PositionView> view = positions.GroupBy(g => g.CurrencyId)
                .Select(s =>
                {
                    var positions = s.ToArray();

                    var position = new PositionView();
                    position.Currency = positions[0].Currency.Name;

                    position.Long = positions.Length == 2 ? GetDiff(positions[0].Long, positions[1].Long) : 0;
                    position.Short = positions.Length == 2 ? GetDiff(positions[0].Short, positions[1].Short) : 0;
                    position.Count = positions.Length == 2 ? GetDiff(positions[0].Count, positions[1].Count) : 0;

                    position.LongWasNowDiff = positions.Length == 2 ? $"Was: {positions[0].Long} Now {positions[1].Long} Diff {GetDiff(positions[0].Long, positions[1].Long)}" : "0";
                    position.ShortWasNowDiff = positions.Length == 2 ? $"Was: {positions[0].Short} Now {positions[1].Short} Diff {GetDiff(positions[0].Short, positions[1].Short)}" : "0";
                    position.CountWasNowDiff = positions.Length == 2 ? $"Was: {positions[0].Count} Now {positions[1].Count} Diff {GetDiff(positions[0].Count, positions[1].Count)}" : "0";

                    return position;
                })
                .ToList();

            List<PositionView> statisticResponse = _mapper.Map<List<PositionView>>(view);
            var response = new GetStatisticResponse(statisticResponse);
            return response;
        }

        private int GetDiff(int first, int second)
        {
            int diff = 0;

            if (first > second)
            {
                diff = second - first;
            }

            if (first < second)
            {
                diff = second - first;
            }

            return diff;
        }
    }
}