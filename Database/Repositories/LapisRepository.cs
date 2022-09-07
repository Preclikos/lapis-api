using Dapper;
using System.Collections.Generic;
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

        public User GetBySub(string sub)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `Users` WHERE `Sub`=@Sub LIMIT 1";

                connection.Open();
                var result = connection.QueryFirstOrDefault<User>(sql, new { Sub = sub });
                connection.Close();

                return result;
            }
        }

        public async IAsyncEnumerable<Lapis> SearchByCode(string code)
        {
            using (var connection = _context.CreateMySqlConnection())
            {

                var sql = @"SELECT * FROM `Lapises` WHERE `Name` LIKE '_r%'";
                var reader = await connection.ExecuteReaderAsync(sql, null);
                var parser = new ReaderParser<Lapis>(reader);
                var enumerator = parser.GetAsyncEnumerator();

                while (await enumerator.MoveNextAsync())
                {
                    await Task.Delay(1000);
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
