using Dapper;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Databases;

namespace WebApi.Database.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly LapisDataContext _context;
        public UserRepository(LapisDataContext context)
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
