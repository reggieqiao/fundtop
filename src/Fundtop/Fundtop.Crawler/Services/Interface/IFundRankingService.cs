using Fundtop.Models.Drawler;

namespace Fundtop.Crawler.Services.Interface
{
    public interface IFundRankingService
    {
        Task FundRankingAsync(CrawlerConfig config);

        Task<List<FundRankingModel>> GetFundRankingAsync(string url, int pageIndex, int pageSize);
    }
}
