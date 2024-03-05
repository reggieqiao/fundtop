using Dapper;
using Fundtop.Core;
using Fundtop.Models.Web;
using Fundtop.Repositories.Web.Interface;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Fundtop.Repositories.Web
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

        public async Task<PagedResult<FundRankingDto>> GetFundListAsync(FundRankingModel model)
        {
            var pageNumber = (model.PageIndex - 1) * model.PageSize;
            var sql = @"
                SELECT *, COUNT(1) OVER() AS total_count
                FROM fund_ranking
                WHERE DATE(created_at)=(SELECT DATE(MAX(created_at)) FROM fund_ranking)
                ORDER BY id
                LIMIT @PageSize OFFSET @PageNumber";
            var result = await _connection.QueryAsync<FundRankingEntity>(sql, new { PageNumber = pageNumber, PageSize = model.PageSize });
            var items = AutoMapperHelper.MapList<FundRankingEntity, FundRankingDto>(result);
            var totalCount = 0;
            if (result.Any())
            {
                var first = result.FirstOrDefault();
                if (first != null)
                {
                    totalCount = first.TotalCount;
                }
            }

            var pagedResult = new PagedResult<FundRankingDto> { 
                Items = items,
                TotalCount = totalCount,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize
            };
            return pagedResult;
        }
    }
}
