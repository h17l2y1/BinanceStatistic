using AutoMapper;

namespace BinanceStatistic.BLL.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // CreateMap<SignUpAccountView, User>()
            // 	.ForMember(from => from.UserName, to => to.MapFrom(source => source.Login))
            // 	.ForMember(from => from.Password, to => to.MapFrom(source => source.Password));

            // CreateMap<League, LeagueItem>().ReverseMap();
            // CreateMap<Team, TeamItem>().ReverseMap();
            // CreateMap<Player, PlayerItem>().ReverseMap();
        }
    }
}