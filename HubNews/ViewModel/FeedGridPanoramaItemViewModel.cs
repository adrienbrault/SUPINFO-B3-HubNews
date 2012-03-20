using System;
using System.Collections.ObjectModel;
using HubNews.Model;

namespace HubNews.ViewModel
{
    public class FeedGridPanoramaItemViewModel : PanoramaItemViewModelBase
    {
        public FeedGridPanoramaItemViewModel(IFeedDataService feedDataService) : base()
        {
            Items = feedDataService.FeedsItems;
            HeaderText = "live";
        }
    }
}
