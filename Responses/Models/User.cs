namespace WebApi.Responses.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Motto { get; set; }
        public string Country { get; set; }
        public int RegionId { get; set; }
        public Image Image { get; set; }
    }
}
