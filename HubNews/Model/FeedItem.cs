using System;

namespace HubNews.Model
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageLink { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
