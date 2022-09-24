using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Extensions;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class LapisService : ILapisService
    {
        const int FeedLimit = 8;

        private readonly ILapisRepository lapisRepository;
        private readonly ILapisCodeRepository lapisCodeRepository;
        private readonly ILapisImageRepository imageRepository;
        private readonly IActivityRepository activityRepository;

        public LapisService(ILapisRepository lapisRepository, ILapisCodeRepository lapisCodeRepository, ILapisImageRepository imageRepository, IActivityRepository activityRepository)
        {
            this.lapisRepository = lapisRepository;
            this.lapisCodeRepository = lapisCodeRepository;
            this.imageRepository = imageRepository;
            this.activityRepository = activityRepository;
        }

        public async Task<Responses.Models.Lapis> GetById(int id, CancellationToken cancellationToken)
        {
            var lapis = await lapisRepository.GetByIdAsync(id, cancellationToken);
            var lapisCode = await lapisCodeRepository.GetByLapisId(id, cancellationToken);
            var lapisImage = lapis != null ? await imageRepository.GetById(lapis.ImageId, cancellationToken) : null;

            return lapis != null ? new Responses.Models.Lapis
            {
                Id = lapis.Id,
                Code = lapisCode != null ? String.Format("{0}/{1}/{2}/{3}", lapisCode.Country, lapisCode.Region, lapisCode.User, lapisCode.Lapis) : String.Empty,
                Name = lapis.Name,
                Description = lapis.Description,
                TimeStamp = lapis.TimeStamp.ToUnixTime(),
                UserId = lapis.UserId,
                Image = GetLapisImage(lapisImage)
            } : null;
        }

        private Image GetLapisImage(LapisImage image)
        {
            return image != null ? new Image
            {
                Src = image.Path,
                Width = image.Width,
                Height = image.Height
            }
            : new Image();
        }

        public async Task<LapisActivity> GetLapisActivities(int id, int offset, CancellationToken cancellationToken)
        {

            var activities = await activityRepository.GetActivitiesByLapisId(id, FeedLimit, offset * FeedLimit, cancellationToken);
            var activityItems = activities
                 .Select(s =>
                     new LapisActivityItem
                     {
                         Id = s.Id,
                         Type = s.Type,
                         Description = s.Description,
                         TimeStamp = s.TimeStamp.ToUnixTime(),
                         UserId = s.UserId,
                         //Image = GetFeedImage(images, s.ImageId)
                     }
                 );

            return new LapisActivity(FeedLimit)
            {
                ActivityItems = activityItems
            };
        }
    }
}
