using System;
using System.Collections.Generic;
using BinanceStatistic.BinanceClient.Models;

namespace BinanceStatistic.BinanceClient.Views.Response
{
    public class OtherPositionResponse: BaseResponse
    {
        public BinancePositionModel Data { get; set; }
    }
    
    public class BinancePositionModel
    {
        public IEnumerable<BinancePosition> OtherPositionRetList { get; set; }
        public IEnumerable<int> UpdateTime { get; set; }
        // public long UpdateTimeStamp { get; set; }
    }
}