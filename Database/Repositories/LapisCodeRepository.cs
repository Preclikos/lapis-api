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
    public class LapisCodeRepository : ILapisCodeRepository
    {
        readonly char[] replacers = new char[] { 'x', 'X', '#' };

        private readonly LapisDataContext _context;
        public LapisCodeRepository(LapisDataContext context)
        {
            _context = context;
        }

        public async Task<LapisCode> GetByLapisId(int id, CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = @"SELECT * FROM `LapisCodes` WHERE `LapisId`=@LapisId LIMIT 1";

                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<LapisCode>(new CommandDefinition(sql, new { LapisId = id }, cancellationToken: cancellationToken));
                connection.Close();

                return result;

            }
        }

        public async IAsyncEnumerable<LapisCode> GetIdAndCodeByCode(string country, string region, string user, string lapis, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using (var connection = _context.CreateMySqlConnection())
            {

                var builder = new SqlBuilder();

                var selector = builder.AddTemplate(@"SELECT * FROM `LapisCodes` /**where**/ LIMIT 20");

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

                var reader = await connection.ExecuteReaderAsync(new CommandDefinition(selector.RawSql, selector.Parameters, cancellationToken: cancellationToken));

                var parser = new ReaderParser<LapisCode>(reader);
                var enumerator = parser.GetAsyncEnumerator(cancellationToken);

                while (await enumerator.MoveNextAsync())
                {
                    yield return enumerator.Current;
                }

            }

        }

        private string ReplaceWithAny(string value)
        {
            foreach (var replacer in replacers)
            {
                value = value.Replace(replacer, '_');
            }
            return value;
        }

        private bool ContainsReplacer(string value)
        {

            foreach (var replacer in replacers)
            {
                if (value.Contains(replacer))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
