using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using HubNews.Model;
using Microsoft.Practices.ServiceLocation;

namespace HubNews.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IFeedDataService, Design.FeedDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IFeedDataService, FeedDataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}