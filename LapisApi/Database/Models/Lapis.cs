using System;

namespace WebApi.Database.Models
{
    public class Lapis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }    
    }
}
