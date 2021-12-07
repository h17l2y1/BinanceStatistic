using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.BLL.ViewModels;

namespace BinanceStatistic.BLL.Services.Interface
{
    public interface IBinanceService
    {
        Task<GetStatisticResponse> GetPositions();

        Task<GetStatisticResponse> GetPositionsWithInterval(int interval);
        
        Task<List<PositionView>> GetPositionsWithInterval2(int interval);

    }
}