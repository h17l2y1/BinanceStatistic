using System;
using System.Net;
using BinanceStatistic.BinanceClient.Views.Response;

namespace BinanceStatistic.BinanceClient.Models
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