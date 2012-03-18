using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace HubNews.Model
{
    public interface IFeedDataService
    {
        void GetFeedItems(string url, Action<ICollection<FeedItem>, Exception> callback);
        ObservableCollection<FeedItem> FeedsItems { get; }
    }
}
