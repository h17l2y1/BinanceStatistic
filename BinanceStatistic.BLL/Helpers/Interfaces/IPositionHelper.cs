using System.Collections.Generic;
using BinanceStatistic.BinanceClient.Models;

namespace BinanceStatistic.BLL.Helpers.Interfaces
{
    public interface IPositionHelper
    {
        List<BinancePosition> GetMocPositions();
    }
}