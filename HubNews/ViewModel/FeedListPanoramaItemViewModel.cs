using System;
using System.Collections.ObjectModel;
using HubNews.Model;

namespace HubNews.ViewModel
{
    public class FeedListPanoramaItemViewModel : PanoramaItemViewModelBase
    {
        public ObservableCollection<FeedItem> Items { get; set; }

        public FeedListPanoramaItemViewModel()
        {
            Items = new ObservableCollection<FeedItem>();
        }
    }
}
