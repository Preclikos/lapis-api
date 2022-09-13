namespace WebApi.Responses.Models
{
    public class FeedItem
    {
        public FeedItem()
        {
            User = new FeedUser();
            Activity = new FeedActivity();
        }

        public int Id { get; set; }
        public FeedUser User { get; set; }
        public FeedActivity Activity { get; set; }
    }
}
