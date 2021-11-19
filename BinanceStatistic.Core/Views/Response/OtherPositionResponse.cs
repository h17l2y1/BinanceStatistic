using System;
using System.Collections.Generic;
using BinanceStatistic.Core.Models;

namespace BinanceStatistic.Core.Views.Response
{
    public class OtherPositionResponse: BaseResponse
    {
        public IEnumerable<Position> OtherPositionRetList { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateTimeStamp { get; set; }
    }
}