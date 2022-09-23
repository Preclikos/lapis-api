namespace WebApi.Responses.Models
{
    public class Lapis
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public Image Image { get; set; }
    }
}
