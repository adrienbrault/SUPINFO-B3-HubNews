using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GalaSoft.MvvmLight.Command;
using HubNews.Model;

namespace HubNews.ViewModel
{
    public class FeedListPanoramaItemViewModel : PanoramaItemViewModelBase
    {
        private ObservableCollection<FeedItem> _items;
        public ObservableCollection<FeedItem> Items
        {
            get { return _items; }
            protected set { _items = value; RaisePropertyChanged("Items"); }
        }

        private NewsSite _newsSite;
        public NewsSite NewsSite
        {
            get { return _newsSite; }
            protected set { _newsSite = value; RaisePropertyChanged("NewsSite"); }
        }

        public RelayCommand RefreshCommand { get; private set; }

        protected IFeedDataService FeedDataService { get; set; }

        public FeedListPanoramaItemViewModel(NewsSite newsSite, IFeedDataService feedDataService)
        {
            NewsSite = newsSite;
            FeedDataService = feedDataService;

            if (null != newsSite.ImageUrl)
            {
                HeaderImageUrl = newsSite.ImageUrl;
            }
            else
            {
                HeaderText = newsSite.Name;
            }

            Items = new ObservableCollection<FeedItem>();

            RefreshCommand = new RelayCommand(() => FeedDataService.GetFeedItems(NewsSite.FeedUrl,
                (feedItems, exception) =>
                    {
                        if (null != exception)
                        {
                            Debug.WriteLine(exception.ToString());
                        }
                        else
                        {
                            Items = new ObservableCollection<FeedItem>();
                            foreach (var feedItem in feedItems)
                            {
                                Items.Add(feedItem);
                            }
                        }
                    }
            ));
        }
    }
}
