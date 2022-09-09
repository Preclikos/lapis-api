using Dapper;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Helpers;
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

        public int Add(User entity)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"INSERT INTO `Users`(`Sub`, `Name`) VALUES (@Sub,@Name)";

                connection.Open();
                var result = connection.Execute(sql, entity);
                connection.Close();

                return result;
            }
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


        public async IAsyncEnumerable<int> GetIdByCode(int country, int region, int user, int lapis, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateMySqlConnection())
            {

                var values = new { Country = country, Region = region, User = user, Lapis = lapis };

                var sql = @"SELECT LapisId FROM `LapisCodes` WHERE `Country` = @Country AND `Region` = @Region AND `User` = @User AND `Lapis` = @Lapis";
                var reader = await connection.ExecuteReaderAsync(new CommandDefinition(sql, values, cancellationToken: cancellationToken));

                var parser = new ReaderParser<int>(reader);
                var enumerator = parser.GetAsyncEnumerator(cancellationToken);

                while (await enumerator.MoveNextAsync())
                {
                    yield return enumerator.Current;
                }

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
