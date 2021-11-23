using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Views.Response;

namespace BinanceStatistic.BLL.Services.Interface
{
    public interface IBinanceService
    {
        Task<SearchFeaturedTraderResponse> Test();
    }
}