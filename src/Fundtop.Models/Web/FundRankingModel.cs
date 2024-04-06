namespace Fundtop.Models.Web
{
    public class FundRankingModel
    {
        public string Keywords { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 100;
    }
}