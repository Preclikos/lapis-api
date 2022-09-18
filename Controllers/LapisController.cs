using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LapisController : ControllerBase
    {
        private readonly IUserService userService;
        public LapisController(IUserService userService)
        {
            this.userService = userService;
        }

    }
}
