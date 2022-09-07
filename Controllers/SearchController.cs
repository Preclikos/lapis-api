using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Database.Interfaces;
using WebApi.Database.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILapisRepository lapisRepository;
        public SearchController(ILapisRepository lapisRepository)
        {
            this.lapisRepository = lapisRepository;
        }

        [HttpGet]
        [HttpGet("Search")]
        public IAsyncEnumerable<Lapis> Search()
        {
            return lapisRepository.SearchByCode("as");
        }

    }
}
