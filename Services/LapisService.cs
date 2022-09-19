using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class LapisService : ILapisService
    {
        const int FeedLimit = 8;

        private readonly ILapisRepository lapisRepository;
        private readonly ILapisImageRepository imageRepository;

        public LapisService(ILapisRepository lapisRepository, ILapisImageRepository imageRepository)
        {
            this.lapisRepository = lapisRepository;
            this.imageRepository = imageRepository;
        }

        public async Task<Responses.Models.Lapis> GetById(int id, CancellationToken cancellationToken)
        {
            var lapis = await lapisRepository.GetByIdAsync(id, cancellationToken);
            var lapisImage = lapis != null ? await imageRepository.GetById(lapis.ImageId, cancellationToken) : null;

            return lapis != null ? new Responses.Models.Lapis
            {
                Id = lapis.Id,
                Name = lapis.Name,
                Description = lapis.Description,
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
    }
}
