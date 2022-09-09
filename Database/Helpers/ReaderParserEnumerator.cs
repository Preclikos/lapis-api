using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Database.Helpers
{
    public class ReaderParserEnumerator<T> : IAsyncEnumerator<T>
    {
        public ReaderParserEnumerator(DbDataReader reader, CancellationToken cancellationToken)
        {
            this.cancellationToken = cancellationToken;
            Reader = reader;
            RowParser = reader.GetRowParser<T>();
        }
        public T Current => Reader.FieldCount == 0 ? default(T) : RowParser(Reader);
        private DbDataReader Reader { get; }
        private Func<DbDataReader, T> RowParser { get; }
        private CancellationToken cancellationToken { get; }
        public async ValueTask DisposeAsync()
        {
            await Reader.DisposeAsync();
        }
        public async ValueTask<bool> MoveNextAsync()
        {
            return await Reader.ReadAsync(cancellationToken);
        }
    }
}
