using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HubNews.Model
{
    public interface IFeedDataService : INotifyPropertyChanged
    {
        ObservableCollection<FeedItem> FeedsItems { get; }
        int RequestsCount { get; }

        void GetFeedItems(string url, Action<ICollection<FeedItem>, Exception> callback);
    }
}
