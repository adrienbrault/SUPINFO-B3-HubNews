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

        private FeedItem _selectedFeedItem;
        public FeedItem SelectedFeedItem
        {
            get { return _selectedFeedItem; }
            set { _selectedFeedItem = value; RaisePropertyChanged("SelectedFeedItem"); }
        }

        public RelayCommand NavigateToFeedItemDetailCommand { get; private set; }

        protected PanoramaItemViewModelBase()
        {
            NavigateToFeedItemDetailCommand = new RelayCommand(() =>
            {
                if (null != SelectedFeedItem)
                {
                    var uri = new Uri("/View/FeedItemDetail.xaml?id=" + SelectedFeedItem.Id, UriKind.Relative);
                    Messenger.Default.Send<Uri>(uri, "NavigationRequest");
                }
            });
        }

        public void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            SelectedFeedItem = null;
        }
    }
}
