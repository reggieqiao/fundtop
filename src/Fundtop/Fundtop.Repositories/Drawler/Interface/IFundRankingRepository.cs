using Fundtop.Models.Drawler;

namespace Fundtop.Repositories.Drawler.Interface
{
    public interface IFundRankingRepository
    {
        Task<int> Insert(List<FundRankingModel> funds);
    }
}
