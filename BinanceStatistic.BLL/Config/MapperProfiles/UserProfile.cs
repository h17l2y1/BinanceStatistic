using AutoMapper;
using BinanceStatistic.BLL.ViewModels;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.BLL.Config.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserView>();
        }
    }
}