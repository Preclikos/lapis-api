using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILapisRepository lapisRepository;
        public SearchService(ILapisRepository lapisRepository)
        {
            this.lapisRepository = lapisRepository;
        }

        public async IAsyncEnumerable<Lapis> GetLapisesByCode(string code, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const string pattern = @"[^\dxX#]+";
            var output = Regex.Split(code, pattern);
            if (output.Length > 3)
            {
                var country = output[0];
                var region = output[1];
                var user = output[2];
                var lapisId = output[3];

                var searchResult = lapisRepository.GetIdByCode(country, region, user, lapisId, cancellationToken);
                var enumerator = searchResult.GetAsyncEnumerator(cancellationToken);

                while (await enumerator.MoveNextAsync())
                {
                    var lapis = await lapisRepository.GetByIdAsync(enumerator.Current, cancellationToken);
                    lapis.Code = country + "/" + region + "/" + user + "/" + lapisId;
                    yield return lapis;
                }
            }
        }
    }
}
