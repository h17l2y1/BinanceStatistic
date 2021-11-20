using System.Collections.Generic;

namespace BinanceStatistic.Core.Views.Response
{
    public class BaseResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        public bool Success { get; set; }
    }
}