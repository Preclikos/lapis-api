using WebApi.Enums;

namespace WebApi.Responses.Models
{
    public class LapisActivityItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ActivityTypes Type { get; set; }    
        public string Description { get; set; }
        public long TimeStamp { get; set; } 
    }
}
