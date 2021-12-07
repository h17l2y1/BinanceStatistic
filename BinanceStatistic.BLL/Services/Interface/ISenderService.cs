using System.Threading.Tasks;

namespace BinanceStatistic.BLL.Services.Interface
{
    public interface ISenderService
    {
        Task Send5MinutesUpdate();
        Task Send15MinutesUpdate();
        Task Send30MinutesUpdate();
        Task Send60MinutesUpdate();
    }
}