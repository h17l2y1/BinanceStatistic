using System.Collections.Generic;
using BinanceStatistic.Core.Models;

namespace BinanceStatistic.BLL.Helpers.Interfaces
{
    public interface IPositionHelper
    {
        List<Position> GetMocPositions();
    }
}