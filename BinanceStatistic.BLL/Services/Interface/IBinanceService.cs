using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.BLL.ViewModels;

namespace BinanceStatistic.BLL.Services.Interface
{
    public interface IBinanceService
    {
        Task<GetStatisticResponse> GetPositions();
        
        Task<List<PositionView>> GetPositionsWithInterval(int interval);

    }
}