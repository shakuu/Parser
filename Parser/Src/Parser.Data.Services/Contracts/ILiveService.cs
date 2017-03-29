using Parser.Data.ViewModels.Live;

namespace Parser.Data.Services.Contracts
{
    public interface ILiveService
    {
        LiveStatisticsViewModel GetLiveStatisticsViewModel(string username);
    }
}
