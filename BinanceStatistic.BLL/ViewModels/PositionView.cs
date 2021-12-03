namespace BinanceStatistic.BLL.ViewModels
{
    public class PositionView
    {
        public string Currency { get; set; }
        public int Count { get; set; }
        public int Short { get; set; }
        public int Long { get; set; }
        public int CountDiff { get; set; }
        public int ShortDiff { get; set; }
        public int LongDiff { get; set; }
    }
}