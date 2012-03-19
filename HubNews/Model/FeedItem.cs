using System;

namespace HubNews.Model
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
