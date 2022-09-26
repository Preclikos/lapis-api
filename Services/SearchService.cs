using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILapisRepository lapisRepository;
        private readonly ILapisCodeRepository lapisCodeRepository;
        private readonly ILapisImageRepository imageRepository;
        public SearchService(ILapisRepository lapisRepository, ILapisCodeRepository lapisCodeRepository, ILapisImageRepository imageRepository)
        {
            this.lapisRepository = lapisRepository;
            this.lapisCodeRepository = lapisCodeRepository;
            this.imageRepository = imageRepository;
        }

        public async IAsyncEnumerable<SearchItem> GetLapisesByCode(string code, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            const string pattern = @"[^\dxX#]+";
            var output = Regex.Split(code, pattern);
            if (output.Length > 3)
            {
                var country = output[0];
                var region = output[1];
                var user = output[2];
                var lapisId = output[3];

                if (!String.IsNullOrEmpty(country) && !String.IsNullOrEmpty(region) && !String.IsNullOrEmpty(user) && !String.IsNullOrEmpty(lapisId))
                {
                    var searchResult = lapisCodeRepository.GetIdAndCodeByCode(country, region, user, lapisId, cancellationToken);
                    var enumerator = searchResult.GetAsyncEnumerator(cancellationToken);

                    while (await enumerator.MoveNextAsync())
                    {
                        var lapis = await lapisRepository.GetByIdAsync(enumerator.Current.LapisId, cancellationToken);
                        var lapisImage = await imageRepository.GetById(lapis.ImageId, cancellationToken);

                        var lapisCode = enumerator.Current.Country + "/" + enumerator.Current.Region + "/" + enumerator.Current.User + "/" + enumerator.Current.Lapis;

                        yield return new SearchItem
                        {
                            Id = lapis.Id,
                            Name = lapis.Name,
                            Code = lapisCode,
                            Image = lapisImage != null ?
                                    new Image { Path = lapisImage.Path, Height = lapisImage.Height, Width = lapisImage.Width } :
                                    new Image { }
                        };
                    }
                }
            }
        }
    }
}
