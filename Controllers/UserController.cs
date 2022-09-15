using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("Id/{id}")]
        public async Task<User> GetById(int id, CancellationToken cancellationToken)
        {
            return await userService.GetUserById(id, cancellationToken);
        }

        [HttpGet("Sub/{sub}")]
        public async Task<User> GetBySub(string sub, CancellationToken cancellationToken)
        {
            return await userService.GetUserBySub(sub, cancellationToken);
        }
    }
}
