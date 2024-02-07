namespace Fundtop.Models.Web
{
    public class FundRankingDto
    {
        public string FundCode { get; set; }

        public string FundShortName { get; set; }

        public DateTime? Date { get; set; }

        public decimal NetAssetValue { get; set; }

        public decimal CumulativeNetAssetValue { get; set; }

        public decimal DailyGrowthRate { get; set; }

        public decimal OneWeek { get; set; }

        public decimal OneMonth { get; set; }

        public decimal ThreeMonth { get; set; }

        public decimal SixMonth { get; set; }

        public decimal OneYear { get; set; }

        public decimal TwoYear { get; set; }

        public decimal ThreeYear { get; set; }

        public decimal YearToDate { get; set; }

        public decimal SinceInception { get; set; }

        public string TransactionFee { get; set; }
    }
}