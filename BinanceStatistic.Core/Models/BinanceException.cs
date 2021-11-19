using System;
using System.Net;
using BinanceStatistic.Core.Views.Response;

namespace BinanceStatistic.Core.Models
{
    public class BinanceException : Exception
    {
        public BinanceException()
        {
        }

        public BinanceException(string message) : base(message)
        {
        }

        public BinanceException(string message, Exception innerException) : base(message, innerException)
        {
        }    
        
        public BinanceException(HttpStatusCode code, string message, BaseResponse binanceException)
        {
        }   
    }
}