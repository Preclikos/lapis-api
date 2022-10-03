namespace WebApi.Database.Models
{
    public class LapisImage
    {
        public int Id { get; set; }
        public int LapisId { get; set; }
        public int UserId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Path { get; set; }
    }
}
