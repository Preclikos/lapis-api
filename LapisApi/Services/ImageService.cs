using Microsoft.AspNetCore.Hosting;
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
using Microsoft.Extensions.Hosting;
using WebApi.Configuration;
using Microsoft.Extensions.Options;

namespace WebApi.Services
{
    public class ImageService : IImageService
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILapisImageRepository lapisRepository;
        private readonly IUserImageRepository userRepository;
        private readonly IWebHostEnvironment environment;
        private readonly IOptions<ImageServerOptions> imageServerOptions;

        public ImageService(IHttpContextAccessor httpContextAccessor, ILapisImageRepository lapisRepository, IUserImageRepository userRepository, IWebHostEnvironment environment, IOptions<ImageServerOptions> imageServerOptions)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.lapisRepository = lapisRepository;
            this.userRepository = userRepository;
            this.environment = environment;
            this.imageServerOptions = imageServerOptions;
        }

        public async Task<Image> GetLapisById(int id, CancellationToken cancellationToken)
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

        public async Task<IEnumerable<Image>> GetLapisById(IEnumerable<int> ids, CancellationToken cancellationToken)
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

            var ipv4 = ip.ToString();

            var securePhase = $"{timeStamp}{path}{ipv4} {imageServerOptions.Value.SecurityPhase}";      

            var securePhaseBytes = Encoding.ASCII.GetBytes(securePhase);

            var hashed = Convert.ToBase64String(MD5.HashData(securePhaseBytes));

            var md5HashString = hashed.Replace('+', '-').Replace('/', '_').Replace("=", "");

            return $"{path}?hash={md5HashString}&expires={timeStamp}";
        }

        private int ToUnixTimestamp(DateTime value)
        {
            return (int)Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        private IPAddress GetIP()
        {
            return !environment.IsDevelopment() ? 
                httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress : 
                IPAddress.Parse(imageServerOptions.Value.ByPassAddress);
        }
    }
}
