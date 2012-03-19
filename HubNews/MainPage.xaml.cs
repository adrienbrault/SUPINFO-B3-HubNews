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
        }

        private void OnApplicationBarRefreshIconButtonClick(object sender, System.EventArgs e)
        {
            var vm = DataContext as MainViewModel;
            if (null != vm)
            {
                vm.RefreshCommand.Execute(null);
            }
        }

        private void OnApplicationBarAboutIconButtonClick(object sender, System.EventArgs e)
        {
            var vm = DataContext as MainViewModel;
            
        }
    }
}
