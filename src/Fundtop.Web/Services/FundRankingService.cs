using Fundtop.Models.Web;
using Fundtop.Repositories.Web.Interface;
using Fundtop.Web.Services.Interface;

namespace Fundtop.Web.Services
{
    public class FundRankingService : IFundRankingService
    {
        private readonly IFundRankingRepository _fundRankingRepository;

        public FundRankingService(IFundRankingRepository fundRankingRepository)
        {
            _fundRankingRepository = fundRankingRepository;
        }

        public async Task<PagedResult<FundRankingDto>> GetFundListAsync(FundRankingModel model)
        {
            var result = await _fundRankingRepository.GetFundListAsync(model);
            return result;
        }
    }
}
