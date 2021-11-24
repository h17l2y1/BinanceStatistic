using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BinanceStatistic.BLL.Services.Interface;
using BinanceStatistic.BinanceClient.Enums;
using BinanceStatistic.BinanceClient.Interfaces;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Views.Request;
using BinanceStatistic.BLL.ViewModels;
using BinanceStatistic.DAL.Entities;
using BinanceStatistic.DAL.Repositories.Interfaces;

namespace BinanceStatistic.BLL.Services
{
    public class BinanceService : IBinanceService
    {
        private readonly IBinanceClient _client;
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IBinanceGrabberService _grabberService;

        public BinanceService(IBinanceClient client, IMapper mapper, ICurrencyRepository currencyRepository,
            IPositionRepository positionRepository, IBinanceGrabberService grabberService)
        {
            _client = client;
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _positionRepository = positionRepository;
            _grabberService = grabberService;
        }

        public async Task CreateCurrencies()
        {
            IEnumerable<BinanceCurrency> binanceCurrencies = await _client.GetCurrencies();
            var currencies = _mapper.Map<IEnumerable<Currency>>(binanceCurrencies);
            await _currencyRepository.Create(currencies);
        }
        
        public async Task CreatePositions()
        {
            List<BinancePosition> binancePositions = await _grabberService.GetPositions();
            List<Position> positions = await CreateStatistic(binancePositions);
            await _positionRepository.Create(positions);
        }
        
        public async Task<GetStatisticResponse> GetPositions()
        {
            List<Position> positions = await _positionRepository.GetAll();
            List<PositionView> statisticResponse = _mapper.Map<List<PositionView>>(positions);
            var response = new GetStatisticResponse(statisticResponse);
            return response;
        }
        
        public async Task<List<Position>> CreateStatistic(IEnumerable<BinancePosition> positions)
        {
            List<Currency> currencies = await _currencyRepository.GetAll();
            
            // for filter
            DateTime someDay = new DateTime(2021, 11, 24);

            var groupedPositions = positions.Where(w=>w.FormattedUpdateTime.Day == someDay.Day)
                                                                   .GroupBy(g => g.Symbol);

            List<Position> statistic = groupedPositions.Select(s =>
                {
                    int longPos = s.Count(c => c.Amount > 0);
                    int shortPos = s.Count(c => c.Amount < 0);
                    int totalPos = s.Count();

                    return new Position
                    {
                        CurrencyId = currencies.FirstOrDefault(prop => prop.Name == s.Key)?.Id,
                        Count = totalPos,
                        Short = shortPos,
                        Long = longPos,
                        Amount = s.Sum(f => f.Amount)
                    };
                }).Where(w => w.Count > 0)
                // .OrderByDescending(o => o.Count)
                .ToList();

            return statistic;
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
                    
                    position.Long = positions.Length == 2 ? GetDiff(positions[0].Long, positions[1].Long) : 0;
                    position.Short = positions.Length == 2 ? GetDiff(positions[0].Short, positions[1].Short) : 0;
                    position.Count = positions.Length == 2 ? GetDiff(positions[0].Count, positions[1].Count) : 0;

                    // position.LongWasNowDiff = positions.Length == 2 ? $"Was: {positions[0].Long} Now {positions[1].Long} Diff {GetDiff(positions[0].Long, positions[1].Long)}" : "0";
                    // position.ShortWasNowDiff = positions.Length == 2 ? $"Was: {positions[0].Short} Now {positions[1].Short} Diff {GetDiff(positions[0].Short, positions[1].Short)}" : "0";
                    // position.CountWasNowDiff = positions.Length == 2 ? $"Was: {positions[0].Count} Now {positions[1].Count} Diff {GetDiff(positions[0].Count, positions[1].Count)}" : "0";

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
