using Microsoft.AspNetCore.Http;

namespace WebApi.Requests
{
    public class FileModel
    {
        public IFormFile FormFile { get; set; }
    }
}
