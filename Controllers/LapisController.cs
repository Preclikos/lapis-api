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
        private readonly ILapisService lapisService;
        public LapisController(ILapisService lapisService)
        {
            this.lapisService = lapisService;
        }

        [HttpGet("Id/{id}")]
        public async Task<Lapis> GetLapisById(int id, CancellationToken cancellationToken)
        {
            return await lapisService.GetById(id, cancellationToken);
        }
    }
}
