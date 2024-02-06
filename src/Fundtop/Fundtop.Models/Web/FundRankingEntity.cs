using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fundtop.Models.Web
{
    [Table("fund_ranking")]
    public class FundRankingEntity
    {
        [Key]
        public int Id { get; set; }

        [Column("fund_code")]
        public string FundCode { get; set; }

        [Column("fund_shortname")]
        public string FundShortName { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("net_asset_value")]
        public decimal NetAssetValue { get; set; }

        [Column("cumulative_net_asset_value")]
        public decimal CumulativeNetAssetValue { get; set; }

        [Column("daily_growth_rate")]
        public decimal DailyGrowthRate { get; set; }

        [Column("one_week")]
        public decimal OneWeek { get; set; }

        [Column("one_month")]
        public decimal OneMonth { get; set; }

        [Column("three_month")]
        public decimal ThreeMonth { get; set; }

        [Column("six_month")]
        public decimal SixMonth { get; set; }

        [Column("one_year")]
        public decimal OneYear { get; set; }

        [Column("two_year")]
        public decimal TwoYear { get; set; }

        [Column("three_year")]
        public decimal ThreeYear { get; set; }

        [Column("year_to_date")]
        public decimal YearToDate { get; set; }

        [Column("since_inception")]
        public decimal SinceInception { get; set; }

        [Column("inception_date")]
        public DateTime? InceptionDate { get; set; }

        [Column("transaction_fee")]
        public string TransactionFee { get; set; }

        [Column("total_count")]
        public int TotalCount { get; set; }
    }
}