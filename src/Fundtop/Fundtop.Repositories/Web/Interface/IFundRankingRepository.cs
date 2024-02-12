using Fundtop.Models.Web;

namespace Fundtop.Repositories.Web.Interface
{
    public interface IFundRankingRepository
    {
        Task<PagedResult<FundRankingDto>> GetFundListAsync(FundRankingModel model);
    }
}
