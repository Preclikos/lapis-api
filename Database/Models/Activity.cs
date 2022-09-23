using System;
using WebApi.Enums;

namespace WebApi.Database.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int LapisId { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public string OtherImageIds { get; set; }
        public string Description { get; set; }
        public ActivityTypes Type { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
