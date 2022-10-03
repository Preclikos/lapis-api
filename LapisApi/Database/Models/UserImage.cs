namespace WebApi.Database.Models
{
    public class UserImage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Path { get; set; }
    }
}
