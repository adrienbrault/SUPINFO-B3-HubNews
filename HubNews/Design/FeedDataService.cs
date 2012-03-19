using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HubNews.Helper;
using HubNews.Model;

namespace HubNews.Design
{
    public class FeedDataService : IFeedDataService
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
            var feedItems = GetFeedItemsCollection();
            FeedsItems = new ObservableCollection<FeedItem>();

            foreach (var feedItem in feedItems)
            {
                FeedsItems.Add(feedItem);
            }

            RequestsCount = 3;
        }

        public void GetFeedItems(string url, Action<ICollection<FeedItem>, Exception> callback)
        {
            callback(GetFeedItemsCollection(), null);
        }

        protected ICollection<FeedItem> GetFeedItemsCollection()
        {
            var feedItem = new FeedItem()
            {
                Title = "EN DIRECT. NKM votera PS en cas de 2e tour Hollande-LePen",
                Description = @"C'est son grand jour. Jean-Luc Mélenchon espère réunir ce dimanche 30 000 personnes à la Bastille. Une marche suivie d'un meeting sur la fameuse place... Le candidat du Front de gauche à la...<img src=""http://rss.leparisien.fr/blank-1775101-2215760793.gif"" alt=""blank"" width=""1"" height=""1"" />",
                ImageUrl = "http://www.leparisien.fr/images/2012/03/16/1908440_nkm2.jpg",
                Url = "http://rss.leparisien.fr/item-1375401-2415260793.html",
                Timestamp = new DateTime(2012, 03, 18, 09, 57, 00) // Sun, 18 Mar 2012 09:57:00
            };

            var feedItems = new Collection<FeedItem>();
            for (int i = 0; i < 20; i++)
            {
                feedItems.Add(feedItem);
            }

            return feedItems;
        }
    }
}
