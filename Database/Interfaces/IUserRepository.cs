using WebApi.Database.Models;

namespace WebApi.Database.Interfaces
{
    public interface IUserRepository
    {
        public int Add(User entity);

        public User GetBySub(string sub);

        public int UpdateNameBySub(string sub, string name);
    }
}
