using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using HubNews.Model;

namespace HubNews.ViewModel
{
    public class FeedItemDetailViewModel : ViewModelBase
    {
        protected IFeedDataService FeedDataService { get; private set; }

        private FeedItem _feedItem;
        public FeedItem FeedItem
        {
            get { return _feedItem; }
            protected set { _feedItem = value; RaisePropertyChanged("FeedItem"); }
        }

        public FeedItemDetailViewModel(IFeedDataService feedDataService)
        {
            FeedDataService = feedDataService;
        }

        public void Initialize(IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                return;
            }

            string idString = null;
            if (!parameters.TryGetValue("id", out idString))
            {
                return;
            }

            FeedItem = FeedDataService.GetFeedItemById(idString);
        }
    }
}
