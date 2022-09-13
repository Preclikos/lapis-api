namespace WebApi.Responses.Models
{
    public class FeedUser
    {
        public FeedUser()
        {
            Image = new FeedImage();
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public string Designation { get;set }
        public string LastActivity { get; set; }
        public FeedImage Image { get; set; }
    }
}
