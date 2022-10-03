using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Databases;

namespace WebApi.Database.Repositories
{
    public class LapisImageRepository : ILapisImageRepository
    {
        private readonly LapisDataContext _context;
        public LapisImageRepository(LapisDataContext context)
        {
            _context = context;
        }

        public async Task<LapisImage> GetById(int id, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `LapisImages` WHERE `Id`=@Id LIMIT 1";

                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<LapisImage>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
                connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<LapisImage>> GetById(IEnumerable<int> lapisId, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `LapisImages` WHERE `Id` IN @Ids";

                connection.Open();
                var result = await connection.QueryAsync<LapisImage>(new CommandDefinition(sql, new { Ids = lapisId }, cancellationToken: cancellationToken));
                connection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<LapisImage>> GetAllByLapisId(int lapisId, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `LapisImages` WHERE `LapisId`=@LapisId LIMIT 1";

                connection.Open();
                var result = await connection.QueryAsync<LapisImage>(new CommandDefinition(sql, new { LapisId = lapisId }, cancellationToken: cancellationToken));
                connection.Close();

                return result;
            }
        }

    }
}
