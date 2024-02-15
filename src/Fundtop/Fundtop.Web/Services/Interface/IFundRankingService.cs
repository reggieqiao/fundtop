using Fundtop.Models.Web;

namespace Fundtop.Web.Services.Interface
{
    public interface IFundRankingService
    {
        Task<PagedResult<FundRankingDto>> GetFundListAsync(FundRankingModel model);
    }
}
