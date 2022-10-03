using WebApi.Enums;

namespace WebApi.Database.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Motto { get; set; }
        public int ImageId { get; set; }
        public Countries CountryId { get; set; }
        public int RegionId { get; set; }
    }
}
