using System.Collections.Generic;
using System.Threading.Tasks;
using BinanceStatistic.BinanceClient.Models;
using BinanceStatistic.BinanceClient.Models.Interfaces;

namespace BinanceStatistic.BinanceClient.Interfaces
{
    public interface IBinanceClient
    {
        Task<IEnumerable<BinanceCurrency>> GrabbCurrencies();
        
        Task<List<IBinanceTrader>> GrabbTraders(List<BinanceRequestTemplate> requests);

        Task<List<BinancePosition>> GrabbPositions(List<BinanceRequestTemplate> requests);
    }
}