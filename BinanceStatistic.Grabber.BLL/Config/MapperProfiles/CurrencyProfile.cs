using AutoMapper;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.Grabber.BLL.Config.MapperProfiles
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<BinanceCurrency, Currency>()
                .ForMember(to => to.Name, from => from.MapFrom(source => source.Pair));
        }
    }
}