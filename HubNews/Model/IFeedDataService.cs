using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HubNews.Model
{
    public interface IFeedDataService
    {
        ObservableCollection<FeedItem> FeedsItems { get; }

        void GetFeedItems(string url, Action<ICollection<FeedItem>, Exception> callback);
    }
}
