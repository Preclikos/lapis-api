using System.Collections.Generic;
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
    public class FeedService : IFeedService
    {
        const int FeedLimit = 8;

        private readonly IActivityRepository activityRepository;
        private readonly IImageRepository imageRepository;

        public FeedService(IActivityRepository activityRepository, IImageRepository imageRepository)
        {
            this.activityRepository = activityRepository;
            this.imageRepository = imageRepository;
        }

        public async Task<Feed> GetFeedItems(int country, int offset, CancellationToken cancellationToken)
        {
            var activities = await activityRepository.GetLastActivities(country, FeedLimit, offset, cancellationToken);
            var imageIds = activities.Select(s => s.ImageId);

            var images = await imageRepository.GetById(imageIds, cancellationToken);
           var feedItems = activities
                .Select(s =>
                    new FeedItem
                    {
                        Id = s.Id,
                        Type = "Location",
                        Path = "/activity/",
                        Description = s.Description,
                        TimeStamp = s.TimeStamp.ToUnixTime(),
                        UserId = s.UserId,
                        Image = GetFeedImage(images, s.ImageId)
                    }
                );
            return new Feed(FeedLimit)
            {
                FeedItems = feedItems
            };
        }

        private FeedImage GetFeedImage(IEnumerable<Image> images, int imageId)
        {
            var image = images.SingleOrDefault(sw => imageId == sw.Id);

            return image != null ? new FeedImage
            {
                Src = image.Path,
                Width = image.Width,
                Height = image.Height,
                Alt = ""
            } : new FeedImage();
        }
    }
}
