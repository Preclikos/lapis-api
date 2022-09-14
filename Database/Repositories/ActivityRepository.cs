using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Databases;

namespace WebApi.Database.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        const int feedLimit = 8;
        private readonly LapisDataContext _context;
        public ActivityRepository(LapisDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Activity>> GetLastActivities(int country, int offset, CancellationToken cancellationToken)
        {

            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `Activities` ORDER BY `Id` LIMIT @Limit OFFSET @Offset";

                connection.Open();
                var result = await connection.QueryAsync<Activity>(new CommandDefinition(sql, new { Offset = offset, Limit = feedLimit }, cancellationToken: cancellationToken));
                connection.Close();

                return result;
            }
        }
    }
}
