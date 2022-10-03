using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public string Decode()
        {

            return "Success";
        }
    }
}
