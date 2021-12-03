using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.BLL.ViewModels;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace BinanceStatistic.BLL.Services
{
    public class BinanceService : IBinanceService
    {
        private readonly IMapper _mapper;
        private readonly IPositionRepository _positionRepository;
        private readonly ILogger<BinanceService> _logger;

        public BinanceService(IMapper mapper,
            IPositionRepository positionRepository, ILogger<BinanceService> logger)
        {
            _mapper = mapper;
            _positionRepository = positionRepository;
            _logger = logger;
        }

        public async Task<GetStatisticResponse> GetPositions()
        {
            IEnumerable<Position> positions = await _positionRepository.GetAll();
            IEnumerable<PositionView> statisticResponse = _mapper.Map<IEnumerable<PositionView>>(positions);
            var response = new GetStatisticResponse(statisticResponse);
            return response;
        }
        
        public async Task<GetStatisticResponse> GetPositionsWithInterval(int interval)
        {
            DateTime lastUpdate = await _positionRepository.GetLastUpdate();

            List<Position> positions = await _positionRepository.GetWithInterval(lastUpdate, interval);

            List<PositionView> view = positions.GroupBy(g => g.CurrencyId)
                .Select(s =>
                {
                    var positions = s.ToArray();

                    var position = new PositionView();
                    position.Currency = positions[0].Currency.Name;
                    
                    position.Long = positions.Length == 2 ? positions[1].Long : positions[0].Long;
                    position.Short = positions.Length == 2 ? positions[1].Short : positions[0].Short;
                    position.Count = positions.Length == 2 ? positions[1].Count : positions[0].Count;
                    
                    position.LongDiff = positions.Length == 2 ? GetDiff(positions[0].Long, positions[1].Long) : 0;
                    position.ShortDiff = positions.Length == 2 ? GetDiff(positions[0].Short, positions[1].Short) : 0;
                    position.CountDiff = positions.Length == 2 ? GetDiff(positions[0].Count, positions[1].Count) : 0;
                    
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