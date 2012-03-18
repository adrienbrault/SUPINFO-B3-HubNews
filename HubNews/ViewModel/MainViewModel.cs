﻿using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using HubNews.Model;

namespace HubNews.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<PanoramaItemViewModelBase> PanoramaItems { get; set; }

        private readonly IFeedDataService _feedDataService;
        protected IFeedDataService FeedDataService { get { return _feedDataService;  } }
        
        public MainViewModel(IFeedDataService feedDataService)
        {
            _feedDataService = feedDataService;

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
        }
    }
}