using Fundtop.Crawler.Services;
using Fundtop.Models.Drawler;
using Fundtop.Repositories.Drawler.Interface;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fundtop.Crawler.Tests
{
    [TestClass]
    public class FundRankingServiceTest
    {
        [TestMethod]
        public async Task FundRankingAsync_WhenNoFundsExist_InsertsFunds()
        {
            // create mock object
            var mockLogger = new Mock<ILogger<Worker>>();
            var mockHttpClient = new Mock<HttpClient>();
            var mockFundRankingRepository = new Mock<IFundRankingRepository>();

            // simulate GetCount return 0
            mockFundRankingRepository.Setup(repo => repo.GetCount()).Returns(Task.FromResult(0));

            // create a service using mock object
            var service = new FundRankingService(mockLogger.Object, mockHttpClient.Object, mockFundRankingRepository.Object);

            // create CrawlerConfig object
            var config = new CrawlerConfig
            {
                Url = "https://fund.eastmoney.com/data/rankhandler.aspx?op=ph&dt=kf&ft=all&rs=&gs=0&sc=1nzf&st=desc&sd={0}&ed={1}&qdii=&tabSubtype=,,,,,&pi={2}&pn={3}&dx=1&v={4}",
                PageIndex = 1,
                PageSize = 10,
                PageCount = 2
            };

            // call method under test
            await service.FundRankingAsync(config);

            // verify that the IFundRankingRepository.Insert method is called
            mockFundRankingRepository.Verify(repo => repo.Insert(It.IsAny<List<FundRankingModel>>()), Times.Once);
        }
    }
}