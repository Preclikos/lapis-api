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
    public class FeedService : IFeedService
    {
        const int FeedLimit = 8;

        private readonly IActivityRepository activityRepository;
        private readonly ILapisImageRepository imageRepository;
        private readonly IImageService imageService;

        public FeedService(IActivityRepository activityRepository, ILapisImageRepository imageRepository, IImageService imageService)
        {
            this.activityRepository = activityRepository;
            this.imageRepository = imageRepository;
            this.imageService = imageService;
        }

        public async Task<Feed> GetFeedItems(int country, int offset, CancellationToken cancellationToken)
        {
            try
            {
                var activities = await activityRepository.GetLastActivities(country, FeedLimit, FeedLimit * offset, cancellationToken);
                var imageIds = activities.Select(s => s.ImageId);

                var images = await imageService.GetLapisById(imageIds, cancellationToken);
                var feedItems = activities
                     .Select(s =>
                         new FeedItem
                         {
                             Id = s.Id,
                             Type = "Location",
                             LapisId = s.LapisId,
                             Description = s.Description,
                             TimeStamp = s.TimeStamp.ToUnixTime(),
                             UserId = s.UserId,
                             Image = images.SingleOrDefault(sw => s.ImageId == sw.Id)
                         }
                     );
                return new Feed(FeedLimit)
                {
                    FeedItems = feedItems
                };
            }
            catch(TaskCanceledException)
            {
                return new Feed(FeedLimit);
            }
        }
    }
}
