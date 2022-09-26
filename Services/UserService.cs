using System.Threading;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserImageRepository imageRepository;

        public UserService(IUserRepository userRepository, IUserImageRepository imageRepository)
        {
            this.userRepository = userRepository;
            this.imageRepository = imageRepository;
        }

        public async Task<User> GetById(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetById(id, cancellationToken);
            var userProfile = await userRepository.GeProfiletById(id, cancellationToken);
            var userImage = await imageRepository.GetById(userProfile?.ImageId ?? 0, cancellationToken);

            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Motto = userProfile?.Motto ?? "",
                Country = userProfile?.CountryId.ToString(),
                Image = new Image
                {
                    Path = userImage?.Path,
                }
            };
        }

        public async Task<User> GetBySub(string sub, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetBySub(sub, cancellationToken);
            var userProfile = await userRepository.GeProfiletById(user.Id, cancellationToken);
            var userImage = await imageRepository.GetById(userProfile?.ImageId ?? 0, cancellationToken);

            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Motto = userProfile?.Motto ?? "",
                Country = userProfile != null ? userProfile.CountryId.ToString() : "",
                Image = new Image
                {
                    Path = userImage?.Path,
                }
            };
        }
    }
}
