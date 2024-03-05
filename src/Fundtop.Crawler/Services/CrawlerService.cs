using Fundtop.Crawler.Services.Interface;

namespace Fundtop.Crawler.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly IFundRankingService _fundRankingService;

        public CrawlerService(IFundRankingService fundRankingService)
        {
            _fundRankingService = fundRankingService;
        }

        public async Task FetchDataAsync(CrawlerConfig config)
        {
            await _fundRankingService.FundRankingAsync(config);
        }
    }
}
