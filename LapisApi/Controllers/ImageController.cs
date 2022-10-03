using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Requests;

namespace WebApi.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> UploadLapisImage([FromForm] FileModel file, CancellationToken cancellationToken)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "image_root");

                using (Stream stream = file.FormFile.OpenReadStream())
                {
                    var image = await Image.LoadAsync(stream, cancellationToken);
                    image.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size { Height = 4096, Width = 4096 } }));
                    await image.SaveAsJpegAsync(path);
                    image.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size { Height = 2048, Width = 2048 } }));
                    await image.SaveAsJpegAsync(path);
                    image.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size { Height = 1024, Width = 1024} }));
                    await image.SaveAsJpegAsync(path);
                    image.Mutate(x => x.Resize(new ResizeOptions { Mode = ResizeMode.Max, Size = new Size { Height = 512, Width = 512 } }));
                    await image.SaveAsJpegAsync(path);
                }

                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
