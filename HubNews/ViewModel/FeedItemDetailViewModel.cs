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
using GalaSoft.MvvmLight.Command;
using HubNews.Model;
using Microsoft.Phone.Tasks;

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

        public RelayCommand EmailCommand { get; private set; }
        public RelayCommand OpenCommand { get; private set; }

        public FeedItemDetailViewModel(IFeedDataService feedDataService)
        {
            FeedDataService = feedDataService;

            EmailCommand = new RelayCommand(() =>
            {
                var emailcomposer = new EmailComposeTask
                {
                    Subject = "Check out this article",
                    Body = "Hey, you should check out this article: " + FeedItem.Url
                };

                emailcomposer.Show();
            });

            OpenCommand = new RelayCommand(() =>
            {
                var task = new WebBrowserTask()
                {
                    Uri = new Uri(FeedItem.Url)
                };

                task.Show();
            });
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
