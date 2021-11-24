using AutoMapper;
using BinanceStatistic.BLL.ViewModels;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.BLL.Config.MapperProfiles
{
    public class StatisticProfile : Profile
    {
        public StatisticProfile()
        {
            CreateMap<Position, PositionView>()
                .ForMember(to => to.Currency, from => from.MapFrom(source => source.Currency.Name));
        }
    }
}