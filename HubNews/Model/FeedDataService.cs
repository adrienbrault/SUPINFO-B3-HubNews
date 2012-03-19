using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;
using System.ComponentModel;
using HubNews.Helper;

namespace HubNews.Model
{
    public class FeedDataService : IFeedDataService
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<string, ICollection<FeedItem>> FeedsItemsDictionary { get; private set; }
        public ObservableCollection<FeedItem> FeedsItems { get; private set; }

        private int _requestCount;
        public int RequestsCount
        {
            get { return _requestCount; }
            private set
            {
                _requestCount = Math.Max(value, 0);
                PropertyChanged.Raise(() => this.RequestsCount);
            }
        }

        public FeedDataService()
        {
            FeedsItemsDictionary = new Dictionary<string, ICollection<FeedItem>>();
            FeedsItems = new ObservableCollection<FeedItem>();
            RequestsCount = 0;
        }

        public void GetFeedItems(string url, Action<ICollection<FeedItem>, Exception> callback)
        {
            var webClient = new WebClient();
            
            webClient.DownloadStringCompleted += delegate(object sender, DownloadStringCompletedEventArgs e)
            {
                RequestsCount--;

                try
                {
                    if (e.Error != null)
                    {
                        throw new Exception(e.Error.ToString());
                    }

                    var feedItems = ParseFeed(e.Result);
                    OnNewFeedItems(url, feedItems);
                    
                    callback(feedItems, null);
                }
                catch(Exception exception)
                {
                    callback(null, exception);
                }
            };

            RequestsCount++;
            webClient.DownloadStringAsync(new Uri(url));
        }

        public FeedItem GetFeedItemById(string id)
        {
            return FeedsItems.First(feedItem => feedItem.Id == id);
        }

        private ICollection<FeedItem> ParseFeed(string xmlString)
        {
            ICollection<FeedItem> feedItems = new Collection<FeedItem>();

            var xml = XElement.Parse(xmlString);

            foreach (var item in xml.Elements("channel").Elements("item"))
            {
                var feedItem = new FeedItem()
                {
                    Title = item.Element("title").Value,
                    Description = Regex.Replace(item.Element("description").Value, @"<(.|\n)*?>", String.Empty),
                    Timestamp = DateTimeOffset.Parse(item.Element("pubDate").Value),
                    Url = item.Element("link").Value,
                    ImageUrl = null != item.Element("enclosure") ? item.Element("enclosure").Attribute("url").Value : ""
                };
                
                feedItems.Add(feedItem);
            }

            return feedItems;
        }

        private void OnNewFeedItems(string url, ICollection<FeedItem> feedItems)
        {
            FeedsItemsDictionary.Remove(url);
            FeedsItemsDictionary.Add(url, feedItems);

            FeedsItems.Clear();

            // Build a new aggregated list of feed items
            foreach(KeyValuePair<string, ICollection<FeedItem>> pair in FeedsItemsDictionary)
            {
                foreach (var feedItem in pair.Value)
                {
                    FeedsItems.Add(feedItem);
                }
            }

            FeedsItems.OrderBy(x => x.Timestamp);
        }
    }
}
