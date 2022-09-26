using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Responses.Models;

namespace WebApi.Services
{
    public class ImageService : IImageService
    {
        private const string SecureString = "kanarekNakopnulKyblik";

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILapisImageRepository lapisRepository;
        private readonly IUserImageRepository userRepository;

        public ImageService(IHttpContextAccessor httpContextAccessor, ILapisImageRepository lapisRepository, IUserImageRepository userRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.lapisRepository = lapisRepository;
            this.userRepository = userRepository;
        }

        public async Task<Image> GetLapisImageById(int id, CancellationToken cancellationToken)
        {
            var ip = GetIP();
            var image = await lapisRepository.GetById(id, cancellationToken);
            if (image != null && ip != null)
            {
                return new Image
                {
                    Path = HashSecuredPath(image.Path, ip),
                    Width = image.Width,
                    Height = image.Height,
                };
            }

            return new Image();

        }

        public async Task<IEnumerable<Image>> GetById(IEnumerable<int> ids, CancellationToken cancellationToken)
        {
            var ip = GetIP();
            var images = await lapisRepository.GetById(ids, cancellationToken);
            if (images != null && images.Count() > 0 && ip != null)
            {
                return images.Select(s => new Image
                {              
                    Id = s.Id,
                    Path = HashSecuredPath(s.Path, ip),
                    Width = s.Width,
                    Height = s.Height,
                });
            }

            return new List<Image>();

        }

        private string HashSecuredPath(string path, IPAddress ip)
        {
            var currentDate = DateTime.UtcNow + TimeSpan.FromDays(1);

            var timeStamp = ToUnixTimestamp(currentDate);

            var securePatter = "{0}{1}{2} {3}";

            var ipv4 = ip.ToString();

            var fullFilledPatter = String.Format(securePatter, timeStamp, path, ipv4, SecureString);

            var filledByteArray = Encoding.ASCII.GetBytes(fullFilledPatter);

            var hashed = Convert.ToBase64String(MD5.HashData(filledByteArray));

            var md5HashString = hashed.Replace('+', '-').Replace('/', '_').Replace("=", "");

            return String.Format("{0}?hash={1}&expires={2}", path,md5HashString, timeStamp);
        }

        private int ToUnixTimestamp(DateTime value)
        {
            return (int)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        private IPAddress GetIP()
        {
            return httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
        }
    }
}
