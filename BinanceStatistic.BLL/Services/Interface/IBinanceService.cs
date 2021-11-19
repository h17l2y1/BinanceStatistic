using System.Threading.Tasks;
using BinanceStatistic.Core.Views.Response;

namespace BinanceStatistic.BLL.Services.Interface
{
    public interface IBinanceService
    {
        Task<SearchFeaturedTraderResponse> Test();
    }
}