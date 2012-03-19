using System;
using GalaSoft.MvvmLight.Messaging;
using HubNews.ViewModel;
using Microsoft.Phone.Controls;

namespace HubNews
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            Messenger.Default.Register<Uri>(this, "NavigationRequest", (uri) => NavigationService.Navigate(uri));
        }

        // ApplicationBar does not support Binding, so we "cheat" a little on MVVM using click event handlers
        private void OnApplicationBarRefreshIconButtonClick(object sender, System.EventArgs e)
        {
            var vm = DataContext as MainViewModel;
            if (null != vm)
            {
                vm.RefreshCommand.Execute(null);
            }
        }

        // ApplicationBar does not support Binding, so we "cheat" a little on MVVM using click event handlers
        private void OnApplicationBarAboutIconButtonClick(object sender, System.EventArgs e)
        {
            var vm = DataContext as MainViewModel;
            if (null != vm)
            {
                vm.NavigateToAboutCommand.Execute(null);
            }
        }
    }
}
