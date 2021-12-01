using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BLL.ViewModels;
using BinanceStatistic.DAL.Entities;

namespace BinanceStatistic.BLL.Services.Interface
{
    public interface IBinanceService
    {
        Task<GetStatisticResponse> GetPositions();
        
        Task<GetStatisticResponse> GetPositionsWithInterval(int interval);
    }
}