using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Models.Interfaces;

namespace BinanceStatistic.Grabber.BLL.Services.Interfaces
{
    public interface IBinanceGrabberService
    {
        Task<IEnumerable<BinancePosition>> CreateStatistic();

        Task<List<IBinanceTrader>> GrabbTraders();

        Task<IEnumerable<BinanceCurrency>> GrabbCurrencies();
    }
}