using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Responses.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        private readonly IFeedService feedService;
        public ActivityController(IFeedService  feedService)
        {
            this.feedService = feedService;
        }

        [HttpGet("Feed")]
        public IEnumerable<FeedItem> Feed(int country, int offset)
        {
            var data = new List<FeedItem>();
            data.Add(
                new FeedItem 
                { 
                    Id = 1 ,
                    User = new FeedUser 
                    { 
                        Name = "Preclikos Husák", 
                        Designation = "Czech",
                        LastActivity = "Pred 5. minutama",
                        Path = "/profile/nejakyid",
                        Image = new FeedImage
                        {
                            Src = "https://scontent-prg1-1.xx.fbcdn.net/v/t1.6435-9/131273260_10218550013368020_3840312457061816031_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=09cbfe&_nc_ohc=M8yomtEYJlEAX89FqW8&_nc_ht=scontent-prg1-1.xx&oh=00_AT_IYFgf9j7VS0xRxh38j6aRKjmpf0KBGf-6suSWs3bO-w&oe=633FCDB6"
                        }
                    },
                    Activity = new FeedActivity 
                    {
                        Excerpt = "Krasny kaminek, dekujeme posuneme dal.",
                        Path = "/lapis/nejakyid",
                        Type = "Location",
                        Title = "Jenda",
                        Image = new FeedImage 
                        { 
                            Src = "https://scontent-prg1-1.xx.fbcdn.net/v/t39.30808-6/306481995_10210504880935976_6210953247463730533_n.jpg?stp=cp6_dst-jpg_p180x540&_nc_cat=102&ccb=1-7&_nc_sid=5cd70e&_nc_ohc=4id0-lYifkMAX_OfjJ5&_nc_ht=scontent-prg1-1.xx&oh=00_AT8EUFc9kNuR2cKLIubhBqAkGTbEzIFZRhqp6LYlbohM0w&oe=6324186D",
                            Width = 720,
                            Height = 540,
                        }
                    }
                }
            );

            data.Add(
                new FeedItem
                {
                    Id = 2,
                    User = new FeedUser
                    {
                        Name = "Marie Langrová",
                        Designation = "Czech",
                        LastActivity = "Pred 30. minutama",
                        Path = "/profile/nejakyid",
                        Image = new FeedImage
                        {
                            Src = "https://scontent-prg1-1.xx.fbcdn.net/v/t1.6435-9/55865112_2850827104935056_1322842037313077248_n.jpg?_nc_cat=104&ccb=1-7&_nc_sid=09cbfe&_nc_ohc=l2sP-W29XnMAX_qKoIB&_nc_ht=scontent-prg1-1.xx&oh=00_AT_OaBLU0aKUF8NLP_D10kG58UwbnKK5IQ338cWnAFsfAw&oe=6341779E"
                        }
                    },
                    Activity = new FeedActivity
                    {
                        Excerpt = "Nalezen nedaleko berouna, pan je asi piskac.",
                        Path = "/lapis/nejakyid",
                        Type = "Location",
                        Title = "Jenda",
                        Image = new FeedImage
                        {
                            Src = "https://scontent-prg1-1.xx.fbcdn.net/v/t39.30808-6/306527926_1697836850597305_3571544960524192567_n.jpg?_nc_cat=111&ccb=1-7&_nc_sid=5cd70e&_nc_ohc=tLbx3d5tAXoAX91xkjw&_nc_ht=scontent-prg1-1.xx&oh=00_AT9WwBtQ6bZMvlbRgdDwrynRAzctv7AsmlGjL5zbKaxygA&oe=63227E2A",
                            Width = 1536,
                            Height = 2048,
                        }
                    }
                }
            );

            data.Add(
                new FeedItem
                {
                    Id = 1,
                    User = new FeedUser
                    {
                        Name = "Preclikos",
                        Designation = "Czech",
                        LastActivity = "Pred 5. minutama",
                        Path = "/profile/nejakyid",
                        Image = new FeedImage
                        {
                            Src = "https://scontent-prg1-1.xx.fbcdn.net/v/t1.6435-9/131273260_10218550013368020_3840312457061816031_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=09cbfe&_nc_ohc=M8yomtEYJlEAX89FqW8&_nc_ht=scontent-prg1-1.xx&oh=00_AT_IYFgf9j7VS0xRxh38j6aRKjmpf0KBGf-6suSWs3bO-w&oe=633FCDB6"
                        }
                    },
                    Activity = new FeedActivity
                    {
                        Excerpt = "Krasny kaminek, dekujeme posuneme dal.",
                        Path = "/lapis/nejakyid",
                        Type = "Location",
                        Title = "Jenda",
                        Image = new FeedImage
                        {
                            Src = "https://scontent-prg1-1.xx.fbcdn.net/v/t39.30808-6/306481995_10210504880935976_6210953247463730533_n.jpg?stp=cp6_dst-jpg_p180x540&_nc_cat=102&ccb=1-7&_nc_sid=5cd70e&_nc_ohc=4id0-lYifkMAX_OfjJ5&_nc_ht=scontent-prg1-1.xx&oh=00_AT8EUFc9kNuR2cKLIubhBqAkGTbEzIFZRhqp6LYlbohM0w&oe=6324186D",
                            Width = 720,
                            Height = 540,
                        }
                    }
                }
            );

            return data;
        }

    }
}
