namespace WebApi.Responses.Models
{
    public class FeedActivity
    {
        public FeedActivity()
        {
            Image = new FeedImage();
        }

        public string Type { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Excerpt { get; set; }
        public FeedImage Image { get; set; }
    }
}