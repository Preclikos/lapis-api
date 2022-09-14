using System;

namespace WebApi.Responses.Models
{
    public class FeedItem
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public long TimeStamp { get; set; }
        public int UserId { get; set; }
        public Image Image { get; set; }
    }
}
