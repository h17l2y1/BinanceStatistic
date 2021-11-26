using System.Net.Http;

namespace BinanceStatistic.BinanceClient.Models
{
    public class BinanceRequestTemplate
    {
        public BinanceRequestTemplate(string endpoint, StringContent content)
        {
            Endpoint = endpoint;
            Content = content;
        }

        public string Endpoint { get; set; }
        public StringContent Content { get; set; }
    }
}