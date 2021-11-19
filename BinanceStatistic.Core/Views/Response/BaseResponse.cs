using System.Collections.Generic;
using Newtonsoft.Json;

namespace BinanceStatistic.Core.Views.Response
{
    // public class BaseResponse<T>
    // {
    //     public string Code { get; set; }
    //     public string Message { get; set; }
    //     public string MessageDetail { get; set; }
    //     public IEnumerable<T> Data { get; set; }
    //     public bool Success { get; set; }
    // }
    
    public class BaseResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        // public IEnumerable<T> Data { get; set; }
        public bool Success { get; set; }
    }
}