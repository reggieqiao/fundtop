using Fundtop.Crawler.Services.Interface;
using Fundtop.Models.Drawler;
using Fundtop.Repositories.Drawler.Interface;
using System.Text.RegularExpressions;

namespace Fundtop.Crawler.Services
{
    public class FundRankingService : IFundRankingService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly IFundRankingRepository _fundRankingRepository;

        public FundRankingService(ILogger<Worker> logger, HttpClient httpClient, IFundRankingRepository fundRankingRepository)
        {
            _logger = logger;
            _httpClient = httpClient;
            _fundRankingRepository = fundRankingRepository;
        }

        public async Task FundRankingAsync(CrawlerConfig config)
        {
            var fundsCount = await _fundRankingRepository.GetCount();
            if (fundsCount == 0)
            {
                var allFunds = new List<FundRankingModel>();
                for (int i = 0; i < config.PageCount; i++)
                {
                    var funds = await GetFundRankingAsync(config.Url, config.PageIndex + i, config.PageSize);
                    allFunds.AddRange(funds);
                }

                await _fundRankingRepository.Insert(allFunds);
            }
        }

        public async Task<List<FundRankingModel>> GetFundRankingAsync(string url, int pageIndex, int pageSize)
        {
            var funds = new List<FundRankingModel>();

            try
            {
                if (!_httpClient.DefaultRequestHeaders.Contains("Referer"))
                {
                    _httpClient.DefaultRequestHeaders.Add("Referer", "https://fund.eastmoney.com/data/fundranking.html");
                }
                if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
                {
                    _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
                }

                var startDate = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
                var endDate = DateTime.Now.ToString("yyyy-MM-dd");
                var randomValue = new Random().NextDouble().ToString("F17");
                url = string.Format(url, startDate, endDate, pageIndex, pageSize, randomValue);
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    funds = ParseData(content);
                }
                else
                {
                    _logger.LogError(endDate, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return funds;
        }

        private List<FundRankingModel> ParseData(string content)
        {
            var funds = new List<FundRankingModel>();

            try
            {
                var match = Regex.Match(content, @"var rankData = {datas:\[(.*?)\],");
                if (match.Success)
                {
                    var datasString = match.Groups[1].Value;
                    var datas = datasString.Split(new[] { "\",\"" }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < datas.Length; i++)
                    {
                        datas[i] = datas[i].Trim('\"');
                        var data = datas[i].Split(',');
                        var fundData = new FundRankingModel
                        {
                            FundCode = data[0],
                            FundShortName = data[1],
                            Date = string.IsNullOrEmpty(data[3]) ? null : Convert.ToDateTime(data[3]),
                            NetAssetValue = string.IsNullOrEmpty(data[4]) ? 0 : Convert.ToDecimal(data[4]),
                            CumulativeNetAssetValue = string.IsNullOrEmpty(data[5]) ? 0 : Convert.ToDecimal(data[5]),
                            DailyGrowthRate = string.IsNullOrEmpty(data[6]) ? 0 : Convert.ToDecimal(data[6]),
                            OneWeek = string.IsNullOrEmpty(data[7]) ? 0 : Convert.ToDecimal(data[7]),
                            OneMonth = string.IsNullOrEmpty(data[8]) ? 0 : Convert.ToDecimal(data[8]),
                            ThreeMonth = string.IsNullOrEmpty(data[9]) ? 0 : Convert.ToDecimal(data[9]),
                            SixMonth = string.IsNullOrEmpty(data[10]) ? 0 : Convert.ToDecimal(data[10]),
                            OneYear = string.IsNullOrEmpty(data[11]) ? 0 : Convert.ToDecimal(data[11]),
                            TwoYear = string.IsNullOrEmpty(data[12]) ? 0 : Convert.ToDecimal(data[12]),
                            ThreeYear = string.IsNullOrEmpty(data[13]) ? 0 : Convert.ToDecimal(data[13]),
                            YearToDate = string.IsNullOrEmpty(data[14]) ? 0 : Convert.ToDecimal(data[14]),
                            SinceInception = string.IsNullOrEmpty(data[15]) ? 0 : Convert.ToDecimal(data[15]),
                            InceptionDate = string.IsNullOrEmpty(data[16]) ? null : Convert.ToDateTime(data[16]),
                            TransactionFee = data[20]
                        };
                        funds.Add(fundData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return funds;
        }
    }
}
