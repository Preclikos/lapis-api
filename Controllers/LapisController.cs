using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet("Id/{id}/Activities/{offset}")]
        public async Task<LapisActivity> GetLapisActivitiesById(int id, int offset, CancellationToken cancellationToken)
        {
            return await lapisService.GetLapisActivities(id, offset, cancellationToken);
        }

        [HttpGet("Id/{id}/LastLocation")]
        public async Task<LapisLocation> GetLapisLastLocationById(int id, CancellationToken cancellationToken)
        {
            return await lapisService.GetLapisLastLocation(id, cancellationToken);
        }
    }
}
