using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using HubNews.ViewModel;
using Microsoft.Phone.Controls;

namespace HubNews.View
{
    public partial class FeedItemDetail : PhoneApplicationPage
    {
        public FeedItemDetail()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            var viewModel = this.DataContext as FeedItemDetailViewModel;
            if (null != viewModel)
            {
                viewModel.Initialize(NavigationContext.QueryString);   
            }
        }

        private void OnApplicationBarEmailIconButtonClick(object sender, EventArgs e)
        {
            var vm = DataContext as FeedItemDetailViewModel;
            if (null != vm)
            {
                vm.EmailCommand.Execute(null);
            }
        }

        private void OnApplicationBarOpenIconButtonClick(object sender, EventArgs e)
        {
            var vm = DataContext as FeedItemDetailViewModel;
            if (null != vm)
            {
                vm.OpenCommand.Execute(null);
            }
        }
    }
}