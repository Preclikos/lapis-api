using Dapper;
using System;
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


        private char[] replacers = new char[] { 'x', 'X', '#' };

        private string ReplaceWithAny(string value)
        {
            foreach(var replacer in replacers)
            {
                value = value.Replace(replacer, '_');
            }
            return value;
        }

        private bool ContainsReplacer(string value)
        {
            
            foreach (var replacer in replacers)
            {
                if(value.Contains(replacer))
                {
                    return true;
                }
            }
            return false;
        }

        public async IAsyncEnumerable<LapisCode> GetIdAndCodeByCode(string country, string region, string user, string lapis, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateMySqlConnection())
            {

                var builder = new SqlBuilder();

                var selector = builder.AddTemplate(@"SELECT * FROM `LapisCodes` /**where**/"); //`Country` = @Country AND `Region` = @Region AND `User` = @User AND `Lapis` = @Lapis";)

                if (!String.IsNullOrEmpty(country))
                {
                    if (ContainsReplacer(country))
                    {
                        builder.Where("Country LIKE @Country", new { Country = ReplaceWithAny(country) });
                    }
                    else
                    {
                        builder.Where("Country = @Country", new { Country = country });
                    }
                }

                if (!String.IsNullOrEmpty(region))
                {
                    if (ContainsReplacer(region))
                    {
                        builder.Where("Region LIKE @Region", new { Region = ReplaceWithAny(region) });
                    }
                    else
                    {
                        builder.Where("Region = @Region", new { Region = region });
                    }
                }

                if (!String.IsNullOrEmpty(user))
                {
                    if (ContainsReplacer(user))
                    {
                        builder.Where("User LIKE @User", new { User = ReplaceWithAny(user) });
                    }
                    else
                    {
                        builder.Where("User = @User", new { User = user });
                    }
                }

                if (!String.IsNullOrEmpty(lapis))
                {
                    if (ContainsReplacer(lapis))
                    {
                        builder.Where("Lapis LIKE @Lapis", new { Lapis = ReplaceWithAny(lapis) });
                    }
                    else
                    {
                        builder.Where("Lapis = @Lapis", new { Lapis = lapis });
                    }
                }

                Console.WriteLine(selector.ToString());

                var reader = await connection.ExecuteReaderAsync(new CommandDefinition(selector.RawSql, selector.Parameters, cancellationToken: cancellationToken));

                var parser = new ReaderParser<LapisCode>(reader);
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
