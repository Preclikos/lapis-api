using Dapper;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Databases;

namespace WebApi.Database.Repositories
{
    public class LapisRepository : ILapisRepository
    {

        private readonly LapisDataContext _context;
        public LapisRepository(LapisDataContext context)
        {
            _context = context;
        }


        public int Add(Lapis entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Lapis> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `Lapises` WHERE `Id`=@Id LIMIT 1";

                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Lapis>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
                connection.Close();

                return result;
            }
        }


        public int UpdateNameBySub(string sub, string name)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"UPDATE `Users` SET `Name`=@Name WHERE `Sub`=@sub";

                connection.Open();
                var result = connection.Execute(sql, new { Sub = sub, Name = name });
                connection.Close();

                return result;
            }
        }
    }
}
