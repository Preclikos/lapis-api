using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Databases;

namespace WebApi.Database.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly LapisDataContext _context;
        public ImageRepository(LapisDataContext context)
        {
            _context = context;
        }

        public async Task<Image> GetById(int id, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `Images` WHERE `Id`=@Id LIMIT 1";

                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Image>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
                connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<Image>> GetById(IEnumerable<int> lapisId, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `Images` WHERE `Id` IN @Ids";

                connection.Open();
                var result = await connection.QueryAsync<Image>(new CommandDefinition(sql, new { Ids = lapisId }, cancellationToken: cancellationToken));
                connection.Close();

                return result;
            }
        }

        public async Task<IEnumerable<Image>> GetAllByLapisId(int lapisId, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `Images` WHERE `LapisId`=@LapisId LIMIT 1";

                connection.Open();
                var result = await connection.QueryAsync<Image>(new CommandDefinition(sql, new { LapisId = lapisId }, cancellationToken: cancellationToken));
                connection.Close();

                return result;
            }
        }

    }
}
