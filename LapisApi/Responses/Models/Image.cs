namespace WebApi.Responses.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Alt { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
    }
}
