using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Views.Request;

namespace BinanceStatistic.BLL.Services.Interface
{
    public interface IBinanceGrabberService
    {
        Task<List<OtherPositionRequest>> GetAllTraders();

        List<BinancePosition> GetPositions(List<OtherPositionRequest> requests);

        Task<List<BinancePosition>> GetPositions();
    }
}