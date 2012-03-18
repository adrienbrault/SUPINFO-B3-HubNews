using System;
using GalaSoft.MvvmLight;

namespace HubNews.ViewModel
{
    public abstract class PanoramaItemViewModelBase : ViewModelBase
    {
        public string HeaderText { get; protected set; }
        public string HeaderImageUrl { get; protected set; }
    }
}
