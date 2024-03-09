using Fundtop.Models.Web;
using Fundtop.Web.Controllers;
using Fundtop.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Fundtop.Web.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public async Task FundListAsync_Returns_Ok()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var fundRankingServiceMock = new Mock<IFundRankingService>();
            var model = new FundRankingModel(); // Create an instance of FundRankingModel

            // Mock the behavior of GetFundListAsync method
            fundRankingServiceMock.Setup(x => x.GetFundListAsync(model)).ReturnsAsync(new PagedResult<FundRankingDto>());

            var controller = new HomeController(loggerMock.Object, fundRankingServiceMock.Object);

            // Act
            var result = await controller.FundListAsync(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult); // Check if the result is of type OkObjectResult
        }
    }
}