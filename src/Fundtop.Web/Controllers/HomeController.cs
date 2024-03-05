using Fundtop.Models.Web;
using Fundtop.Web.Models;
using Fundtop.Web.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Fundtop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFundRankingService _fundRankingService;

        public HomeController(ILogger<HomeController> logger, IFundRankingService fundRankingService)
        {
            _logger = logger;
            _fundRankingService = fundRankingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> FundListAsync(FundRankingModel model)
        {
            var result = await _fundRankingService.GetFundListAsync(model);
            return Ok(ApiResponse<PagedResult<FundRankingDto>>.Success(result));
        }
    }
}