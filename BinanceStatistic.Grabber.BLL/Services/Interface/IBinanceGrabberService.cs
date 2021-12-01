using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Models.Interfaces;

namespace BinanceStatistic.Grabber.BLL.Services.Interface
{
    public interface IBinanceGrabberService
    {
        Task CreateStatistic();
        
        Task<IEnumerable<BinanceCurrency>> GrabbCurrencies();
        
        Task<List<BinancePosition>> GrabbAll();

        Task<List<IBinanceTrader>> GrabbTraders();
        
        Task<List<BinancePosition>> GrabbPositions(List<IBinanceTrader> traders);
    }
}