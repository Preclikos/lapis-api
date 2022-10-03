using Dapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Databases;

namespace WebApi.Database.Repositories
{
    public class UserImageRepository : IUserImageRepository
    {
        private readonly LapisDataContext _context;
        public UserImageRepository(LapisDataContext context)
        {
            _context = context;
        }

        public async Task<UserImage> GetById(int id, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `UserImages` WHERE `Id`=@Id LIMIT 1";

                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<UserImage>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
                connection.Close();

                return result;

            }
        }

        public async Task<IEnumerable<UserImage>> GetById(IEnumerable<int> lapisId, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `UserImages` WHERE `Id` IN @Ids";

                connection.Open();
                var result = await connection.QueryAsync<UserImage>(new CommandDefinition(sql, new { Ids = lapisId }, cancellationToken: cancellationToken));
                connection.Close();

                return result;
            }
        }
    }
}
