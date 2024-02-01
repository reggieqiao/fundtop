using Dapper;
using Fundtop.Models.Drawler;
using Fundtop.Repositories.Drawler.Interface;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Fundtop.Repositories.Drawler
{
    public class FundRankingRepository : IFundRankingRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public FundRankingRepository(IConfiguration configuration, DataContext connection)
        {
            _configuration = configuration;
            _connection = connection.GetConnection();
        }

        public async Task<int> Insert(List<FundRankingModel> funds)
        {
            var result = 0;

            var sql = @"
                INSERT INTO fund_ranking (fund_code, fund_shortname, date, net_asset_value, cumulative_net_asset_value, daily_growth_rate, one_week, one_month, three_month, six_month, one_year, two_year, three_year, year_to_date, since_inception, inception_date, transaction_fee)
                VALUES (@FundCode, @FundShortName, @Date, @NetAssetValue, @CumulativeNetAssetValue, @DailyGrowthRate, @OneWeek, @OneMonth, @ThreeMonth, @SixMonth, @OneYear, @TwoYear, @ThreeYear, @YearToDate, @SinceInception, @InceptionDate, @TransactionFee)";

            var batchSize = 100;
            for (int i = 0; i < funds.Count(); i += batchSize)
            {
                var batchFunds = funds.Skip(i).Take(batchSize);
                result += await _connection.ExecuteAsync(sql, batchFunds);
            }

            return result;
        }
    }
}
