namespace Fundtop.Crawler.Services.Interface
{
    public interface ICrawlerService
    {
        Task FetchDataAsync(CrawlerConfig config);
    }
}
