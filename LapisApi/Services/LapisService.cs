using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
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
        private readonly IImageService imageService;
        private readonly ILapisLocationRepository locationRepository;
        private readonly IActivityRepository activityRepository;


        public LapisService(ILapisRepository lapisRepository, ILapisCodeRepository lapisCodeRepository, IImageService imageService, ILapisLocationRepository locationRepository, IActivityRepository activityRepository)
        {
            this.lapisRepository = lapisRepository;
            this.lapisCodeRepository = lapisCodeRepository;
            this.imageService = imageService;
            this.locationRepository = locationRepository;
            this.activityRepository = activityRepository;

        }

        public async Task<Responses.Models.Lapis> GetById(int id, CancellationToken cancellationToken)
        {
            var lapis = await lapisRepository.GetByIdAsync(id, cancellationToken);
            var lapisCode = await lapisCodeRepository.GetByLapisId(id, cancellationToken);
            var lapisImage = lapis != null ? await imageService.GetLapisById(lapis.ImageId, cancellationToken) : null;

            return lapis != null ? new Responses.Models.Lapis
            {
                Id = lapis.Id,
                Code = lapisCode != null ? String.Format("{0}/{1}/{2}/{3}", lapisCode.Country, lapisCode.Region, lapisCode.User, lapisCode.Lapis) : String.Empty,
                Name = lapis.Name,
                Description = lapis.Description,
                TimeStamp = lapis.TimeStamp.ToUnixTime(),
                UserId = lapis.UserId,
                Image = lapisImage
            } : null;
        }

        public async Task<LapisActivity> GetLapisActivities(int id, int offset, CancellationToken cancellationToken)
        {

            var activities = await activityRepository.GetActivitiesByLapisId(id, FeedLimit, offset * FeedLimit, cancellationToken);
            if (activities != null && activities.Count() > 0)
            {
                var activityItems = activities
                     .Select(async s =>
                         new LapisActivityItem
                         {
                             Id = s.Id,
                             Type = s.Type,
                             Description = s.Description,
                             TimeStamp = s.TimeStamp.ToUnixTime(),
                             UserId = s.UserId,
                             Images = await GetActivityImages(s.ImageId, s.OtherImageIds, cancellationToken),
                             Location = s.LocationId != null ? await GetLapisActivityLocation((int)s.LocationId, cancellationToken) : null
                         }
                     );

                Task.WaitAll(activityItems.ToArray(), cancellationToken);
                return new LapisActivity(FeedLimit)
                {
                    ActivityItems = activityItems.Where(w => w.IsCompleted && !w.IsCanceled).Select(s => s.Result)
                };
            }
            return new LapisActivity(FeedLimit);
        }

        public async Task<IEnumerable<Image>> GetActivityImages(int imageId, string otherImageIdsJson, CancellationToken cancellationToken)
        {
            var imageIds = new[] { imageId };
            var otherImages = JsonConvert.DeserializeObject<int[]>(otherImageIdsJson);

            var images = await imageService.GetLapisById(imageIds.Concat(otherImages), cancellationToken);

            return images != null && images.Count() > 0 ? images : new List<Image>();
        }

        public async Task<Responses.Models.LapisLocation> GetLapisLastLocation(int id, CancellationToken cancellationToken)
        {
            var location = await locationRepository.GetLastByLapisId(id, cancellationToken);
            return location != null ? new Responses.Models.LapisLocation
            {
                Lat = location.Lat,
                Long = location.Long,
            } : new Responses.Models.LapisLocation();
        }

        public async Task<LapisActivityLocation> GetLapisActivityLocation(int id, CancellationToken cancellationToken)
        {
            var location = await locationRepository.GetById(id, cancellationToken);
            return location != null ? new LapisActivityLocation
            {
                Lat = location.Lat,
                Long = location.Long,
                CountryCode = location.CountryCode,
                City = location.City,
            } : new LapisActivityLocation();
        }

        public async Task<LapisOverview> GetOverviewById(int id, CancellationToken cancellationToken)
        {
            var locationCount = await locationRepository.GetLocationCountByLapisId(id, cancellationToken);

            return new LapisOverview
            {
                Locations = locationCount,
            };
        }
    }
}
