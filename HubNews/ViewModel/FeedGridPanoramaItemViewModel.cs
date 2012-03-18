﻿using System;
using System.Collections.ObjectModel;
using HubNews.Model;

namespace HubNews.ViewModel
{
    public class FeedGridPanoramaItemViewModel : PanoramaItemViewModelBase
    {
        public ObservableCollection<FeedItem> Items { get; set; }

        public FeedGridPanoramaItemViewModel(IFeedDataService feedDataService)
        {
            Items = feedDataService.FeedsItems;
            HeaderText = "En direct";
        }
    }
}
