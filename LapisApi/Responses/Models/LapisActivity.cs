using System.Collections.Generic;

namespace WebApi.Responses.Models
{
    public class LapisActivity
    {
        public LapisActivity(int responseLimit)
        {
            ResponseLimit = responseLimit;
            ActivityItems = new List<LapisActivityItem>();
        }

        public int ResponseLimit { get; set; }
        public IEnumerable<LapisActivityItem> ActivityItems { get; set; }
    }
}
