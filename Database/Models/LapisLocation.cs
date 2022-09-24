namespace WebApi.Database.Models
{
    public class LapisLocation
    {
        public int Id { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public int LapisId { get; set; }
    }
}
