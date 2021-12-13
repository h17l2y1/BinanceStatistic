using System.Threading.Tasks;

namespace BinanceStatistic.BLL.Services.Interface
{
    public interface ISenderService
    {
        Task Send5MinutesUpdate();
    }
}