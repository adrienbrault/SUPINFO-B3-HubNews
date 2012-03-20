using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using HubNews.Model;

namespace HubNews.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<PanoramaItemViewModelBase> PanoramaItems { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            protected set { _isLoading = value; RaisePropertyChanged("IsLoading"); }
        }

        public RelayCommand RefreshCommand { get; private set; }
        public RelayCommand NavigateToAboutCommand { get; private set; }

        protected IFeedDataService FeedDataService { get; private set; }
        
        public MainViewModel(IFeedDataService feedDataService)
        {
            FeedDataService = feedDataService;

            // Configure behaviors

            FeedDataService.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e)
            {
                if ("RequestsCount" == e.PropertyName)
                {
                    IsLoading = FeedDataService.RequestsCount > 0;
                }
            };

            RefreshCommand = new RelayCommand(() =>
            {
                if (!IsLoading)
                {
                    foreach (var panoramaItem in PanoramaItems)
                    {
                        if (panoramaItem is FeedListPanoramaItemViewModel)
                        {
                            var feedListPanoramaItem = panoramaItem as FeedListPanoramaItemViewModel;
                            feedListPanoramaItem.RefreshCommand.Execute(null);
                        }
                    }
                }
            });

            NavigateToAboutCommand = new RelayCommand(() => Messenger.Default.Send<Uri>(new Uri("/View/About.xaml", UriKind.Relative), "NavigationRequest"));

            // Configure panorama items/data

            var siteLeParisien = new NewsSite()
            {
                Name = "Le parisien",
                ImageUrl = "http://t2.gstatic.com/images?q=tbn:ANd9GcTEo1dBnDfqQpSuHz3qXgUImPbqqyx74rOgv01ZAYbD2oUzq6BG",
                Url = "http://www.leparisien.fr",
                FeedUrl = "http://rss.leparisien.fr/leparisien/rss/actualites-a-la-une.xml"
            };

            var siteLeMonde = new NewsSite()
            {
                Name = "Le monde",
                ImageUrl = "http://t1.gstatic.com/images?q=tbn:ANd9GcQUuiOAOZ_XdyAFEFHpmeD8Bef4S7yXtr8UGRIeO-5qFz7ArlIikA",
                Url = "http://www.lemonde.fr",
                FeedUrl = "http://rss.lemonde.fr/c/205/f/3050/index.rss"
            };

            var siteLeFigaro = new NewsSite()
            {
                Name = "Le Figaro",
                ImageUrl = "http://t1.gstatic.com/images?q=tbn:ANd9GcQQynnyb0aBX5wbcfekFPp-RbgR_NqJ8imXW9y0WkYb0zjj7byN",
                Url = "http://www.lefigaro.fr",
                FeedUrl = "http://rss.lefigaro.fr/lefigaro/laune"
            };

            PanoramaItems = new ObservableCollection<PanoramaItemViewModelBase>()
            {
                new FeedGridPanoramaItemViewModel(FeedDataService),
                new FeedListPanoramaItemViewModel(siteLeParisien, FeedDataService),
                new FeedListPanoramaItemViewModel(siteLeMonde, FeedDataService),
                new FeedListPanoramaItemViewModel(siteLeFigaro, FeedDataService)
            };

            // Trigger the refresh command

            RefreshCommand.Execute(null);
        }

        public void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            foreach (var panoramaItem in PanoramaItems)
            {
                panoramaItem.OnNavigatedTo(e);
            }
        }
    }
};