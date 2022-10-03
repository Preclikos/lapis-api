namespace WebApi.Database.Models
{
    public class LapisLocation
    {
        public int Id { get; set; }
        public int LapisId { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public decimal Alt { get; set; }
        public int CountryId { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string State { get; set; }
        public string County { get; set; }
        public string Municipality { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string Road { get; set; }
        public string HouseNumber { get; set; }
    }
}
