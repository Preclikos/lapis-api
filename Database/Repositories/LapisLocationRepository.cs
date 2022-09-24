using Dapper;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Databases;

namespace WebApi.Database.Repositories
{
    public class LapisLocationRepository : ILapisLocationRepository
    {
        private readonly LapisDataContext _context;
        public LapisLocationRepository(LapisDataContext context)
        {
            _context = context;
        }

        public async Task<LapisLocation> GetLastByLapisId(int id, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `LapisLocations` WHERE `LapisId`=@LapisId ORDER BY `Id` DESC LIMIT 1";

                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<LapisLocation>(new CommandDefinition(sql, new { LapisId = id }, cancellationToken: cancellationToken));
                connection.Close();

                return result;

            }
        }
    }
}
