using System.Collections.Generic;
using System.Data.Common;
using System.Threading;

namespace WebApi.Database.Helpers
{
    public class ReaderParser<T> : IAsyncEnumerable<T>
    {
        public ReaderParser(DbDataReader reader)
        {
            Reader = reader;
        }
        private DbDataReader Reader { get; }
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new ReaderParserEnumerator<T>(Reader, cancellationToken);
        }
    }
}
