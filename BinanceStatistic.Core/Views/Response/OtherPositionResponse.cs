using System;
using System.Collections.Generic;
using BinanceStatistic.Core.Models;

namespace BinanceStatistic.Core.Views.Response
{
    public class OtherPositionResponse: BaseResponse
    {
        public PositionModel Data { get; set; }
    }
    
    public class PositionModel
    {
        public IEnumerable<Position> OtherPositionRetList { get; set; }
        public IEnumerable<int> UpdateTime { get; set; }
        public long UpdateTimeStamp { get; set; }
    }
}