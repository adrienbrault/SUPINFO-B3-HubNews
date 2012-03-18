using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace HubNews.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public static ObservableCollection<PanoramaItemViewModelBase> PanoramaItems { get; set; }

        static MainViewModel()
        {
            PanoramaItems = new ObservableCollection<PanoramaItemViewModelBase>();
        }
        
        public MainViewModel()
        {
        }
    }
}