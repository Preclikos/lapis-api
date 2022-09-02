using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LapisApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class JwtController : ControllerBase
    {

        public JwtController()
        {
        }

        [HttpGet("Decode")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<string> Decode()
        {

            return "Success";
        }
    }
}
