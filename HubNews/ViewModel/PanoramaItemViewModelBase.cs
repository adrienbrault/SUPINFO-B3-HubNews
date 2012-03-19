using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HubNews.Model;

namespace HubNews.ViewModel
{
    public abstract class PanoramaItemViewModelBase : ViewModelBase
    {
        public string HeaderText { get; protected set; }
        public string HeaderImageUrl { get; protected set; }

        private ObservableCollection<FeedItem> _items;
        public ObservableCollection<FeedItem> Items
        {
            get { return _items; }
            protected set { _items = value; RaisePropertyChanged("Items"); }
        }

        public FeedItem SelectedFeedItem { get; set; }

        public RelayCommand NavigateToFeedItemDetailCommand { get; private set; }

        protected PanoramaItemViewModelBase()
        {
            NavigateToFeedItemDetailCommand = new RelayCommand(() =>
            {
                var uri = new Uri("/View/FeedItemDetail.xaml?id=" + SelectedFeedItem.Id, UriKind.Relative);
                Messenger.Default.Send<Uri>(uri, "NavigationRequest");
            });
        }
    }
}
